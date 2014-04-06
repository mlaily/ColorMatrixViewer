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
