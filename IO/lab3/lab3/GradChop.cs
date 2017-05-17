using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class GradChop
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
            Vector antiGrad;
            double alpha;
            double delt;
            int n = x.Length;
            double nt = 0;
            for (It = 0; ; It++)
            {
                antiGrad = new Vector(n);
                for (int i = 0; i < n; i++)
                {
                    antiGrad[i] = -FF.PdF(x, i);
                }
                if (Math.Abs(antiGrad.Norm - nt) <= FF.Eps)
                {
                    break;
                }
                for (alpha = 1; ;)
                {
                    delt = FF.F(x + alpha * antiGrad) - FF.F(x);
                    if (delt <= 0)
                        break;
                    else
                        alpha *= Beta;
                }
                x = x + alpha * antiGrad;
                nt = antiGrad.Norm;
            }
            Min = FF.F(x);
            MinV = x.Copy();
        }
        
    }
}
