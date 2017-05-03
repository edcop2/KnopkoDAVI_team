using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    public class Levere
    {
        public Matrix A { get; set; }

        public int N { get; set; }
        public int M { get; set; }

        public List<string> Log { get; set; }

        public List<double> Res { get; set; }

        public Levere()
        {
            Log = new List<string>();
        }
        public void SetAB(DataArray a)
        {
            N = a.N;
            M = a.M;
            A = new Matrix(N, M);
            Res = new List<double>();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < M; j++)
                    A[i][j] = a[i][j];
            }
        }



        public void Calculate()
        {
            List<double> traces = new List<double>();
            List<Matrix> an = new List<Matrix>();
            List<Matrix> bn = new List<Matrix>();
            an.Add(A);
            traces.Add(an[0].GetTrace());
            bn.Add(A - traces[0] * Matrix.IdentityMatrix);
            for (int k = 1, i=2; k < N; k++, i++)
            {
                an.Add(A * bn[k-1]);
                traces.Add(an[k].GetTrace()/i);
                bn.Add(an[k] - traces[k] * Matrix.IdentityMatrix);
            }
            Res = traces.Select(e => -e).ToList();
        }


        public string GetKhaEqu()
        {
            string s = "";
            s += "Характериситическое уравнение: \n λ^3 ";
            for (int i = 0, j = 2; i < N; i++, j--)
            {
                if (j > 1)
                {
                    if (Res[i] < 0)
                    {
                        s += string.Format("- {0}*λ^{1} ", Math.Abs(Res[i]), j);
                    }
                    else
                    {
                        s += string.Format("+ {0}*λ^{1} ", Res[i], j);
                    }
                }
                else if (j == 1)
                {
                    if (Res[i] < 0)
                    {
                        s += string.Format("- {0}*λ ", Math.Abs(Res[i]));
                    }
                    else
                    {
                        s += string.Format("+ {0}*λ ", Res[i]);
                    }
                }
                else
                {
                    if (Res[i] < 0)
                    {
                        s += string.Format("- {0} ", Math.Abs(Res[i]));
                    }
                    else
                    {
                        s += string.Format("+ {0} ", Res[i]);
                    }
                }
            }
            s += "= 0";

            return s;
        }

    }
}
