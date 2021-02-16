using System;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra;

namespace FirstTask
{
    class FirstTask
    {
        static void Main(string[] args)
        {
            DenseVector x_data = DenseVector.OfEnumerable(new double[] { -7, 0, 1.234, 10, -4, 3 });
            DenseVector y_data = DenseVector.OfEnumerable(new double[] { 1132.856, 123.456, 97.575, 9323.456, -17.344, 56.856 });

            PolynomialRegression polynomialRegression = new PolynomialRegression(x_data, y_data, 4);

            string[] input = Console.In.ReadToEnd().Split('\n');

            int T = 0;

            if (input.Length > 0)
            {
                T = int.TryParse(input[0], out int result) ? result : throw new ArgumentException($"Cannot parse {nameof(T)}");
            }
            else
            {
                throw new ArgumentException($"Not enough parameters in {nameof(input)}");
            }

            if (T > 100)
            {
                throw new ArgumentException($"{nameof(T)} cannot be greater than 100");
            }

            double[] xValues = new double[T];

            if (input.Length == T + 1)
            {
                for (int i = 1; i < T + 1; i++)
                {
                    xValues[i - 1] = double.TryParse(input[i], out double result) ? result : throw new ArgumentException($"Cannot parse number for evaluation");

					if(xValues[i-1] < -1000)
                    {
						throw new ArgumentException($"Number for evaluation cannot be less than -1000");
					}
					else if(xValues[i - 1] > 1000)
                    {
						throw new ArgumentException($"Number for evaluation cannot be greater than 1000");
					}
                }
            }

            foreach (var x in xValues)
            {
                Console.Out.WriteLine(polynomialRegression.Calculate(x).ToString("0.000"));
            }
        }
    }

	public class PolynomialRegression
	{
		private int _order;
		private Vector<double> _coefs;

		/// <summary>
		/// Calculates polynom regression for xData = [x1, x2, ... , xn] and yData = [y1, y2, ... , yn].
		/// </summary>
		/// <param name="order">Order of output polynom.</param>
		public PolynomialRegression(DenseVector xData, DenseVector yData, int order)
		{
			_order = order;

			var vandMatrix = new DenseMatrix(xData.Count, order + 1);
			for (int i = 0; i < xData.Count; i++)
			{
				double mult = 1;
				for (int j = 0; j < order + 1; j++)
				{
					vandMatrix[i, j] = mult;
					mult *= xData[i];
				}
			}

			// var vandMatrixT = vandMatrix.Transpose();
			// 1 variant:
			//_coefs = (vandMatrixT * vandMatrix).Inverse() * vandMatrixT * yData;
			// 2 variant:
			//_coefs = (vandMatrixT * vandMatrix).LU().Solve(vandMatrixT * yData);
			// 3 variant (most fast I think. Possible LU decomposion also can be replaced with one triangular matrix):
			_coefs = vandMatrix.TransposeThisAndMultiply(vandMatrix).LU().Solve(TransposeAndMult(vandMatrix, yData));
		}

		/// <summary>
		/// Calculates polynom regression for xData = [0, 1, ... , n] and yData = [y1, y2, ... , yn].
		/// </summary>
		/// <param name="order">Order of output polynom.</param>
		public PolynomialRegression(DenseVector yData, int order)
		{
			_order = order;

			var vandMatrix = new DenseMatrix(yData.Count, order + 1);

			for (int i = 0; i < yData.Count; i++)
			{
				double mult = 1;
				for (int j = 0; j < order + 1; j++)
				{
					vandMatrix[i, j] = mult;
					mult *= i;
				}
			}

			_coefs = vandMatrix.TransposeThisAndMultiply(vandMatrix).LU().Solve(TransposeAndMult(vandMatrix, yData));
		}

		private Vector<double> VandermondeRow(double x)
		{
			double[] result = new double[_order + 1];
			double mult = 1;
			for (int i = 0; i <= _order; i++)
			{
				result[i] = mult;
				mult *= x;
			}
			return new DenseVector(result);
		}

		private static DenseVector TransposeAndMult(Matrix m, Vector v)
		{
			var result = new DenseVector(m.ColumnCount);
			for (int j = 0; j < m.RowCount; j++)
				for (int i = 0; i < m.ColumnCount; i++)
					result[i] += m[j, i] * v[j];
			return result;
		}

		public double Calculate(double x)
		{
			return VandermondeRow(x) * _coefs;
		}
	}
}
