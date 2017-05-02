using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static lab3.Utils;

namespace lab3
{
    public class KhaEqu
    {
        public double[][] A { get; set; }
        public double[] B { get; set; }

        public double[] P { get; set; }
        public double[] Res { get; set; }
        public double[] Lambda { get; set; }

        public int N { get; set; }
        public int M { get; set; }

        public string Equ { get; set; }

        public Matrix OwnMatrix { get; set; }

        public List<string> Log { get; set; }

        public KhaEqu()
        {
            Log = new List<string>();
        }

        public void SetAB(DataArray a)
        {
            N = a.N;
            M = a.M;
            A = new double[N][];
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
            Lambda = GetRootsOfCubicEquations(P[0], P[1], P[2]).ToArray();


            List<double> roots = new CharactericEqual(P.ToList()).GetRoots();
            Matrix a = new Matrix(A);
            OwnMatrix = Utils.GetOwnVectors(a, roots.ToArray());

            //MessageBox.Show(q.ToString());


            //List<double[]> es = new List<double[]>();
            //double[][] at;
            //string s = "";
            //foreach (var i in Lambda)
            //    s += i + "  ";
            //s += "\n\n";
            //Gauss ga;
            //for (int k = 0; k < N; k++)
            //{
            //    InitArray(out at, Lambda[k]);
            //    for (int i = 0; i < N; i++)
            //    {
            //        for (int j = 0; j < N; j++)
            //            s += at[i][j] + " ";
            //        s += "\n";
            //    }
            //    s += "\n\n";
            //    ga = new Gauss(N);
            //    ga.A = at;
            //    ga.B = new double[] { 1, 0, 0 };
            //    double[] res = ga.Calculate().Select(e => Math.Round(e, 2)).ToArray();
            //    MessageBox.Show(res[0] + " " + res[1] + " " + res[2]);
            //    es.Add(res);
            //}
            //for (int i = 0; i < N; i++)
            //{
            //    for (int j = 0; j < N; j++)
            //        s += es[j][i] + " ";
            //    s += "\n";
            //}
            //MessageBox.Show(s);
        }
        

        private void InitArray(out double[][] at, double lambda)
        {
            at = new double[N][];
            for (int i = 0; i < N; i++)
            {
                at[i] = new double[N];
                for (int j = 0; j < N; j++)
                {
                    at[i][j] = A[i][j];
                    if (i == j)
                        at[j][j] -= lambda;
                }
            }
        }


        private static List<double> GetRootsOfCubicEquations(double a, double b, double c)
        {
            var q = (Math.Pow(a, 2) - 3 * b) / 9;
            var r = (2 * Math.Pow(a, 3) - 9 * a * b + 27 * c) / 54;

            if (Math.Pow(r, 2) < Math.Pow(q, 3))
            {
                var t = Math.Acos(r / Math.Sqrt(Math.Pow(q, 3))) / 3;
                var x1 = -2 * Math.Sqrt(q) * Math.Cos(t) - a / 3;
                var x2 = -2 * Math.Sqrt(q) * Math.Cos(t + (2 * Math.PI / 3)) - a / 3;
                var x3 = -2 * Math.Sqrt(q) * Math.Cos(t - (2 * Math.PI / 3)) - a / 3;
                return new List<double> { Math.Round(x1, 2), Math.Round(x2, 2), Math.Round(x3, 2) };
            }
            else
            {
                var A = -Math.Sign(r) * Math.Pow(Math.Abs(r) + Math.Sqrt(Math.Pow(r, 2) - Math.Pow(q, 3)), (1.0 / 3.0));
                var B = (A == 0) ? 0.0 : q / A;

                var x1 = (A + B) - a / 3;

                if (A == B)
                {
                    var x2 = -A - a / 3;
                    return new List<double> { Math.Abs(x1), Math.Abs(x2) };
                }
                return new List<double> { Math.Abs(x1) };
            }
        }




    }
}
