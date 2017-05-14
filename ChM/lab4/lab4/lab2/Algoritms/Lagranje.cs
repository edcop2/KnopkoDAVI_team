using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace lab2.Algorithms
{
    public class Lagranje
    {

        public PolyFunc pf { get; set; }

        public List<double> Solutions { get; set; }

        public List<string> Log { get; set; }

        public List<List<double>> Dels { get; set; }


        public Lagranje()
        {
            pf = new PolyFunc();
            Solutions = new List<double>();
            Log = new List<string>();
            Dels = new List<List<double>>();
        }

        public void Clear()
        {
            pf = new PolyFunc();
            Solutions.Clear();
            Log.Clear();
            Dels.Clear();
        }


        private void CalculateDels(List<double> x_values, List<double> y_values)
        {
            List<double> temp = new List<double>();
            for (int i = 0; i < y_values.Count; i++)
            {
                temp.Add(y_values[i]);
            }
            Dels.Add(temp);

            double t;


            for (int j = 0; j < y_values.Count - 1; j++)
            {
                temp = new List<double>();
                for (int i = 1; i < Dels[j].Count; i++)
                {
                    t = Dels[j][i] - Dels[j][i - 1];
                    temp.Add(t);
                    MessageBox.Show(t.ToString());
                }
                Dels.Add(temp);
            }
            //string s ="";
            //foreach (var i in Dels)
            //{
            //    foreach (var j in i)
            //        s += j + " ";
            //    s += "\n";
            //}
            //MessageBox.Show(s);
        }

        private double Fact(double x)
        {
            if (x == 0)
                return 1;
            else
                return x * Fact(x - 1);
        }


        public double Calculate(double x, List<double> x_values, List<double> y_values)
        {
            double lagrange_pol = 0;
            double basics_pol;

            //MessageBox.Show("hu");
            int x0 = x_values.Count / 2;
            if (Dels.Count == 0)
                CalculateDels(x_values, y_values);

            double q = (x - x_values[x0]) / 0.1;
            lagrange_pol += y_values[x0];
            lagrange_pol+=q*Dels[1]


            return lagrange_pol;
        }
    }
}
