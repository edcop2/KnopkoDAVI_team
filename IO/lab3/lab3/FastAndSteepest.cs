using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class FastAndSteepest
    {
        public Vector X { get; set; }

        public Function FF { get; set; } = new Function();

        public double Min { get; set; }

        public Vector MinV { get; set; }

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
            Vector alpha;
            int n = x.Length;
            for (It = 0; ; It++)
            {
                dFs = new Vector(n);
                alpha = new Vector(n);
                for (int i = 0; i < n; i++)
                {
                    dFs[i] = FF.PdF(x, i);
                }
                if (dFs.Norm < FF.Eps)
                {
                    break;
                }
                for (int i = 0; i < n; i++)
                    alpha[i] = Optimize(x, dFs, i);
                x = x - alpha * dFs;
            }
            Min = FF.F(x);
            MinV = x.Copy();
        }

        public double Optimize(Vector x, Vector dFs, int k)
        {
            double a;
            double min, tMin = FF.F(x);
            for (a = FF.Eps; ; a+=FF.Eps)
            {
                min = FF.F(x - dFs[k] * (x.BasicVector(k) * a));     
                if (min >= tMin )
                {
                    break;
                }
                tMin = min;
            }
            return a;
        }




    }
}
