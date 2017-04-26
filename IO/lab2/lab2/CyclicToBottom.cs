using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    class CyclicToBottom
    {
        public double Eps { get; set; }

        public double A { get; set; }

        public double B { get; set; }

        public int It { get; set; } = 0;

        public GoldenAxeEffect AxeEffect { get; set; } = new GoldenAxeEffect();

        public delegate double Myfunc(double[] x);

        public double[] VMin { get; set; }

        public double Min { get; set; }

        Myfunc F;



        public void Calculate(double[] x, int flag = 1, double eps = 0.01)
        {
            AxeEffect.pf.Flag = flag;
            F = AxeEffect.pf.F;
            Eps = eps;

            Min = Descend(x);

        }

        private double Descend(double[] x)
        {
            int xc = x.Length;
            double xmin = F(x), xt;
            double xd;
            for (int i = 0; ; i++)
            {
                //  Console.WriteLine(i);
                xt = xmin;
                for (int xi = 0; xi < xc; xi++)
                    x[xi] = AxeEffect.Calculate(A, B, Eps, x, xi);
                //     Console.WriteLine(i+"zsd");
                xmin = F(x);
                xd = Math.Abs(xt - xmin);
                if (xd <= Eps)
                {
                    It = i + 1;
                    VMin = x;
                    return xmin;
                }
            }
        }
    }
}
