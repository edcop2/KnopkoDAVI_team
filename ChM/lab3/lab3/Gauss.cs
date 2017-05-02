using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class Gauss
    {

        public double[][] A { get; set; }
        public double[] B { get; set; }

        public double[][] P { get; set; }

        public List<string> Log { get; set; }


        public Gauss(int n)
        {
            A = new double[n][];
            P = new double[n][];
            for (int i = 0; i < n; i++)
            {
                A[i] = new double[n];
                P[i] = new double[n];
            }
            B = new double[n];
            Log = new List<string>();
        }

        public double[] Calculate()
        {
            int n = A.Length;
            double[] res = new double[n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                        P[i][j] = 0;
                    else
                        P[i][j] = 1;
                }
            }

            int k;
            for (int i = 0; i < n - 1; i++)
            {
                k = i;
                for (int j = i + 1; j < n; j++)
                    if (Math.Abs(A[j][i]) > Math.Abs(A[k][i]))
                        k = j;
                if (k != i)
                {
                    Swarp(i, k);
                }

                double me;
                for (int j = i + 1; j < n; j++)
                {
                    me = A[j][i] / A[i][i];
                    for (int q = i; q < n; q++)
                        A[j][q] -= A[i][q] * me;
                    B[j] -= B[i] * me;
                }
            }


            for (int i = n - 1; i >= 0; i--)
            {
                res[i] = B[i];

                for (int j = n - 1; j > i; j--)
                    res[i] -= A[i][j] * res[j];
                res[i] /= A[i][i];
            }

            return res;
        }

        private void Swarp(int i, int j)
        {
            double[] t = A[i];
            A[i] = A[j];
            A[j] = t;
            t = P[i];
            P[i] = P[j];
            P[j] = t;
            double tt = B[i];
            B[i] = B[j];
            B[j] = tt;

        }

    }
}
