using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1.Model
{
    public class Matrix
    {
        public double A1 { get; set; }
        public double A2 { get; set; }
        public double A3 { get; set; }
        public double A4 { get; set; }
        public double B { get; set; }
        

        public Matrix()
        {
            A1 = 0;
            A2 = 0;
            A3 = 0;
            A4 = 0;
            B = 0;
        }

        public double[] ToArray()
        {
            return new double[] { A1, A2, A3, A4 };
        }
        


    }
}
