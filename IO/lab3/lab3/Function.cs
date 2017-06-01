using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class Function
    {
        public int Flag { get; set; } = 0;

        public double Eps { get; set; } = 0.01;


        public double F(double[] x)
        {
            if (Flag == 1)
            {
                return Math.Pow(x[0] - 1, 2) + 0.5 * Math.Pow(x[1] - 1, 2) + 2 * Math.Pow(x[2] - 1, 2) + 0.3 * Math.Pow(x[3] - 1, 2) + 0.5 * Math.Pow(x[4] - 1, 2)
                    + 0.5 * Math.Pow(x[5] - 1, 2) + Math.Pow(x[6] - 1, 2) + 0.7 * Math.Pow(x[7] - 1, 2) + 0.9 * Math.Pow(x[8] - 1, 2) + 6 * Math.Pow(x[9] - 1, 2);

            }
            else
            {
                return 5 * Math.Pow(x[0] - 3, 2) + Math.Pow(x[1] - 5, 2);
            }
        }

        public double F(Vector x)
        {
            if (Flag==0)
            {
                return Math.Pow(x[0] - 2, 4) + Math.Pow(x[0] - 2 * x[1], 2);
            }
            else if (Flag == 1)
            {
                return Math.Pow(x[0] - 1, 2) + 0.5 * Math.Pow(x[1] - 1, 2) + 2 * Math.Pow(x[2] - 1, 2) + 0.3 * Math.Pow(x[3] - 1, 2) + 0.5 * Math.Pow(x[4] - 1, 2)
                    + 0.5 * Math.Pow(x[5] - 1, 2) + Math.Pow(x[6] - 1, 2) + 0.7 * Math.Pow(x[7] - 1, 2) + 0.9 * Math.Pow(x[8] - 1, 2) + 6 * Math.Pow(x[9] - 1, 2);

            }
            else
            {
                return 5 * Math.Pow(x[0] - 3, 2) + Math.Pow(x[1] - 5, 2);
            }
        }

        public double PdF(Vector x, int n)
        {
            Vector eps = Vector.BasicVector(n, x.Length) * Eps;
            return (F(x + eps) - F(x)) / Eps;
        }
    }
}
