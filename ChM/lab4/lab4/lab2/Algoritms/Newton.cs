using lab2.Algorithms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace lab2.Algoritms
{
    class Newton
    {
        public PolyFunc pf { get; set; }

        public List<double> Solutions { get; set; }


        public List<string> Log { get; set; }


        public Newton()
        {
            pf = new PolyFunc();
            Solutions = new List<double>();
            Log = new List<string>();
        }

        public void Clear()
        {
            pf = new PolyFunc();
            Solutions.Clear();
            Log.Clear();
        }

        public double Calculate(double t, List<double> x, List<double> y)
        {
            double res = y[0], F, den;
            int i, j, k;
            for (i = 1; i < x.Count; i++)
            {
                F = 0;
                for (j = 0; j <= i; j++)
                {
                    den = 1;
                    for (k = 0; k <= i; k++)
                    {
                        if (k != j) den *= (x[j] - x[k]);
                    }
                    F += y[j] / den;
                }
                for (k = 0; k < i; k++) F *= (t - x[k]);
                res += F;
            }
            return res;
        }
    }
}