using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Lab2_WpfApp1.Models
{
    public partial class MyMatrix
    {
        private double[,] matrix;
        private double? cachedDeterminant = null;
        public int Height { get { return matrix.GetLength(0); } }
        public int Width { get { return matrix.GetLength(1); } }
        public MyMatrix(double[,] data)
        {
            matrix = (double[,])data.Clone();
        }
        public MyMatrix(double[][] data)
        {
            int rows = data.Length;
            int cols = data[0].Length;

            for (int i = 1; i < rows; i++)
                if (data[i].Length != cols)
                    throw new ArgumentException("All rows must have the same number of columns.");

            matrix = new double[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    matrix[i, j] = data[i][j];
        }
        public MyMatrix(string[] data)
        {
            string[] firstRow = data[0].Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries);
            int cols = firstRow.Length;
            int rows = data.Length;

            double[,] tempMatrix = new double[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                string[] rowNums = data[i].Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries);
                if (rowNums.Length != cols)
                    throw new ArgumentException("All rows must have the same number of columns.");

                for (int j = 0; j < cols; j++)
                    tempMatrix[i, j] = double.Parse(rowNums[j]);
            }

            matrix = tempMatrix;
        }
        public MyMatrix(MyMatrix previousMatrix) : this(previousMatrix.matrix) { }
        public MyMatrix(string matrixString) : this(matrixString.Split(["\r\n", "\n", "\r"], StringSplitOptions.RemoveEmptyEntries)) { }



        public int getHeight() => Height;
        public int getWidth() => Width;

        private void ValidateIndexes(int x, int y)
        {
            if (x < 0 || x >= Height || y < 0 || y >= Width)
                throw new IndexOutOfRangeException("Matrix indices are out of range.");
        }
        public double this[int x, int y]
        {
            get
            {
                ValidateIndexes(x, y);
                return matrix[x, y];
            }
            set
            {
                ValidateIndexes(x, y);
                matrix[x, y] = value;
                cachedDeterminant = null;
            }
        }
        public double GetElement(int x, int y)
        {
            ValidateIndexes(x, y);
            return matrix[x, y];
        }
        public void SetElement(int x, int y, double value)
        {
            ValidateIndexes(x, y);
            matrix[x, y]=value;
            cachedDeterminant = null;
        }

        override public String ToString()
        {
            var sb = new StringBuilder();
            int height = getHeight();
            int width = getWidth();
            int[] colWidths = new int[width];
            for (int j = 0; j < width; j++)
            {
                int maxLen = 0;
                for (int i = 0; i < height; i++)
                {
                    int len = matrix[i, j].ToString().Length;
                    if (len > maxLen) maxLen = len;
                }
                colWidths[j] = maxLen;
            }

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    string elem = matrix[i, j].ToString().PadLeft(colWidths[j], ' ');
                    sb.Append(elem);
                    if (j < width - 1)
                        sb.Append(' ');
                }
                if (i < height - 1)
                    sb.AppendLine();
            }
            return sb.ToString();
        }
    }
}
