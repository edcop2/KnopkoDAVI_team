using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Algorithms
{
    public static class Competitiveness
    {
        public static readonly double[] WeightingFactor =
        {
            0.14, 0.1, 0.2, 0.05, 0.13, 0.1, 0.06, 0.13, 0.09
        };

        public static double Calculate(int[] myProj, int[] otherProj)
        {
            if (myProj.Length != otherProj.Length && myProj.Length != 9)
                throw new ArgumentException();
            double j1 = 0, j2 = 0;
            for (int i = 0; i < 9; i++)
            {
                j1 += WeightingFactor[i] * myProj[i];
                j2 += WeightingFactor[i] * otherProj[i];
            }
            return j1 / j2;
            //if (ak < 1)
            //{
            //    return $"A(k) = {ak} \nРазработка с технической точки зрения не оправдана";
            //}
            //return $"A(k) = {ak} \nРазработка с технической точки зрения оправдана";
        }
    }
}
