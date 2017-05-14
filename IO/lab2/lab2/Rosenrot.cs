using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace lab2
{
    public class Rosenrot
    {
        public double Eps { get; set; }

        public double A { get; set; }

        public double B { get; set; }

        public int It { get; set; } = 0;

        public double Step { get; set; } = 1;

        public double Shrink { get; set; } = 0.5;

        public double Extend { get; set; } = 2;

        public GoldenAxeEffect AxeEffect { get; set; } = new GoldenAxeEffect();

        public delegate double Myfunc(Vector x);

        public Vector VMin { get; set; }

        public double Min { get; set; }

        public int N { get; set; }


        Myfunc F;

        public void Calculate(double[] x0, int flag = 2, double eps = 0.001)
        {
            AxeEffect.pf.Flag = flag;
            F = AxeEffect.pf.F;
            Eps = eps;
            N = x0.Length;

            List<Vector> dVectors = new List<Vector>();
            Vector x = new Vector(x0);
            for (int i = 0; i < N; i++)
            {
                double[] t = new double[N];
                for (int j = 0; j < N; j++)
                {
                    if (i != j)
                        t[j] = 0;
                    else
                        t[j] = 1;
                }
                dVectors.Add(new Vector(t));
            }
            List<double> lambdas = new List<double>();

            for (int k = 0; ; k++)
            {
                //   Console.WriteLine("k=" + k);
                lambdas.Clear();
                for (int j = 0; j < N; j++)
                    lambdas.Add(Research(ref x, dVectors[j]));
                // Console.WriteLine("fd"+(x- new Vector(x0)));
                 // Console.WriteLine("fd");
              //  Console.WriteLine(lambdas[0] + "  " + lambdas[1] + " " + lambdas[2]);
                //Console.WriteLine("");
                //  Console.WriteLine();
                //    Console.WriteLine(lambdas[0]*dVectors[0] + "  " + lambdas[1] * dVectors[1]);
                dVectors = Gramm(dVectors, lambdas);
                //foreach (var i in dVectors)
                //    Console.WriteLine(i);
                //Console.WriteLine();
              //  Console.WriteLine(x);
                //Console.WriteLine();
                if ((lambdas.Select(e => Math.Abs(e))).Sum() < eps || k > 100)
                {
                    VMin = x;
                    Min = F(x);
                    break;
                }
            }
        }


        private List<Vector> Gramm(List<Vector> dVectors, List<double> lambdas)
        {
            int n = dVectors.Count;

            Vector[] a = new Vector[n];
            Vector[] b = new Vector[n];
            for (int j = 0; j < n; j++)
            {
                if (lambdas[j] == 0)
                    a[j] = dVectors[j].Copy();
                else
                {
                    a[j] = new Vector(n);
                    for (int i = j; i < n; i++)
                    {
                        a[j] += dVectors[i] * lambdas[i];
                    }
                }
            }
            for (int j = 0; j < n; j++)
            {
                if (j == 0)
                    b[j] = a[j].Copy();
                else
                {
                    b[j] = a[j].Copy();
                    double t = 0;
                    for (int i = 0; i < j; i++)
                    {
                        t = a[j] * b[i] / (b[i] * b[i]);
                        b[j] -= t * b[i];
                    }
                }
            }
            for (int i = 0; i < n; i++)
                dVectors[i] = b[i] / b[i].Modus();
            //Console.WriteLine("A:");
            //foreach (var i in a)
            //    Console.WriteLine(i);
            //Console.WriteLine();
            //Console.WriteLine("B:");
            //foreach (var i in b)
            //    Console.WriteLine(i);
            //Console.WriteLine();
            //Console.WriteLine("D:");
            //foreach (var i in dVectors)
            //    Console.WriteLine(i);
            return dVectors;
        }


        private double Research(ref Vector x, Vector d)
        {
            double lambda = Step;
            double k = Step;
            double q = 1;
            if (F(x + d * k) > F(x - d * k))
                q *= -1;
            double y, yt = F(x + lambda * d);
            for (int i = 0; i < 100; i++)
            {
                lambda += k * q;
                y = F(x + lambda * d);
                //      Console.WriteLine("i= " + i + "  l= " + lambda);
                //   Console.WriteLine(x + lambda * d);
                //      Console.WriteLine(lambda);
                //    Console.WriteLine(y + " " + yt);
                if (Math.Abs(yt - y) < Eps)
                {
                    // Console.WriteLine("eps");
                    break;
                }
                if (y > yt)
                {
                    k *= Shrink;
                    if (F(x + d * (lambda + k)) > F(x + d * (lambda - k)))
                    {
                        //Console.WriteLine("dif1: " + (lambda + k) + "   " + (lambda - k));
                        //Console.WriteLine("dif2: " + F(x + d * (lambda + k)) + "   " + F(x + d * (lambda - k)));
                        q = -1;
                        //  Console.WriteLine("gfdgjdf;gjs;fsdl;khjg;lsdjgf;ldajg;sldjfj;l");
                    }
                    else
                        q = 1;
                }
                yt = y;
            }
            x += lambda * d;
            //Console.WriteLine(k.ToString());
            //  Console.WriteLine(x);
            return lambda;
        }

        
    }
}
