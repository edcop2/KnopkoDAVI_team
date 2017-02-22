using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace lab1.Algorithms
{
    public class GoldenAxeEffect
    {

        public PolyFunc pf { get; set; }

        public List<double> Solutions { get; set; }


        public GoldenAxeEffect()
        {
            pf = new PolyFunc();
            Solutions = new List<double>();
        }

        public void Clear()
        {
            pf = new PolyFunc();
            Solutions.Clear();
        }


        public void ManyCalculate(double a, double b, double eps)
        {
            bool isMin = false;
            double sol;
            double _a = a, _b = b;

            sol = Math.Round(Calculate(_a, _b, !isMin, eps), eps.ToString().Length - 2);
            if (!Solutions.Contains(sol) && Math.Abs(sol - a) > 2 * eps && Math.Abs(sol - b) > 2 * eps)
            {
                Solutions.Add(sol);
                ManyCalculate(sol, b, eps);
                ManyCalculate(a, sol, eps);
            }
            sol = Math.Round(Calculate(_a, _b, isMin, eps), eps.ToString().Length - 2);
            if (!Solutions.Contains(sol) && Math.Abs(sol - a) > 2 * eps && Math.Abs(sol - b) > 2 * eps)
            {
                Solutions.Add(sol);
                ManyCalculate(sol, b, eps);
                ManyCalculate(a, sol, eps);
            }
        }





        public double Calculate(double a, double b, bool isMin, double eps)
        {
            if (eps <= 0)
                return double.NaN;

            if (a >= b)
                return double.NaN;

            double GProp = (1 + Math.Sqrt(5)) / 2;

            while (Math.Abs(b - a) > eps)
            {
                double x1 = b - (b - a) / GProp;
                double x2 = a + (b - a) / GProp;

                double y1 = pf.F(x1);
                double y2 = pf.F(x2);

                if (isMin == true && y1 >= y2 || isMin == false && y1 <= y2)
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
