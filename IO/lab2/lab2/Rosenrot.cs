using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public delegate double Myfunc(double[] x);

        public double[] VMin { get; set; }

        public double Min { get; set; }

        Myfunc F;

        public void Calculate(double[] x, int flag = 1, double eps = 0.01)
        {
            AxeEffect.pf.Flag = flag;
            F = AxeEffect.pf.F;
            Eps = eps;

            Min = RosesAreRedVioletsAreBlueEverybodyLovesIOFckU2(x);

        }


        private void Research(double[] x)
        {
            int j;
            double newValue;


        }

        private double RosesAreRedVioletsAreBlueEverybodyLovesIOFckU2(double[] x)
        {
            int xc = x.Length;
            double xmin = F(x), xt;
            double xd;
            for (int i = 0; ; i++)
            {
                xt = xmin;
                for (int xi = 0; xi < xc; xi++)
                    x[xi] = AxeEffect.Calculate(A, B, Eps, x, xi);
                xmin = F(x);
                xd = Math.Abs(xt - xmin);
                if (xd <= Eps)
                {
                    It = i + 1;
                    VMin = x;
                    return xmin;
                }
            }
            return xmin;
        }

    }
}
