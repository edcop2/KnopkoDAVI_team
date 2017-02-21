using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1.Algorithms
{
    public class CubicRubic
    {

        PolyFunc pf = new PolyFunc();


        public double Calculate(double x0, double step, double eps1, double eps2)
        {
            if (x0 > 0)
                return double.NaN;

            if (step <= 0)
                return double.NaN;

            //Шаг 1 + 2.1
            List<double> x = new List<double>();
            x.Add(x0);
            int k = 0;
            do
            {
                if (pf.dF(x[x.Count - 1]) < 0)
                    x.Add(x[x.Count - 1] + Math.Pow(2, k) * step);

                if (pf.dF(x[x.Count - 1]) > 0)
                    x.Add(x[x.Count - 1] - Math.Pow(2, k) * step);

                k++;
            }
            while (pf.dF(x[x.Count - 2]) * pf.dF(x[x.Count - 1]) >= 0);

            //Шаг 2.2
            double x1 = x[x.Count - 2];
            double x2 = x[x.Count - 1];

            //Шаг 2.3
            double f1 = pf.F(x1);
            double f2 = pf.F(x2);
            double f1_d = pf.dF(x1);
            double f2_d = pf.dF(x2);

            double z = (3 * (f1 - f2)) / (x2 - x1) + f1_d + f2_d;

            double w = double.NaN;
            if (x1 < x2)
                w = Math.Pow(Math.Pow(z, 2) - f1_d * f2_d, 0.5);
            if (x1 > x2)
                w = -Math.Pow(Math.Pow(z, 2) - f1_d * f2_d, 0.5);

            double m = (f2_d + w - z) / (f2_d - f1_d + 2 * w);

            //Шаг 3
            double x_stationary = double.NaN;
            if (m < 0)
                x_stationary = x2;
            if (0 <= m && m <= 1)
                x_stationary = x2 - m * (x2 - x1);
            if (m > 1)
                x_stationary = x2;

            //Шаг 4
            while (pf.F(x_stationary) > pf.F(x1))
                x_stationary = x_stationary + 0.5 * (x_stationary - x1);

            //Шаг 5
            for (int c = 0; c < 500; c++)
            {
                if (pf.dF(x_stationary) <= eps1 && Math.Abs((x_stationary - x1) / x_stationary) <= eps2)
                    return x_stationary;

                if (pf.dF(x_stationary) * pf.dF(x1) < 0)
                {
                    x2 = x1;
                    x1 = x_stationary;
                }

                if (pf.dF(x_stationary) * pf.dF(x2) < 0)
                {
                    x1 = x_stationary;
                }

                //Переход на шаг 3
                x_stationary = double.NaN;
                if (m < 0)
                    x_stationary = x2;
                if (0 <= m && m <= 1)
                    x_stationary = x2 - m * (x2 - x1);
                if (m > 1)
                    x_stationary = x2;
            }

            return x_stationary;
        }
    }

}
