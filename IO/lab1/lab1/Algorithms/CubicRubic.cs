using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;



namespace lab1.Algorithms
{
    public class CubicRubic
    {

        public PolyFunc pf { get; set; }
        public List<double> Solutions { get; set; }

        public int It { get; set; }

        private int k = 0;

        public CubicRubic()
        {
            pf = new PolyFunc();
            Solutions = new List<double>();
        }

        public void Clear()
        {
            pf = new PolyFunc();
            Solutions.Clear();
        }





        public double Calculate(double x0, double step, double eps1, double eps2, double a, double b)
        {
            if (step <= 0)
                return double.NaN;

            It = 0;
            
            List <double> x = new List<double>();
            x.Add(x0);
            int k = 0;
            do
            {
                if (pf.dF(x[x.Count - 1]) < 0)
                    x.Add(x[x.Count - 1] + Math.Pow(2, k) * step);

                if (pf.dF(x[x.Count - 1]) > 0)
                    x.Add(x[x.Count - 1] - Math.Pow(2, k) * step);

                if (x[x.Count - 1] <= a )
                    return a;
                if ( x[x.Count - 1] >= b)
                    return b;

                k++;
                It++;
            }
            while (pf.dF(x[x.Count - 2]) * pf.dF(x[x.Count - 1]) >= 0 );

            
            double x1 = x[x.Count - 2];
            double x2 = x[x.Count - 1];
            
            double f1 = pf.F(x1);
            double f2 = pf.F(x2);
            double df1 = pf.dF(x1);
            double df2 = pf.dF(x2);

            double z = (3 * (f1 - f2)) / (x2 - x1) + df1 + df2;

            double w = double.NaN;
            if (x1 < x2)
                w = Math.Pow(Math.Pow(z, 2) - df1 * df2, 0.5);
            if (x1 > x2)
                w = -Math.Pow(Math.Pow(z, 2) - df1 * df2, 0.5);

            double m = (df2 + w - z) / (df2 - df1 + 2 * w);
            
            double xStat = double.NaN;
            if (m < 0)
                xStat = x2;
            if (0 <= m && m <= 1)
                xStat = x2 - m * (x2 - x1);
            if (m > 1)
                xStat = x2;

            
            while (pf.F(xStat) > pf.F(x1))
            {
                xStat = xStat + 0.5 * (xStat - x1);
                It++;
            }            
            
            for (int c = 0; c < 500; c++)
            {
                if (pf.dF(xStat) <= eps1 && Math.Abs((xStat - x1) / xStat) <= eps2)
                    return xStat;
                if (x[x.Count - 1] <= a)
                    return a;
                if (x[x.Count - 1] >= b)
                    return b;

                if (pf.dF(xStat) * pf.dF(x1) < 0)
                {
                    x2 = x1;
                    x1 = xStat;
                }

                if (pf.dF(xStat) * pf.dF(x2) < 0)
                {
                    x1 = xStat;
                }

                xStat = double.NaN;
                if (m < 0)
                    xStat = x2;
                if (0 <= m && m <= 1)
                    xStat = x2 - m * (x2 - x1);
                if (m > 1)
                    xStat = x2;
                It++;
            }

            return xStat;
        }
    }

}
