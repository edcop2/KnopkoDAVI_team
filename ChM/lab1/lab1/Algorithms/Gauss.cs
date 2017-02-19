using lab1.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace lab1.Algorithms
{

    public class Step
    {
        public double[][] A { get; set; }

        public double[][] P { get; set; }


        public Step(double[][] a, double[] b, double[][] p)
        {
            int n = a.Length, m = a.Length + 1;
            A = new double[n][];
            P = new double[n][];
            for (int i = 0; i < n; i++)
            {
                A[i] = new double[m];
                P[i] = new double[n];
            }
            for (int j, i = 0; i < n; i++)
            {
                for (j = 0; j < n; j++)
                {
                    A[i][j] = a[i][j];
                    P[i][j] = p[i][j];
                }
                A[i][j] = b[i];
            }
        }


        public ObservableCollection<Matrix> ToMatrix()
        {
            ObservableCollection<Matrix> temp = new ObservableCollection<Matrix>();
            for (int i = 0; i < A.Length; i++)
            {
                temp.Add(new Matrix()
                {
                    A1 = Math.Round(A[i][0], 3),
                    A2 = Math.Round(A[i][1], 3),
                    A3 = Math.Round(A[i][2], 3),
                    A4 = Math.Round(A[i][3], 3),
                    B = Math.Round(A[i][4], 3)
                });
            }
            return temp;
        }

        public ObservableCollection<Matrix> ToPMatrix()
        {
            ObservableCollection<Matrix> temp = new ObservableCollection<Matrix>();
            for (int i = 0; i < P.Length; i++)
            {
                temp.Add(new Matrix()
                {
                    A1 = P[i][0],
                    A2 = P[i][1],
                    A3 = P[i][2],
                    A4 = P[i][3]
                });
            }

            //string s = "";
            //for (int i = 0; i < P.Length; i++)
            //{
            //    for (int j = 0; j < P.Length; j++)
            //        s += P[i][j] + " ";
            //    s += "\n";
            //}
            //MessageBox.Show(s);

            return temp;
        }
    }


    public class Gauss
    {
        public double[][] A { get; set; }
        public double[] B { get; set; }

        public double[][] P { get; set; }

        public List<string> Log { get; set; }

        public List<Step> Steps { get; set; }


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
            Steps = new List<Step>();
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
            Steps.Add(new Step(A, B, P));

            string s = "";
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
                Steps.Add(new Step(A, B, P));
            }

            //s = "";
            //for (int i = 0; i < n; i++)
            //{
            //    for (int j = 0; j < n; j++)
            //        s += Math.Round(A[i][j], 3) + " ";
            //    s += ";  " + B[i];
            //    s += "\n";
            //}
            //MessageBox.Show(s);

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
