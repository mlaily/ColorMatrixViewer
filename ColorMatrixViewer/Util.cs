using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMatrixViewer
{
	public static class Util
	{

		public static float[,] FromJaggedArrays(float[][] matrix)
		{
			float[,] result = new float[matrix.Length, matrix.First().Length];
			for (int i = 0; i < matrix.Length; i++)
			{
				for (int j = 0; j < matrix[i].Length; j++)
				{
					result[i, j] = matrix[i][j];
				}
			}
			return result;
		}

		public static float[][] ToJaggedArrays(float[,] matrix)
		{
			float[][] result = new float[matrix.GetLength(0)][];
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				result[i] = new float[matrix.GetLength(1)];
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					result[i][j] = matrix[i, j];
				}
			}
			return result;
		}

		public static string MatrixToString(float[,] matrix)
		{
			int maxDecimal = 0;
			foreach (var item in matrix)
			{
				string toString = item.ToString("0.#######", System.Globalization.NumberFormatInfo.InvariantInfo);
				int indexOfDot = toString.IndexOf('.');
				int currentMax = indexOfDot >= 0 ? toString.Length - indexOfDot - 1 : 0;
				if (currentMax > maxDecimal)
				{
					maxDecimal = currentMax;
				}
			}
			string format = "0." + new string('0', maxDecimal);

			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < matrix.GetLength(0); i++)
			{
				sb.Append("{ ");
				for (int j = 0; j < matrix.GetLength(1); j++)
				{
					if (matrix[i, j] >= 0)
					{
						//align negative signs
						sb.Append(" ");
					}
					sb.Append(matrix[i, j].ToString(format, System.Globalization.NumberFormatInfo.InvariantInfo));
					if (j < matrix.GetLength(1) - 1)
					{
						sb.Append(", ");
					}
				}
				sb.Append(" }\n");
			}
			return sb.ToString();
		}

		public static float[,] Multiply(float[,] a, float[,] b)
		{
			if (a.GetLength(1) != b.GetLength(0))
			{
				throw new Exception("a.GetLength(1) != b.GetLength(0)");
			}
			float[,] c = new float[a.GetLength(0), b.GetLength(1)];
			for (int i = 0; i < c.GetLength(0); i++)
			{
				for (int j = 0; j < c.GetLength(1); j++)
				{
					for (int k = 0; k < a.GetLength(1); k++) // k<b.GetLength(0)
					{
						c[i, j] = c[i, j] + a[i, k] * b[k, j];
					}
				}
			}
			return c;
		}

		public static float[,] MoreBlue(float[,] colorMatrix)
		{
			float[,] temp = (float[,])colorMatrix.Clone();
			temp[2, 4] += 0.1f;//or remove 0.1 off the red
			return temp;
		}

		public static float[,] MoreGreen(float[,] colorMatrix)
		{
			float[,] temp = (float[,])colorMatrix.Clone();
			temp[1, 4] += 0.1f;
			return temp;
		}

		public static float[,] MoreRed(float[,] colorMatrix)
		{
			float[,] temp = (float[,])colorMatrix.Clone();
			temp[0, 4] += 0.1f;
			return temp;
		}

		public static List<float[,]> Interpolate(float[,] A, float[,] B)
		{
			const int STEPS = 10;
			const int SIZE = 5;

			if (A.GetLength(0) != SIZE ||
				A.GetLength(1) != SIZE ||
				B.GetLength(0) != SIZE ||
				B.GetLength(1) != SIZE)
			{
				throw new ArgumentException();
			}

			List<float[,]> result = new List<float[,]>(STEPS);

			for (int i = 0; i < STEPS; i++)
			{
				result.Add(new float[SIZE, SIZE]);

				for (int x = 0; x < SIZE; x++)
				{
					for (int y = 0; y < SIZE; y++)
					{
						// f(x)=ya+(x-xa)*(yb-ya)/(xb-xa)
						//calculate 10 steps, from 1 to 10 (we don't need 0, as we start from there)
						result[i][x, y] = A[x, y] + (i + 1/*-0*/) * (B[x, y] - A[x, y]) / (STEPS/*-0*/);
					}
				}
			}

			return result;
		}

		public static Bitmap ApplyColorMatrix(Image original, float[,] colorMatrix)
		{
			Bitmap bmp = new Bitmap(original.Width, original.Height);

			ColorMatrix matrix = new ColorMatrix(Util.ToJaggedArrays(colorMatrix));

			using (var attributes = new ImageAttributes())
			{
				//Attach matrix to image attributes
				attributes.SetColorMatrix(matrix);
				using (var g = Graphics.FromImage(bmp))
				{
					g.DrawImage(original,
						new Rectangle(0, 0, original.Width, original.Height),
						0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
				}
			}
			return bmp;
		}
	}
}
