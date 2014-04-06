using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;

//Adapted from C++:
// QColorMatrix
//
// Extension of the GDI+ struct ColorMatrix.
// Adds some member functions so you can actually do something with it.
// Use QColorMatrix like ColorMatrix to update the ImmageAttributes class.
// Use at your own risk. Comments welcome.
//
// See: http://www.sgi.com/grafica/matrix/
// http://www.sgi.com/software/opengl/advanced98/notes/node182.html
//
// (c) 2003, Sjaak Priester, Amsterdam.
// mailto:sjaak@sjaakpriester.nl


namespace ShomreiTorah.WinForms
{
	///<summary>A functional replacement for the standard GDI+ ColorMatrix class.</summary>
	public class QColorMatrix
	{
		// The luminance weight factors for the RGB color space.
		// These values are actually preferable to the better known factors of
		// Y = 0.30R + 0.59G + 0.11B, the formula which is used in color television technique.
		const float LuminanceRed = 0.3086f;
		const float LuminanceGreen = 0.6094f;
		const float LuminanceBlue = 0.0820f;


		float[][] values;

		static float[][] CopyMatrixValues(float[][] values)
		{
			return new[] {
				new [] { values[0][0], values[0][1], values[0][2], values[0][3], values[0][4] },
				new [] { values[1][0], values[1][1], values[1][2], values[1][3], values[1][4] },
				new [] { values[2][0], values[2][1], values[2][2], values[2][3], values[2][4] },
				new [] { values[3][0], values[3][1], values[3][2], values[3][3], values[3][4] },
				new [] { values[4][0], values[4][1], values[4][2], values[4][3], values[4][4] }
			};
		}

		#region Basic Interface
		///<summary>Creates a new QColorMatrix for the identity matrix.</summary>
		public QColorMatrix()
		{
			values = new[] {
				new float[] { 1, 0, 0, 0, 0 },
				new float[] { 0, 1, 0, 0, 0 },
				new float[] { 0, 0, 1, 0, 0 },
				new float[] { 0, 0, 0, 1, 0 },
				new float[] { 0, 0, 0, 0, 1 }
			};
		}

		///<summary>Creates a QColorMatrix by copying a native <see cref="System.Drawing.Imaging.ColorMatrix"/>.</summary>
		public QColorMatrix(ColorMatrix native)
		{
			if (native == null) throw new ArgumentNullException("native");
			values = new[] {
				new [] { native.Matrix00, native.Matrix01, native.Matrix02, native.Matrix03, native.Matrix04 },
				new [] { native.Matrix10, native.Matrix11, native.Matrix12, native.Matrix13, native.Matrix14 },
				new [] { native.Matrix20, native.Matrix21, native.Matrix22, native.Matrix23, native.Matrix24 },
				new [] { native.Matrix30, native.Matrix31, native.Matrix32, native.Matrix33, native.Matrix34 },
				new [] { native.Matrix40, native.Matrix41, native.Matrix42, native.Matrix43, native.Matrix44 },
			};
		}

		///<summary>Creates a QColorMatrix by copying a 5x5 matrix.</summary>
		public QColorMatrix(float[][] values)
		{
			if (values == null) throw new ArgumentNullException("values");
			if (values.Length != 5 || values.Any(row => row == null || row.Length != 5)) throw new ArgumentException("values must be a 5x5 matrix", "values");

			this.values = CopyMatrixValues(values);
		}

		///<summary>Gets or sets a single value in the matrix.</summary>
		[SuppressMessage("Microsoft.Design", "CA1023:IndexersShouldNotBeMultidimensional")]
		[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
		public float this[int x, int y]
		{
			get { return values[y][x]; }
			set { values[y][x] = value; }
		}

		///<summary>Creates a deep copy of this matrix.</summary>
		///<returns>A new QColorMatrix instance with the same values as this instance.</returns>
		public QColorMatrix CreateCopy() { return new QColorMatrix(values); }	//The constructor copies its parameter.

		///<summary>Converts a QColorMatrix to a native <see cref="System.Drawing.Imaging.ColorMatrix"/>.</summary>
		public static implicit operator ColorMatrix(QColorMatrix matrix) { return matrix == null ? null : matrix.ToNative(); }

		///<summary>Converts this QColorMatrix to a native <see cref="System.Drawing.Imaging.ColorMatrix"/>.</summary>
		public ColorMatrix ToNative() { return new ColorMatrix(values); }

		///<summary>Converts this QColorMatrix to its string representation.</summary>
		public override string ToString() { return ToString("0.00", CultureInfo.CurrentCulture); }
		///<summary>Converts this QColorMatrix to its string representation.</summary>
		public string ToString(string format, IFormatProvider provider)
		{
			var builder = new StringBuilder(100);
			for (int y = 0; y < 5; y++)
			{
				builder.Append("| ");
				for (int x = 0; x < 5; x++)
				{
					builder.Append(this[x, y].ToString(format, provider));
					builder.Append(' ');
				}
				builder.AppendLine("|");
			}
			return builder.ToString(0, builder.Length - 2);
		}
		#endregion

		///<summary>Creates an ImageAttributes that contains this matrix.</summary>
		public ImageAttributes CreateAttributes()
		{
			var retVal = new ImageAttributes();
			retVal.SetColorMatrix(ToNative());
			return retVal;
		}

		///<summary>Multiplies this matrix by another matrix by prepending the other matrix.</summary>
		///<returns>The original (modified) QColorMatrix.</returns>
		public QColorMatrix MultiplyBy(QColorMatrix other) { return MultiplyBy(other, MatrixOrder.Prepend); }

		///<summary>Multiplies this matrix by another matrix in the specified order.</summary>
		///<returns>The original (modified) QColorMatrix.</returns>
		public QColorMatrix MultiplyBy(QColorMatrix other, MatrixOrder order)
		{
			if (order == MatrixOrder.Append)
				values = (this * other).values;
			else
				values = (other * this).values;
			return this;
		}

		///<summary>Multiplies two matricies.</summary>
		[SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates", Justification = "MultiplyBy")]
		public static QColorMatrix operator *(QColorMatrix left, QColorMatrix right)
		{
			if (left == null || right == null) return null;

			var retVal = new QColorMatrix();
			for (int y = 0; y < 5; y++)
			{
				for (int x = 0; x < 5; x++)
				{

					retVal[x, y] = 0;
					for (int i = 0; i < 5; i++)
						retVal[x, y] += left[i, y] * right[x, i];
				}
			}

			return retVal;
		}
		///<summary>Transforms a vector by this matrix.</summary>
		///<param name="vector">An array with four or five elements.</param>
		///<returns>A transformed vector with four or five elements.  If the vector had four elements, the fifth is assumed to be 0.</returns>
		public float[] TransformVector(params float[] vector)
		{
			if (vector == null) throw new ArgumentNullException("vector");
			if (vector.Length != 4 && vector.Length != 5) throw new ArgumentException("vector must have 4 or 5 elements", "vector");
			var retVal = new float[vector.Length];

			for (int x = 0; x < retVal.Length; x++)
			{
				retVal[x] = 0;
				for (int y = 0; y < 5; y++)
				{
					retVal[x] += this[x, y] * (y == vector.Length ? 1 : vector[y]);
				}
			}

			return retVal;
		}

		///<summary>Transforms a color by this matrix.</summary>
		public Color TransformColor(Color color)
		{
			var rgba = TransformVector(color.R, color.G, color.B, color.A);
			return Color.FromArgb(ConstrainByte(rgba[3]), ConstrainByte(rgba[0]), ConstrainByte(rgba[1]), ConstrainByte(rgba[2]));
		}
		static int ConstrainByte(float value) { if (value < 0) return 0; if (value > 255) return 255; return (int)value; }

		#region Per-color methods
		///<summary>Rotates colors by the given angle.</summary>
		///<param name="phi">The angle to rotate by in degrees.</param>
		///<param name="x">The X coordinate that receives the sin.</param>
		///<param name="y">The Y coordinate that receives the sin.</param>
		///<param name="order">The order to apply the rotation.</param>
		///<returns>The original (modified) QColorMatrix.</returns>
		QColorMatrix RotateColor(float phi, int x, int y, MatrixOrder order)
		{
			phi *= (float)(Math.PI / 180);
			var multiplier = new QColorMatrix();
			multiplier[x, x] = multiplier[y, y] = (float)Math.Cos(phi);

			var sin = (float)Math.Sin(phi);
			multiplier[x, y] = sin;
			multiplier[y, x] = -sin;
			return MultiplyBy(multiplier, order);
		}

		///<summary>Rotates the matrix around the red color axis.</summary>
		///<param name="phi">The angle of rotation in degrees (between -180 and +180).</param>
		///<returns>The original (modified) QColorMatrix.</returns>
		public QColorMatrix RotateRed(float phi) { return RotateRed(phi, MatrixOrder.Prepend); }
		///<summary>Rotates the matrix around the red color axis.</summary>
		///<param name="phi">The angle of rotation in degrees (between -180 and +180).</param>
		///<param name="order">The order to apply the rotation.</param>
		///<returns>The original (modified) QColorMatrix.</returns>
		public QColorMatrix RotateRed(float phi, MatrixOrder order) { return RotateColor(phi, 2, 1, order); }

		///<summary>Rotates the matrix around the green color axis.</summary>
		///<param name="phi">The angle of rotation in degrees (between -180 and +180).</param>
		///<returns>The original (modified) QColorMatrix.</returns>
		public QColorMatrix RotateGreen(float phi) { return RotateGreen(phi, MatrixOrder.Prepend); }
		///<summary>Rotates the matrix around the green color axis.</summary>
		///<param name="phi">The angle of rotation in degrees (between -180 and +180).</param>
		///<param name="order">The order to apply the rotation.</param>
		///<returns>The original (modified) QColorMatrix.</returns>
		public QColorMatrix RotateGreen(float phi, MatrixOrder order) { return RotateColor(phi, 0, 2, order); }

		///<summary>Rotates the matrix around the blue color axis.</summary>
		///<param name="phi">The angle of rotation in degrees (between -180 and +180).</param>
		///<returns>The original (modified) QColorMatrix.</returns>
		public QColorMatrix RotateBlue(float phi) { return RotateBlue(phi, MatrixOrder.Prepend); }
		///<summary>Rotates the matrix around the blue color axis.</summary>
		///<param name="phi">The angle of rotation in degrees (between -180 and +180).</param>
		///<param name="order">The order to apply the rotation.</param>
		///<returns>The original (modified) QColorMatrix.</returns>
		public QColorMatrix RotateBlue(float phi, MatrixOrder order) { return RotateColor(phi, 1, 0, order); }

		QColorMatrix ShearColor(int x, int y1, float d1, int y2, float d2, MatrixOrder order)
		{
			var multiplier = new QColorMatrix();
			multiplier[x, y1] = d1;
			multiplier[x, y2] = d2;
			return MultiplyBy(multiplier, order);
		}


		///<summary>Shears the matrix in the red color plane.</summary>
		///<returns>The original (modified) QColorMatrix.</returns>
		public QColorMatrix ShearRed(float green, float blue) { return ShearRed(green, blue, MatrixOrder.Prepend); }
		///<summary>Shears the matrix in the red color plane.</summary>
		///<returns>The original (modified) QColorMatrix.</returns>
		public QColorMatrix ShearRed(float green, float blue, MatrixOrder order) { return ShearColor(0, 1, green, 2, blue, order); }

		///<summary>Shears the matrix in the green color plane.</summary>
		///<returns>The original (modified) QColorMatrix.</returns>
		public QColorMatrix ShearGreen(float red, float blue) { return ShearGreen(red, blue, MatrixOrder.Prepend); }
		///<summary>Shears the matrix in the green color plane.</summary>
		///<returns>The original (modified) QColorMatrix.</returns>
		public QColorMatrix ShearGreen(float red, float blue, MatrixOrder order) { return ShearColor(1, 0, red, 2, blue, order); }

		///<summary>Shears the matrix in the blue color plane.</summary>
		///<returns>The original (modified) QColorMatrix.</returns>
		public QColorMatrix ShearBlue(float red, float green) { return ShearBlue(red, green, MatrixOrder.Prepend); }
		///<summary>Shears the matrix in the blue color plane.</summary>
		///<returns>The original (modified) QColorMatrix.</returns>
		public QColorMatrix ShearBlue(float red, float green, MatrixOrder order) { return ShearColor(2, 0, red, 1, green, order); }
		#endregion

		///<summary>Translates colors.</summary>
		///<returns>The original (modified) QColorMatrix.</returns>
		public QColorMatrix Translate(float red, float green, float blue, float alpha) { return Translate(red, green, blue, alpha, MatrixOrder.Prepend); }
		///<summary>Translates colors.</summary>
		///<returns>The original (modified) QColorMatrix.</returns>
		public QColorMatrix Translate(float red, float green, float blue, float alpha, MatrixOrder order)
		{
			var multiplier = new QColorMatrix();
			multiplier[4, 0] = red;
			multiplier[4, 1] = green;
			multiplier[4, 2] = blue;
			multiplier[4, 3] = alpha;
			return MultiplyBy(multiplier, order);
		}
		///<summary>Scales colors.</summary>
		///<returns>The original (modified) QColorMatrix.</returns>
		public QColorMatrix Scale(float red, float green, float blue, float alpha) { return Scale(red, green, blue, alpha, MatrixOrder.Prepend); }
		///<summary>Scales colors.</summary>
		///<returns>The original (modified) QColorMatrix.</returns>
		public QColorMatrix Scale(float red, float green, float blue, float alpha, MatrixOrder order)
		{
			var multiplier = new QColorMatrix();
			multiplier[0, 0] = red;
			multiplier[1, 1] = green;
			multiplier[2, 2] = blue;
			multiplier[3, 3] = alpha;
			return MultiplyBy(multiplier, order);
		}

		///<summary>Sets the saturation.</summary>
		///<returns>The original (modified) QColorMatrix.</returns>
		public QColorMatrix SetSaturation(float saturation) { return SetSaturation(saturation, MatrixOrder.Prepend); }
		///<summary>Sets the saturation.</summary>
		///<returns>The original (modified) QColorMatrix.</returns>
		public QColorMatrix SetSaturation(float saturation, MatrixOrder order)
		{
			//if (saturation < 0 || saturation > 1) throw new ArgumentOutOfRangeException("saturation", "Saturation must be between zero and one");
			// For the theory behind this, see the web sites at the top of this file.
			// In short: if saturation is 1.0f, m becomes the identity matrix, and this matrix is
			// unchanged. If saturation is 0.0f, each color is scaled by it's luminance weight.

			float satComplement = 1.0f - saturation;
			float satComplR = satComplement * LuminanceRed;
			float satComplG = satComplement * LuminanceGreen;
			float satComplB = satComplement * LuminanceBlue;

			var multiplier = new QColorMatrix(new[] {
				new [] { satComplR + saturation,	satComplR,					satComplR,				0.0f, 0.0f },
				new [] { satComplG,					satComplG + saturation,		satComplG,				0.0f, 0.0f },
				new [] { satComplB,					satComplB,					satComplB + saturation,	0.0f, 0.0f },
				new [] { 0.0f,						0.0f,						0.0f,					1.0f, 0.0f },
				new [] { 0.0f,						0.0f,						0.0f,					0.0f, 1.0f }
			});

			return MultiplyBy(multiplier, order);
		}

		#region RotateHue
		static class HueMatricies
		{
			public static readonly QColorMatrix PreHue = new QColorMatrix();
			public static readonly QColorMatrix PostHue = new QColorMatrix();

			const float greenRotation = 35.0f;
			//const float greenRotation = 39.182655f;

			// NOTE: theoretically, greenRotation should have the value of 39.182655 degrees,
			// being the angle for which the sine is 1/(sqrt(3)), and the cosine is sqrt(2/3).
			// However, I found that using a slightly smaller angle works better.
			// In particular, the greys in the image are not visibly affected with the smaller
			// angle, while they deviate a little bit with the theoretical value.
			// An explanation escapes me for now.
			// If you rather stick with the theory, change the comments in the previous lines.

			[SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Impossible, beforefieldinit")]
			static HueMatricies()
			{
				// Rotating the hue of an image is a rather convoluted task, involving several matrix
				// multiplications. For efficiency, we prepare two static matrices.
				// This is by far the most complicated part of this class. For the background
				// theory, refer to the sgi-sites mentioned at the top of this file.

				// Prepare the preHue matrix.
				// Rotate the grey vector in the green plane.
				PreHue.RotateRed(45.0f);

				// Next, rotate it again in the green plane, so it coincides with the blue axis.
				PreHue.RotateGreen(-greenRotation, MatrixOrder.Append);

				// Hue rotations keep the color luminations constant, so that only the hues change
				// visible. To accomplish that, we shear the blue plane.
				var lum = PreHue.TransformVector(LuminanceRed, LuminanceGreen, LuminanceBlue, 1.0f);

				// Transform the luminance vector.

				// Calculate the shear factors for red and green.
				float red = lum[0] / lum[2];
				float green = lum[1] / lum[2];

				// Shear the blue plane.
				PreHue.ShearBlue(red, green, MatrixOrder.Append);

				// Prepare the postHue matrix. This holds the opposite transformations of the
				// preHue matrix. In fact, postHue is the inversion of preHue.
				PostHue.ShearBlue(-red, -green);
				PostHue.RotateGreen(greenRotation, MatrixOrder.Append);
				PostHue.RotateRed(-45.0f, MatrixOrder.Append);
			}
		}

		///<summary>Rotates the hue around the grey axis, keeping luminance fixed.</summary>
		///<returns>The original (modified) QColorMatrix.</returns>
		public QColorMatrix RotateHue(float phi)
		{
			return MultiplyBy(HueMatricies.PreHue, MatrixOrder.Append)
				  .RotateBlue(phi, MatrixOrder.Append)
				  .MultiplyBy(HueMatricies.PostHue, MatrixOrder.Append);
		}
		#endregion
	}
}
