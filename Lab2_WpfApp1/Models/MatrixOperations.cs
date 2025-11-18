using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_WpfApp1.Models
{
    public partial class MyMatrix
    {
        public static MyMatrix operator +(MyMatrix matrix1, MyMatrix matrix2)
        {
            if (matrix2.Height != matrix1.Height || matrix2.Width != matrix1.Width)
                throw new ArgumentException("Matrixes must have the same dimensions for addition.");
            double[,] result = new double[matrix1.Height, matrix1.Width];
            for (int i = 0; i < matrix1.Height; i++)
                for (int j = 0; j < matrix1.Width; j++)
                    result[i, j] = matrix1.matrix[i, j] + matrix2.matrix[i, j];
            return new MyMatrix(result);
        }
        public static MyMatrix operator *(MyMatrix matrix1, MyMatrix matrix2)
        {
            if (matrix1.Width != matrix2.Height)
                throw new ArgumentException("The number of columns in the first matrix must match the number of rows in the second matrix for multiplication.");
            double[,] result = new double[matrix1.Height, matrix2.Width];
            for (int i = 0; i < matrix1.Height; i++)
                for (int j = 0; j < matrix2.Width; j++)
                    for (int k = 0; k < matrix1.Width; k++)
                        result[i, j] += matrix1.matrix[i, k] * matrix2.matrix[k, j];
            return new MyMatrix(result);
        }
        private double[,] GetTransponedArray()
        {
            double[,] transponed = new double[Width, Height];
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    transponed[j, i] = matrix[i, j];
            return transponed;
        }
        public MyMatrix GetTransponedCopy() => new MyMatrix(GetTransponedArray());
        public void TransponeMe()
        {
            matrix = GetTransponedArray();
            cachedDeterminant = null;
        }
        public double CalcDeterminant()
        {
            if (Height != Width)
                throw new InvalidOperationException("Determinant can only be calculated for square matrixes.");

            if (cachedDeterminant.HasValue)
                return cachedDeterminant.Value;

            int n = Height;
            double[,] a = new double[Height, Width];
            for (int i = 0; i < Height; i++)
                for (int j = 0; j < Width; j++)
                    a[i, j] = matrix[i, j];

            double det = 1.0;

            for (int i = 0; i < n; i++)
            {
                int pivot = i;
                for (int row = i + 1; row < n; row++)
                    if (Math.Abs(a[row, i]) > Math.Abs(a[pivot, i]))
                        pivot = row;

                if (Math.Abs(a[pivot, i]) < 1e-12)
                {
                    cachedDeterminant = 0;
                    return 0;
                }

                if (pivot != i)
                {
                    for (int col = 0; col < n; col++)
                    {
                        double tmp = a[i, col];
                        a[i, col] = a[pivot, col];
                        a[pivot, col] = tmp;
                    }
                    det *= -1;
                }

                det *= a[i, i];

                for (int row = i + 1; row < n; row++)
                {
                    double coeff = a[row, i] / a[i, i];
                    for (int col = i; col < n; col++)
                        a[row, col] -= coeff * a[i, col];
                }
            }

            cachedDeterminant = det;
            return det;
        }
    }
}
