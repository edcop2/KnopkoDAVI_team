﻿using lab2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab5.Algoritms
{
    public class RoltonRabinovich
    {

        public static int Flag { get; set; } = 1;

        static double f(double x, double y)
        {
            if (Flag == 1)
                return Math.Cos(1.5 * x + y) + 1.5 * (x - y);
            else
                return 1 - 0.1 * y / (x + 2) - Math.Sin(2 * x + y);
        }

        public List<Point> Rolton(double x, double x1, double y, int n, int flag=1)
        {
            Flag = flag;
            List<Point> result = new List<Point>() { new Point(x, y) };
            double h = 0;
            double k1 = 0;
            double k2 = 0;
            double y1 = 0;
            h = (x1 - x) / n;
            for (int i = 0; i < n; i++)
            {
                k1 = f(x, y);
                k2 = f(x + (h * 3) / 4, y + (h * k1 * 3) / 4);
                y1 = y + h * (k1 / 3 + (2 * k2) / 3);
                x = x + h;
                result.Add(new Point(x, y1));
                y = y1;
            }
            return result;
        }
    }
}
