﻿using lab2.Algorithms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace lab2.Algoritms
{
    class CombinedMethod
    {
        public PolyFunc pf { get; set; }

        public List<double> Solutions { get; set; }


        public CombinedMethod()
        {
            pf = new PolyFunc();
            Solutions = new List<double>();
        }

        public void Clear()
        {
            pf = new PolyFunc();
            Solutions.Clear();
        }

        public void Calculate(double a, double b, double eps)
        {
             if (eps <= 0)
                 return ;

             if (a >= b)
                 return ;


            while (Math.Abs(b - a) > 2*eps)
            {
                if (pf.F(a) * pf.ddF(a) < 0)
                    a -= pf.F(a) * ((a - b) / pf.F(a) - pf.F(b));
                else
                    a -= pf.F(a) / pf.dF(a);
                if (pf.F(b) * pf.ddF(b) < 0)
                    b -= pf.F(b) * ((b - a) / pf.F(b) - pf.F(a));
                else
                    b -= pf.F(b) / pf.dF(b);


            }
            Solutions.Add((a + b) / 2);
        }
    }
}