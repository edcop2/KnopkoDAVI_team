using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;

namespace lab2
{
    public class GoldenAxeEffect
    {

        public Function pf { get; set; }


        public int It { get; set; }

        public GoldenAxeEffect()
        {
            pf = new Function();
        }


        public double Calculate(double a, double b, double eps, double[]x, int xt)
        {
            if (eps <= 0)
                return double.NaN;

            if (a >= b)
                return double.NaN;

            double GProp = (1 + Math.Sqrt(5)) / 2;
            double x1, x2;
            double[] v1 = new double[x.Length], v2 = new double[x.Length];
            It = 0;
            while (Math.Abs(b - a) > eps)
            {

                It++;
                x1 = b - (b - a) / GProp;
                x2 = a + (b - a) / GProp;

                for (int i=0; i<x.Length; i++)
                {
                    if (i!=xt)
                    {
                        v1[i] = x[i];
                        v2[i] = x[i];
                    }
                    else
                    {
                        v1[i] = x1;
                        v2[i] = x2;
                    }
                }
                

                double y1 = pf.F(v1);
                double y2 = pf.F(v2);
                //  Console.WriteLine(Math.Abs(b - a) + "; " + a + "; " + b);
                //      Thread.Sleep(20);
                if (y1 >= y2)
                {
                    a = x1;
                    x1 = x2;
                    x2 = a + (b - a) / GProp;
                }
                else
                {
                    b = x2;
                    x2 = x1;
                    x1 = b - (b - a) / GProp;
                }
            }
            return (a + b) / 2;
        }
    }
}
