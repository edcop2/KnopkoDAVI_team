using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace lab3
{
    public class Krilov
    {
        public double[][] A { get; set; }
        public double[] B { get; set; }

        public int N { get; set; }
        public int M { get; set; }

        public List<string> Log { get; set; }

        public double[] Res { get; set; }

        public Krilov()
        {
            Log = new List<string>();
        }

        public void SetAB(DataArray a)
        {
            N = a.N;
            M = a.M;
            A = new double[N][];
            B = new double[] { 1, 0, 0 };
            Res = new double[N];
            for (int i = 0; i < N; i++)
            {
                A[i] = new double[M];
                for (int j = 0; j < M; j++)
                    A[i][j] = a[i][j];
            }
        }


        public void Calculate()
        {
            List<double[]> ys = new List<double[]>();
            ys.Add(B);
            for (int i = 0; i < N; i++)
            {
                ys.Add(Multiply(ys[i]));
            }

            double[][] m = new double[N][];
            double[] b = ys[ys.Count - 1].Select(e => e * -1).ToArray();
            for (int i = 0; i < N; i++)
            {
                m[i] = new double[N];
                for (int j = 0; j < N; j++)
                {
                    m[i][j] = ys[ys.Count - 2 - j][i];
                }
            }
            //string s = "";
            //foreach (var i in ys)
            //{
            //    s += string.Format("{0} {1} {2}", i[0], i[1], i[2]) + "\n";
            //}
            ////messagebox.show(s);
            //s += "\n\n";
            //for (int i = 0; i < N; i++)
            //{
            //    for (int j = 0; j < N; j++)
            //        s += m[i][j] + "p" + (j + 1) + " ";
            //    s += " = " + b[i] + "\n";
            //}
            Gauss ga = new Gauss(N);
            ga.A = m;
            ga.B = b;
            Res = ga.Calculate();
            //s += "\n\n";
            //s += string.Format("{0} {1} {2}",Res[0], Res[1], Res[2]);
            //   MessageBox.Show(s);
        }

        private double[] Multiply(double[] y)
        {
            double[] newY = new double[N];

            for (int i = 0; i < N; i++)
            {
                newY[i] = 0;
                for (int j = 0; j < N; j++)
                    newY[i] += A[i][j] * y[j];
            }

            return newY;
        }

        public string GetKhaEqu()
        {
            string s = "";
            s += "Характериситическое уравнение: \n λ^3 ";
            for (int i = 0, j = 2; i < N; i++, j--)
            {
                if (j >1)
                {
                    if (Res[i] < 0)
                    {
                        s += string.Format("- {0}*λ^{1} ", Math.Abs(Res[i]), j);
                    }
                    else
                    {
                        s += string.Format("+ {0}*λ^{1} ", Res[i], j);
                    }
                }
                else if (j==1)
                {
                    if (Res[i] < 0)
                    {
                        s += string.Format("- {0}*λ ", Math.Abs(Res[i]));
                    }
                    else
                    {
                        s += string.Format("+ {0}*λ ", Res[i]);
                    }
                }
                else
                {
                    if (Res[i] < 0)
                    {
                        s += string.Format("- {0} ", Math.Abs(Res[i]));
                    }
                    else
                    {
                        s += string.Format("+ {0} ", Res[i]);
                    }
                }
            }
            s += "= 0";

            return s;
        }





    }
}
