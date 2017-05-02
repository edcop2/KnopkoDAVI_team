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

        public double[] VMin { get; set; }

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

            for (int k = 0; k < 1; k++)
            {
                for (int j = 0; j < N; j++)
                    lambdas.Add(Research(ref x, dVectors[j]));
               // Console.WriteLine("fd"+(x- new Vector(x0)));
              //  Console.WriteLine(lambdas[0] + "  " + lambdas[1]);
              //  Console.WriteLine();
                //    Console.WriteLine(lambdas[0]*dVectors[0] + "  " + lambdas[1] * dVectors[1]);
                dVectors = Gramm(dVectors, lambdas);
            }
        }

        private List<Vector> Gramm(List<Vector> dVectors, List<double> lambdas)
        {
            int n = dVectors.Count;
            //   List<Vector>

            Vector[] a = new Vector[n];
            Vector[] b = new Vector[n];
            for (int j = 0; j < n; j++)
            {
                if (lambdas[j] == 0)
                    a[j] = dVectors[j];
                else
                    a[j] = dVectors[j] * lambdas[j];
                if (j == 0)
                    b[j] = a[j];
                else
                {
                    b[j] = a[j];
                    double t = 0;
                    for (int i = 0; i < j - 1; i++)
                    {
                        t += a[j][i] * dVectors[j][i];
                    }
                    b[j] -= dVectors[j] * t;
                }
            }
            foreach (var i in a)
                Console.WriteLine(i);
            Console.WriteLine();
            foreach (var i in b)
                Console.WriteLine(i);
            return null;
        }


        private double Research(ref Vector x, Vector d)
        {
            double lambda = Step;
            double k = Step;
            double q = 1;
            if (F(x + d * k) > F(x - d * k))
                q *= -1;
            double y, yt = F(x + lambda * d);
            for (int i = 0; ; i++)
            {
                lambda += k * q;
                y = F(x + lambda * d);
                //   Console.WriteLine("i= " + i + "  l= " + lambda);
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
            //  Console.WriteLine(x);
            return lambda;
        }



        //private double RosesAreRedVioletsAreBlueEverybodyLovesIOFckU2(double[] x)
        //{
        //    int xc = x.Length;
        //  //  double xmin = F(x), xt;
        //    double xd;
        //    for (int i = 0; ; i++)
        //    {
        //        xt = xmin;
        //        for (int xi = 0; xi < xc; xi++)
        //            x[xi] = AxeEffect.Calculate(A, B, Eps, x, xi);
        //        xmin = F(x);
        //        xd = Math.Abs(xt - xmin);
        //        if (xd <= Eps)
        //        {
        //            It = i + 1;
        //            VMin = x;
        //            return xmin;
        //        }
        //    }

        //    return xmin;
        //}

    }
}
