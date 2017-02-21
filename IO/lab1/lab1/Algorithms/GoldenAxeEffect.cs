using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1.Algorithms
{
    public class GoldenAxeEffect
    {

        public PolyFunc pf = new PolyFunc();


        public  double Calculate(double a, double b, bool is_min, double e)
        {
            if (e <= 0)
                return double.NaN;

            if (a >= b)
                return double.NaN;

            double GS_proportion = (1 + Math.Sqrt(5)) / 2;

            while (Math.Abs(b - a) > e)
            {
                double x1 = b - (b - a) / GS_proportion;
                double x2 = a + (b - a) / GS_proportion;

                double y1 = pf.F(x1);
                double y2 = pf.F(x2);

                if (is_min == true && y1 >= y2 || is_min == false && y1 <= y2)
                {
                    a = x1;
                    x1 = x2;
                    x2 = a + (b - a) / GS_proportion;
                }
                else
                {
                    b = x2;
                    x2 = x1;
                    x1 = b - (b - a) / GS_proportion;
                }
            }

            return (a + b) / 2;
        }
    }
}
