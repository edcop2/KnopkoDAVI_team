using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class Function
    {
        public int Flag { get; set; } = 1;


        public double F(double[] x)
        {
            switch (Flag)
            {
                case 1:
                    return 2 * x[0] * x[1] * x[2] - 4 * x[0] * x[2] - 2 * x[1] * x[2] - x[0] * x[0] + x[1] * x[1] + x[2] * x[2] - 2 * x[0] - 4 * x[1] + 4 * x[2];
                case 2:
                    return Math.Exp(x[1]) - Math.Cos(x[0] * x[0] - x[1]);
                default:
                    return double.NaN;
            }
        }
        public double F2(double[] x)
        {
            switch (Flag)
            {
                case 1:
                    return 2 * x[0] * x[1] * x[2] - 4 * x[0] * x[2] - 2 * x[1] * x[2] - x[0] * x[0] + x[1] * x[1] + x[2] * x[2] - 2 * x[0] - 4 * x[1] + 4 * x[2];
                case 2:
                    return Math.Exp(x[1]) - Math.Cos(x[0] * x[0] - x[2]);
                default:
                    return double.NaN;
            }
        }

    }
}
