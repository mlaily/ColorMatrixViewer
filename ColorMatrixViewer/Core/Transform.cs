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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMatrixViewer
{
	public static class Transform
	{
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
	}
}
