using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    public class Vector
    {
        private double[] _vect;

        public int Length
        {
            get
            {
                return _vect.Length;
            }
            set
            {
                Array.Resize(ref _vect, value);
            }
        }

        public Vector(int n)
        {
            _vect = new double[n];
        }

        public Vector(double[] v)
        {
            _vect = v;
        }

        public bool CheckAB(double a, double b)
        {
            foreach (var i in _vect)
                if (i < a || i > b)
                    return false;
            return true;

        }

        public static Vector operator +(Vector v1, Vector v2)
        {
            if (v1.Length != v2.Length)
                throw new InvalidOperationException();
            Vector newVector = new Vector(v1.Length);
            for (int i = 0; i < v1.Length; i++)
                newVector[i] = v1[i] + v2[i];
            return newVector;
        }

        public static Vector operator -(Vector v1, Vector v2)
        {
            if (v1.Length != v2.Length)
                throw new InvalidOperationException();
            Vector newVector = new Vector(v1.Length);
            for (int i = 0; i < v1.Length; i++)
                newVector[i] = v1[i] - v2[i];
            return newVector;
        }


        public static Vector operator *(Vector v, double k)
        {
            Vector newVector = new Vector(v.Length);
            for (int i = 0; i < v.Length; i++)
                newVector[i] = v[i] * k;
            return newVector;
        }

        public static Vector operator -(Vector v, double k)
        {
            Vector newVector = new Vector(v.Length);
            for (int i = 0; i < v.Length; i++)
                newVector[i] = v[i] - k;
            return newVector;
        }

        public static Vector operator /(Vector v, double k)
        {
            Vector newVector = new Vector(v.Length);
            for (int i = 0; i < v.Length; i++)
                newVector[i] = v[i] / k;
            return newVector;
        }

        public Vector Copy()
        {
            int n = Length;
            double[] clone = new double[n];
            for (int i = 0; i < n; i++)
                clone[i] = _vect[i];
            return new Vector(clone);
        }

        public static Vector operator *(double k, Vector v)
        {
            Vector newVector = new Vector(v.Length);
            for (int i = 0; i < v.Length; i++)
                newVector[i] = v[i] * k;
            return newVector;
        }

        public static double operator *(Vector v1, Vector v2)
        {
            if (v1.Length != v2.Length)
                throw new InvalidOperationException();
            double scalar = 0;
            for (int i = 0; i < v1.Length; i++)
                scalar += v1[i] * v2[i];
            return scalar;
        }

        public double Modus()
        {
            return Math.Sqrt(_vect.Select(e => e * e).Sum());
        }



        public double this[int i]
        {
            get
            {
                return _vect[i];
            }
            set
            {
                _vect[i] = value;
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Length; i++)
            {
                if (double.IsInfinity(_vect[i]) || double.IsNaN(_vect[i]))
                    sb.AppendFormat("{0}  ", new string('_', 6));
                else
                    sb.AppendFormat("{0:0.000}  ", _vect[i]);
            }
            return sb.ToString();
        }

    }
}
