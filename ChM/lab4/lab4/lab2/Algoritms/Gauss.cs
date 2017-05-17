using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace lab4.Algorithms
{
    public class Gauss
    {

        public PolyFunc pf { get; set; }

        public List<double> Solutions { get; set; }

        public List<string> Log { get; set; }

        public List<List<double>> Dels { get; set; }


        public Gauss()
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



        private double Fact(double x)
        {
            if (x == 0)
                return 1;
            else
                return x * Fact(x - 1);
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
                }
                Dels.Add(temp);
            }
            //string s = "";
            //for (int i = 0; i < Dels.Count; i++)
            //{
            //    for (int j = 0; j < Dels[i].Count; j++)
            //        s += Dels[i][j] + " ";
            //    s += "\n";
            //}
            //MessageBox.Show(s);
        }


        public double Calculate(double x, List<double> x_values, List<double> y_values)
        {

            double gauss_pol = 0;
            double basic_pol = 1;

            int x0 = x_values.Count / 2;
            if (Dels.Count == 0)
                CalculateDels(x_values, y_values);

            double q = (x - x_values[x0]) / (x_values[2] - x_values[1]);
            gauss_pol += y_values[x0];
            for (int j = 1, k = 1, k1 = 0; j < y_values.Count; j++)
            {
                basic_pol = q;
                for (int m = 1, m1 = 0, m2 = 1; m < j; m++)
                {
                    if (m1 == 0)
                    {
                        basic_pol *= q + m2;
                        m1 = 1;
                    }
                    else
                    {
                        basic_pol *= q - m2;
                        m2++;
                        m1 = 0;
                    }
                }
                gauss_pol += basic_pol * Dels[j][x0 - k] / Fact(j);
                if (k1 != 0)
                {
                    k++;
                    k1 = 0;
                }
                else
                    k1++;
            }
            return gauss_pol;
        }
    }
}
