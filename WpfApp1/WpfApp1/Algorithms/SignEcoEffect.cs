using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Algorithms
{
    public static class SignEcoEffect
    {
        public static double Calculate(double netCost1, double netCost2, double implementCost1, double implementCost2)
        {
            int n = 1;
            double ak = 1.6, en = 0.33;

            var z1 = netCost1 + en * implementCost1;
            var z2 = netCost2 + en * implementCost2;
            var e = (z1 * ak - z2) * n;
            var tok = z1 / e;

            return 1 / tok;
        }
    }
}
