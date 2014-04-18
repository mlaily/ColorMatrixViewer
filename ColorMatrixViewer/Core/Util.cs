//Copyright (c) 2014 Melvyn Laily
//http://arcanesanctum.net

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in
//all copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//THE SOFTWARE.

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
				sb.AppendLine(" }");
			}
			return sb.ToString();
		}

		public static float[,] ParseMatrix(string raw)
		{
			float[,] matrix = new float[5, 5];
			var rows = System.Text.RegularExpressions.Regex.Matches(raw, @"{(?<row>.*?)}",
				System.Text.RegularExpressions.RegexOptions.ExplicitCapture);
			if (rows.Count != 5)
			{
				throw new Exception("The matrix must have 5 rows.");
			}
			for (int x = 0; x < rows.Count; x++)
			{
				var row = rows[x];
				var columnSplit = row.Groups["row"].Value.Split(',');
				if (columnSplit.Length != 5)
				{
					throw new Exception("The matrix must have 5 columns.");
				}
				for (int y = 0; y < matrix.GetLength(1); y++)
				{
					float value;
					if (!float.TryParse(columnSplit[y],
						System.Globalization.NumberStyles.Float,
						System.Globalization.NumberFormatInfo.InvariantInfo,
						out value))
					{
						throw new Exception(string.Format("Unable to parse \"{0}\" to a float.", columnSplit[y]));
					}
					matrix[x, y] = value;
				}
			}
			return matrix;
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
