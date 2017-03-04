using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace lab1.Algorithms
{
    public class SimpleIterations
    {
        public double[][] A { get; set; }
        public double[] B { get; set; }

        public double[][] _B { get; set; }
        public double[] _D { get; set; }

        public List<string> Log { get; set; }

        public SimpleIterations(int n)
        {
            A = new double[n][];
            _B = new double[n][];
            for (int i = 0; i < n; i++)
            {
                A[i] = new double[n];
                _B[i] = new double[n];
            }
            B = new double[n];
            _D = new double[n];
            Log = new List<string>();
        }

        private double[] Base(double[] x)
        {
            int n = x.Length;
            double[] newX = new double[n];

            for (int i = 0; i < n; i++)
                newX[i] = 0;

            for (int i = 0; i < n; i++)
            {
                newX[i] += B[i];
                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                        newX[i] -= A[i][j] * x[j];
                }
                newX[i] /= A[i][i];
            }

            return newX;
        }

        private double FirstForm()
        {
            double sum = 0, sumt = 0;
            int n = A.Length;
            for (int i = 0; i < n; i++)
            {
                sumt = 0;
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                        sumt += Math.Abs(A[i][j] / A[i][i]);
                }
                if (sum < sumt)
                    sum = sumt;
            }
            return sum;
        }


        public double[] Calculate(double eps)
        {
            Log.Clear();
            int n = A.Length;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                        _B[i][j] = A[i][j] / A[i][i];
                    else
                        _B[i][j] = 0;
                }
                _D[i] = B[i] / A[i][i];
            }

            double b1 = FirstForm();

            if (b1 >= 1)
            {
                Log.Add("Данная система не сходится по этому алгоритму");
                return null;
            }

            double[] res = _D;
            double[] rest;
            int k = 1;
            string s = "Итерация №0\n";
            s += string.Format("x1 = {0}; x2 = {1}; x3 = {2}; x4 = {3}\n********\n",
                Math.Round(res[0], (eps % 1).ToString().Length - 2), Math.Round(res[1], (eps % 1).ToString().Length - 2),
                Math.Round(res[2], (eps % 1).ToString().Length-2), Math.Round(res[3], (eps % 1).ToString().Length - 2));
            Log.Add(s);
            do
            {
                s = "";
                rest = res;
                res = Base(res);
                s += "Итерация №" + k + "\n";
                s += string.Format("x1 = {0}; x2 = {1}; x3 = {2}; x4 = {3}\n********\n",
                    Math.Round(res[0], (eps % 1).ToString().Length - 2), Math.Round(res[1], (eps % 1).ToString().Length - 2), 
                    Math.Round(res[2], (eps % 1).ToString().Length - 2), Math.Round(res[3], (eps % 1).ToString().Length - 2));
                Log.Add(s);
                k++;
            } while (Math.Abs(res.Sum() - rest.Sum()) > Math.Abs(1 - b1) / b1 * eps);

            
            return res;
        }
    }

}
