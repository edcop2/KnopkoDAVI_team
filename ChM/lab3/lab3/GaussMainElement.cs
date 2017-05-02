using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public static class GaussMainElement
    {

        public static double[] Solve(double[][] a, double[] b)
        {
            Matrix A = new Matrix(a);

            double[] x = new double[A.ColumnsCount];
            List<int> qList = new List<int>();
            Matrix mainRows = new Matrix(0);
            Matrix ab = A.Copy();
            ab.AddColumn(b);


            for (int i = 0; i < A.RowsCount; i++)
            {
                int p, q;
                double max = GetMaxWithIndexes(ab, out p, out q);
                qList.Add(q);
                ab.SwapRows(p, 0);

                for (int j = 1; j < ab.RowsCount; j++)
                {
                    ab.SumRows(-ab[j][q] / ab[0][q], 0, j);

                }

                mainRows.AddRow((double[])ab[0].Clone());

                ab.RemoveRow(0);
            }

            for (int i = 0; i < mainRows.RowsCount; i++)
                for (int j = 0; j < mainRows.ColumnsCount - 1; j++)
                    if (double.IsNaN(mainRows[i][j]) || double.IsInfinity(mainRows[i][j]))
                        mainRows[i][j] = 0.0;


            double[][] sys = mainRows.GetElements();
            int rows = mainRows.RowsCount;
            int cols = mainRows.ColumnsCount;

            for (int i = rows - 1; i >= 0; i--)
            {
                int q = qList[i];
                x[q] = sys[i][cols - 1] / sys[i][q];
                for (int j = 0; j < i; j++)
                {
                    sys[j][cols - 1] -= x[q] * sys[j][q];
                    // 
                }
            }

            return x;
        }

        public static string AsString(this double[] array)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in array)
                sb.Append(item + " ");
            return sb.ToString();
        }

        private static double GetMaxWithIndexes(Matrix matrix, out int row, out int column)
        {
            double[][] m = matrix.GetElements();
            double max = 0;
            row = column = 0;

            for (int i = 0; i < m.Length; i++)
                for (int j = 0; j < m[i].Length - 1; j++)
                {
                    if (Math.Abs(max) < Math.Abs(m[i][j]))
                    {
                        max = m[i][j];
                        row = i;
                        column = j;
                    }
                }
            return max;
        }



    }
}
