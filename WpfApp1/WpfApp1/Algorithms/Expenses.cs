using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Algorithms
{
    public static class Expenses
    {

        public static double Calculate(double zdn1, double zdn2, double cm, double tmv, double n, 
            double[] cbj, double[] qj, double kzd, double kaa, double kcb, double kib, double kpn)
        {
            double wd = 0.4;
            double wc = 0.22;
            double wh = 0.6;
            int t = 21;
            int sm = 2;
            int km = 1;
            double kP = ((1 + wd) * (1 + wc) + wh) * (zdn1 * t + zdn2 * t) + cm + tmv * sm * km;
            double k0 = 0;
            for (int i = 0; i < n; i++)
                k0 += cbj[i] * qj[i];
            int s = 3;
            int tk = 6;
            int uk = 251;
            int fef = 2008;
            k0 *= s * tk * uk * fef;
            double kR = k0 + kzd + kaa + kcb + kib + kpn;
            return kP + kR;
        }
    }
}
