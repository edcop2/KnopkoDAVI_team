using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    public class LevenTOR
    {
        public Vector X { get; set; }

        public Function FF { get; set; } = new Function();

        public double Min { get; set; }

        public Vector MinV { get; set; }

        public double Beta { get; set; } = 0.5;

        public int It { get; set; }

        public void Calculate(double[] x0, int flag = 0, double eps = 0.01)
        {
            FF.Flag = flag;
            FF.Eps = eps;

            Descend(new Vector(x0));


        }

        public void Descend(Vector x)
        {
            Vector dFs;
            double alpha, delta;
            int n = x.Length;
            Matrix gesse = new Matrix(n);
            Vector hX;
            Matrix inverse;
            double nt = 0;
            Vector xt = null;
            for (It = 0; ; It++)
            {
                dFs = new Vector(n);
                hX = new Vector(n);
                for (int i = 0; i < n; i++)
                {
                    dFs[i] = FF.PdF(x, i);
                }
                if (xt != null && Math.Abs((x - xt).Norm) < FF.Eps)
                {
                    break;
                }
                Matrix tmp1 = new Matrix(n, 1);
                Matrix tmp2 = new Matrix(1, n);
                for (int i = 0; i < n; i++)
                {
                    tmp1[i][0] = dFs[i];
                    tmp2[0][i] = dFs[i];
                }
                gesse = tmp1 * tmp2;
                //for (int i = 0; i < n; i++)
                //{
                //    for (int j = 0; j < n; j++)
                //        gesse[i][j] = FF.Pd2F(x, i, j);
                //}
                inverse = (gesse + Matrix.GetIdentityMatrix(n) * FF.Eps).GetInverseMatrix;
                if (inverse != null)
                {
                    double temp;
                    for (int i = 0; i < n; i++)
                    {
                        temp = 0;
                        for (int j = 0; j < n; j++)
                        {
                            temp += dFs[j] * inverse[i][j];
                        }
                        hX[i] = -temp;
                    }
                }
                else
                {
                    for (int i = 0; i < n; i++)
                        hX[i] = -dFs[i];
                }
                for (alpha = 1; ;)
                {
                    delta = FF.F(x + alpha * hX) - FF.F(x) ;
                    if (delta <= 0)
                        break;
                    else
                        alpha *= Beta;
                }
                xt = x;
                x = x + alpha * hX;
                nt = dFs.Norm;

            }
            Min = FF.F(x);
            MinV = x.Copy();
        }

        public double Optimize(Vector x, Vector dFs, int k)
        {
            double a;
            double min, tMin = FF.F(x);
            for (a = FF.Eps; ; a += FF.Eps)
            {
                min = FF.F(x - dFs[k] * (x.BasicVector(k) * a));
                if (min >= tMin)
                {
                    break;
                }
                tMin = min;
            }
            return a;
        }

    }

}
