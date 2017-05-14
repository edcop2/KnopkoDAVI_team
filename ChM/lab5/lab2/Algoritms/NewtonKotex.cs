using lab5.Algorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5.Algoritms
{
    public class NewtonKotex
    {

        public PolyFunc pf { get; set; }

        public List<double> Solutions { get; set; }


        public List<string> Log { get; set; }

        public List<double> Hi { get; set; }

        public double X0 { get; set; }

        public double Eps { get; set; }


        public NewtonKotex()
        {
            pf = new PolyFunc();
            pf.Flag = 1;
            Solutions = new List<double>();
            Log = new List<string>();
            Hi = new List<double>();
        }

        public void Clear()
        {
            pf = new PolyFunc();
            pf.Flag = 1;
            Solutions.Clear();
            Log.Clear();
            Hi.Clear();
        }

        private double Fact(double x)
        {
            if (x == 0)
                return 1;
            else
                return x * Fact(x - 1);
        }


        private double T(double x)
        {
            return (x - X0) / Eps;

        }



        public void Calculate(double a, double b, int n, double eps)
        {
            if (eps <= 0)
                return;

            if (a >= b)
                return;



            Solutions.Add(NewtonCotes(a, b, n, n));


        }

        public double NewtonCotes(double a, double b, int Degree, int Ndivisions)
        {
            int[][] koef = new int[][]{ new int[]{ 1,0,0,0,0,0,0,0,0,0 },
                        new int[]{ 1,1,0,0,0,0,0,0,0,0 },
                       new int[] { 1,4,1,0,0,0,0,0,0,0 },
                       new int[] { 1,3,3,1,0,0,0,0,0,0 },
                       new int[] { 7,32,12,32,7,0,0,0,0,0 },
                       new int[] { 19,75,50,50,75,19,0,0,0,0 },
                       new int[] { 41,216,27,272,27,216,41,0,0,0 },
                       new int[] { 751,3577,1323,2989,2989,1323,3577,751,0,0 },
                        new int[]{ 989,5888,-928,10496,-4540,10496,-928,5888,989,0 },
                        new int[]{ 2857,15741,1080,19344,5778,5778,19344,1080,15741,2857 }
                        };
            double[] mltp = { 1, 1.0 / 2, 1.0 / 3, 3.0 / 8, 2.0 / 45, 5.0 / 288, 1.0 / 140, 7.0 / 17280, 4.0 / 14175, 9.0 / 89600 };

            if ((Degree < 0) || (Degree > 9))
                throw new Exception("Wrong degree");
            if (a >= b)
                throw new Exception("Wrong segment");
            if (Ndivisions < 1)
                Ndivisions = 1;

            double Sum, PartSum;
            double h = (b - a) / (Degree * Ndivisions);

            Sum = 0;
            for (int j = 0; j < Ndivisions; j++)
            {
                PartSum = 0;
                for (int i = 0; i <= Degree; i++)
                    PartSum += koef[Degree][i] * pf.F(a + (i + j * Degree) * h);
                Sum += mltp[Degree] * PartSum * h;
                Log.Add(Sum.ToString());
            }

            return Sum;
        }

    }
}
