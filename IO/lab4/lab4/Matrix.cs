using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class Matrix
    {
        private double[][] matrix;


        public Matrix(int n) : this(n, n)
        {
        }

        public Matrix(int n, int m)
        {
            this.matrix = new double[n][];
            for (int i = 0; i < n; i++)
                matrix[i] = new double[m];
        }

        public Matrix(double[][] matrix)
        {
            this.matrix = matrix;
        }

        public static Matrix GetE(int dim)
        {
            Matrix e = new Matrix(dim);
            for (int i = 0; i < dim; i++)
                e[i][i] = 1;
            return e;
        }

        public double[] this[int index]
        {
            get { return matrix[index]; }
        }

        public int RowsCount
        {
            get { return matrix.Length; }
        }

        public int ColumnsCount
        {
            get { return matrix[0].Length; }
        }

        public double GetDeterminant()
        {
            throw new NotImplementedException();
        }

        public double[] GetRow(int i)
        {
            return matrix[i];
        }

        public double Get(int i, int j)
        {
            return matrix[i][j];
        }

        public void Set(int i, int j, double value)
        {
            matrix[i][j] = value;
        }

        public void SwapRows(int i, int j)
        {
            double[] term = matrix[i];
            matrix[i] = matrix[j];
            matrix[j] = term;
        }

        public void SwapColumns(int i, int j)
        {
            int m = RowsCount;
            for (int line = 0; i < m; i++)
            {
                double term = matrix[line][i];
                matrix[line][i] = matrix[line][j];
                matrix[line][j] = term;
            }
        }

        public double[][] GetElements()
        {
            return matrix;
        }

        public Matrix GetT()
        {
            Matrix t = new Matrix(ColumnsCount, RowsCount);
            for (int i = 0; i < RowsCount; i++)
            {
                for (int j = 0; j < ColumnsCount; j++)
                {
                    t[j][i] = this[i][j];
                }
            }
            return t;
        }

        public bool ValidateRow(int row)
        {
            return Math.Abs(matrix[row][row]) > GetRowSum(row);
        }

        public double GetRowSum(int row)
        {

            double[] @string = matrix[row];
            double sum = 0;

            for (int i = 0; i < @string.Length; i++)
            {
                if (i != row)
                {
                    sum += Math.Abs(@string[i]);
                }
            }

            return sum;
        }

        public bool ValidateNorm()
        {
            int m = RowsCount;
            double[] linesSum = new double[m];
            for (int i = 0; i < m; i++)
            {
                linesSum[i] = GetRowSum(i);
            }
            return Max(linesSum) < 1;
        }

        public double Max(params double[] elements)
        {
            double maxEl = elements[0];
            foreach (double el in elements)
            {
                maxEl = el > maxEl ? el : maxEl;
            }
            return maxEl;
        }

        public void AddColumn(double[] column)
        {
            for (int i = 0; i < column.Length; i++)
            {
                int prevSize = matrix[i].Length;
                Array.Resize(ref matrix[i], prevSize + 1);
                matrix[i][prevSize] = column[i];
            }
        }

        public void AddRow(double[] row)
        {
            int prevSize = matrix.Length;
            Array.Resize(ref matrix, prevSize + 1);
            matrix[prevSize] = row;
        }

        public void RemoveColumn(int column)
        {
            int m = RowsCount;
            int n = ColumnsCount;
            for (int i = 0; i < m; i++)
            {
                for (int j = column; j < n - 1; j++)
                    matrix[i][j] = matrix[i][j + 1];
                Array.Resize(ref matrix[i], n - 1);
            }
        }

        public void RemoveRow(int row)
        {
            int m = RowsCount;
            for (int i = row; i < m - 1; i++)
                matrix[i] = (double[])matrix[i + 1].Clone();
            Array.Resize(ref matrix, m - 1);
        }

        public void MultipleRow(double mul, int row)
        {
            int n = ColumnsCount;
            for (int j = 0; j < n; j++)
                matrix[row][j] = mul * matrix[row][j];
        }

        public void SumRows(double mul1, int row1, int row2)
        {
            int n = ColumnsCount;
            for (int i = 0; i < n; i++)
                matrix[row2][i] += mul1 * matrix[row1][i];
        }

        public Matrix Copy()
        {
            int m = RowsCount;
            double[][] clone = new double[m][];
            for (int i = 0; i < m; i++)
                clone[i] = (double[])matrix[i].Clone();
            return new Matrix(clone);
        }

        public double Max()
        {
            double max = matrix[0][0];
            for (int i = 0; i < matrix.Length; i++)
                for (int j = 0; j < matrix[i].Length; j++)
                    max = max > matrix[i][j] ? max : matrix[i][j];
            return max;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (double.IsInfinity(matrix[i][j]) || double.IsNaN(matrix[i][j]))
                        sb.AppendFormat("{0}  ", new String('_', 6));
                    else
                        sb.AppendFormat("{0:0.000}  ", matrix[i][j]);
                }
                sb.AppendLine();
            }
            return sb.ToString();
        }

        public double GetTrace()
        {
            if (RowsCount != ColumnsCount)
                throw new InvalidOperationException();
            double tr = 0;
            for (int i = 0; i < RowsCount; i++)
                tr += this[i][i];
            return tr;
        }

        public static Matrix operator +(Matrix a, Matrix b)
        {
            if (a.RowsCount != b.RowsCount || a.ColumnsCount != b.ColumnsCount)
                throw new ArgumentException();
            Matrix res = new Matrix(a.RowsCount, a.ColumnsCount);
            int m = a.RowsCount, n = a.ColumnsCount;
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    res[i][j] = a[i][j] + b[i][j];
            return res;
        }

        public static Matrix operator -(Matrix a, Matrix b)
        {
            if (a.RowsCount != b.RowsCount || a.ColumnsCount != b.ColumnsCount)
                throw new ArgumentException();
            Matrix res = new Matrix(a.RowsCount, a.ColumnsCount);
            int m = a.RowsCount, n = a.ColumnsCount;
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    res[i][j] = a[i][j] - b[i][j];
            return res;
        }

        public static Matrix operator -(Matrix a)
        {
            int m = a.RowsCount, n = a.ColumnsCount;
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    a[i][j] *= -1;
            return a;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            if (a.ColumnsCount != b.RowsCount)
                throw new ArgumentException();
            int m = a.RowsCount, n = b.ColumnsCount, inner = a.ColumnsCount;
            Matrix c = new Matrix(a.RowsCount, b.ColumnsCount);
            for (int row = 0; row < m; row++)
                for (int col = 0; col < n; col++)
                    for (int i = 0; i < inner; i++)
                        c[row][col] += a[row][i] * b[i][col];
            return c;
        }

        public static Matrix operator *(Matrix a, double n)
        {
            Matrix res = new Matrix(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
                for (int j = 0; j < a.ColumnsCount; j++)
                    res[i][j] = a[i][j] * n;
            return res;
        }

        public static Matrix operator *(double n, Matrix a)
        {
            Matrix res = new Matrix(a.RowsCount, a.ColumnsCount);
            for (int i = 0; i < a.RowsCount; i++)
                for (int j = 0; j < a.ColumnsCount; j++)
                    res[i][j] = a[i][j] * n;
            return res;
        }

        public static double[] operator *(Matrix a, double[] vectorT)
        {
            if (a[0].Length != vectorT.Length)
                throw new ArgumentException();
            double[] res = new double[a.RowsCount];
            for (int row = 0; row < res.Length; row++)
                for (int i = 0; i < a.ColumnsCount; i++)
                    res[row] += a[row][i] * vectorT[i];
            return res;
        }

        public double[] GetColumn(int index)
        {
            double[] res = new double[RowsCount];
            for (int i = 0; i < RowsCount; i++)
            {
                res[i] = matrix[i][index];
            }
            return res;
        }


        private Matrix _inv = null;

        public Matrix GetInverseMatrix
        {
            get
            {
                return _inv ?? Inverse(this);
            }
        }


        public Matrix Inverse(Matrix mA)
        {
            if (mA.RowsCount != mA.ColumnsCount)
                throw new ArgumentException("Матрица не квадратн");
            Matrix inv = new Matrix(mA.ColumnsCount);
            double det = mA.Determinant;
            //xCsssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssssonsole.WriteLine(Math.Round(det,3));

            if (det == 0)
                return null;
            for (int i = 0; i < mA.RowsCount; i++)
            {
                for (int t = 0; t < mA.ColumnsCount; t++)
                {
                    Matrix tmp = mA.Exclude(i, t);
                    inv[t][i] = (1 / det) * Math.Pow(-1, i + t) * tmp.Determinant;
                }
            }
            return inv;
        }

        public Matrix Exclude(int i, int j)
        {
            Matrix newM = new Matrix(matrix.Length - 1);
            for (int i1 = 0, i2 = 0; i2 < newM.RowsCount; i1++)
            {
                if (i1 == i)
                    continue;
                for (int j1 = 0, j2 = 0; j2 < newM.ColumnsCount; j1++)
                {
                    if (j1 == j)
                        continue;
                    newM[i2][j2] = matrix[i1][j1];
                    j2++;
                }
                i2++;
            }
            return newM;
        }

        private double? _det = null;

        public double Determinant
        {
            get
            {
                return _det ?? Determ(matrix);
            }
        }


        public static double Determ(double[][] matrix)
        {
            if (matrix.Length != matrix[0].Length)
                throw new Exception(" Число строк в матрице не совпадает с числом столбцов");
            double det = 0;

            int Rank = matrix.Length;
            if (Rank == 1)
                det = matrix[0][0];
            if (Rank == 2)
                det = matrix[0][0] * matrix[1][1] - matrix[0][1] * matrix[1][0];
            if (Rank > 2)
            {
                det = 1;
                for (int j = 0; j < matrix.Length; j++)
                {
                    det *= matrix[j][j];
                }
            }
            return det;
        }

        public Matrix Transpose()
        {
            Matrix tM = new Matrix(ColumnsCount, RowsCount);
            for (int i = 0; i < ColumnsCount; i++)
            {
                for (int j = 0; j < RowsCount; j++)
                    tM[j][i] = matrix[i][j];
            }
            return tM;
        }

        public static double[][] GetMinor(double[][] matrix, int row, int column)
        {
            if (matrix.Length != matrix[0].Length)
                throw new Exception(" Число строк в матрице не совпадает с числом столбцов");
            double[][] buf = new double[matrix.Length - 1][];
            for (int i = 0; i < matrix.Length - 1; i++)
                buf[i] = new double[matrix.Length - 1];
            for (int i = 0; i < matrix.Length - 1; i++)
            {
                for (int j = 0; j < matrix.Length; j++)
                {
                    if ((i != row) || (j != column))
                    {
                        if (i > row && j < column)
                            buf[i - 1][j] = matrix[i][j];
                        if (i < row && j > column)
                            buf[i][j - 1] = matrix[i][j];
                        if (i > row && j > column)
                            buf[i - 1][j - 1] = matrix[i][j];
                        if (i < row && j < column)
                            buf[i][j] = matrix[i][j];
                    }
                }
            }
            return buf;
        }

        public static Matrix GetIdentityMatrix(int n)
        {
            Matrix im = new Matrix(n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j)
                        im[i][j] = 1;
                    else
                        im[i][j] = 0;
                }
            }
            return im;
        }


        public static Matrix IdentityMatrix
        {
            get
            {
                return new Matrix(new double[][] { new double[] { 1, 0, 0 }, new double[] { 0, 1, 0 }, new double[] { 0, 0, 1 } });
            }
        }

    }
}
