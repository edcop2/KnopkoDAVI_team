using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public static class Utils
    {
        public static Matrix GetOwnVectors(Matrix a, double[] ownValues)
        {
            int n = a.RowsCount;
            Matrix Q = new Matrix(n);
            Matrix temp = null;
            double[] ov;
            double[] bn;
            for (int i = 0; i < n; i++)
            {
                temp = a.Copy();
                temp.RemoveRow(n - 1);
                for (int k = 0; k < n - 1; k++)
                    temp[k][k] -= ownValues[i];
                ov = new double[n - 1];
                bn = new double[n - 1];
                for (int k = 0; k < bn.Length; k++)
                    bn[k] = -temp[k][n - 1];
                temp.RemoveColumn(n - 1);

                double[] res = (GaussMainElement.Solve(temp.GetElements(), bn));
                for (int j = 0; j < n - 1; j++)
                {
                    Q[j][i] = res[j];
                }
                Q[n - 1][i] = 1;
            }
            return Q;
        }

        public class CharactericEqual
        {
            int power;

            List<double> pList;

            public CharactericEqual(List<double> pList)
            {
                power = pList.Count;
                this.pList = pList;
            }

            public List<double> GetRoots()
            {
                double r1 = Dichotomy((x) => { return Derivative(F, power - 1, x); }, -1E+2, 1E+2);
                List<double> borders = new List<double> { r1 };
                List<double> roots = new List<double>();
                for (int i = power - 2; i >= 1; i--)
                {
                    borders.AddRange(new double[] { -1E+2, 1E+2 });
                    borders.Sort();
                    for (int j = 0; j < borders.Count - 1; j++)
                    {
                        roots.Add(Dichotomy((x) => { return Derivative(F, i, x); }, borders[j], borders[j + 1]));
                    }
                    borders.Clear();
                    borders.AddRange(roots);
                    roots.Clear();
                }
                borders.AddRange(new double[] { -1E+2, 1E+2 });
                borders.Sort();
                for (int j = 0; j < borders.Count - 1; j++)
                    roots.Add(Dichotomy(F, borders[j], borders[j + 1]));
                return roots;
            }

            public double Derivative(Func<double, double> function, int order, double x)
            {
                if (order < 0)
                    throw new ArgumentException();

                double delta = 0.0005;

                if (order == 1)
                    return (function(x + delta) - function(x)) / delta;
                else
                    return (Derivative(function, order - 1, x + delta) - Derivative(function, order - 1, x)) / delta;
            }

            public double F(double x)
            {
                double res = Math.Pow(x, power);
                for (int i = 0; i < power - 1; i++)
                {
                    res -= pList[i] * Math.Pow(x, power - i - 1);
                }
                res -= pList[power - 1];
                res *= Math.Pow(-1, power);
                return res;
            }

            public double Dichotomy(Func<double, double> function, double a, double b)
            {
                double eps = 0.0000001;
                double c;
                int isRising = (function(a) < function(b)) ? 1 : -1;

                while (b - a > eps)
                {
                    c = (a + b) / 2;
                    if (function(c) * isRising >= 0)
                        b = c;
                    else
                        a = c;
                }
                return (a + b) / 2;
            }

            public override string ToString()
            {
                string s = "l^[" + power + "]";
                for (int i = 0; i < power - 1; i++)
                {
                    s += String.Format("+({0:0.000}*l^[{1}])", pList[i], power - i - 1);
                }
                s += String.Format("+({0:0.000})=0", pList[power - 1]);
                return s;
            }
        }
    }
}
