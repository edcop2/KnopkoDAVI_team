﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace lab5.Algorithms
{
    public class DihMethod
    {

        public PolyFunc pf { get; set; }

        public List<double> Solutions { get; set; }

        public List<string> Log { get; set; }


        public DihMethod()
        {
            pf = new PolyFunc();
            Solutions = new List<double>();
            Log = new List<string>();
        }

        public void Clear()
        {
            pf = new PolyFunc();
            Solutions.Clear();
            Log.Clear();
        }

        public void Calculate(double a, double b, double eps)
        {
            if (eps <= 0)
                return;

            if (a >= b)
                return;



            while (Math.Abs(b - a) >= eps)
            {
                double x = (a + b) / 2;
                if (pf.F(a) * pf.F(x) < 0)
                    b = x;
                else
                    a = x;
                Log.Add(x.ToString());
            }
            Log.Add(((a + b) / 2).ToString());
            Solutions.Add((a + b) / 2);
        }
    }
}
