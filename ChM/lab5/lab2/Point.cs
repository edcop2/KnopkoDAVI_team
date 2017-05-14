using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class Point
    {
        public double x { get; set; }
        public double y { get; set; }
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        public Point()
        {
            this.x = 0;
            this.y = 0;
        }
    }
    public class CompareByX : IComparer<Point>
    {
        public int Compare(Point p1, Point p2)
        {
            return p1.x.CompareTo(p2.x);
        }
    }
}
