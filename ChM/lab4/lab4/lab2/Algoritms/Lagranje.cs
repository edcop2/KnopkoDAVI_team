using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace lab2.Algorithms
{
    public class DihMethod
    {

        public PolyFunc pf { get; set; }

        public List<double> Solutions { get; set; }

        public List<string> Log { get; set; }


        public DihMethod()
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

        public void Calculate(double x, double[] x_values, double[] y_values)
        {
            double lagrange_pol = 0;
            double basics_pol;

            for (int i = 0; i < x_values.Length-1; i++)
            {
                basics_pol = 1;
                for (int j = 0; j < y_values.Length-1; j++)
                {
                    if (j == i) continue;
                    basics_pol *= (x - x_values[j]) / (x_values[i] - x_values[j]);
                }
                lagrange_pol += basics_pol * y_values[i];
            }
            Solutions.Add(lagrange_pol);
        }
    }
}
