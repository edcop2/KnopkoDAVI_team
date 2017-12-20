using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Algorithms
{
    public static class Costs
    {
        public static double Calculate(int empCount, double[] timeT, double[] timeZ, int eqCount,
            double[] yearNorm, double[] price, double[] count, double[] workTime, double[] power)
        {
            double wd = 0.4, wc = 0.2, ta = 0.57, cp = 0.05, zn = 0.2, zzp = 0, sa = 0, za = 0, srem = 0;
            int dp = 251, na = 8, zm = 150;
            var faf = dp * na;
            for (int i = 0; i < empCount; i++)
                zzp += timeT[i] * timeZ[i] * (1 + wd) * (1 + wc);
            for (int i = 0; i < eqCount; i++)
            {
                sa += yearNorm[i] * price[i] * count[i] * workTime[i] / faf;
                za += power[i] * workTime[i] * count[i] * ta;
                srem += cp * price[i] * workTime[i] / faf;
            }
            return (zzp + sa + za + srem + zm) * zn;
        }
    }
}