using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
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

        public void SetToZero()
        {
            for (int i = 0; i < Length; i++)
                _vect[i] = 0;
        }

        public void SetToOne()
        {
            for (int i = 0; i < Length; i++)
                _vect[i] = 0;
        }

        public Vector BasicVector(int k)
        {
            if (k > Length)
                return null;
            Vector bV = new Vector(Length);
            bV.SetToZero();
            bV[k] = 1;
            return bV;
        }


        public static Vector BasicVector(int k, int n)
        {
            if (k > n)
                return null;
            Vector bV = new Vector(n);
            bV.SetToZero();
            bV[k] = 1;
            return bV;            
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

        public static Vector operator *(Vector v1, Vector v2)
        {
            if (v1.Length != v2.Length)
                throw new InvalidOperationException();
            Vector newVector = new Vector(v1.Length);
            for (int i = 0; i < v1.Length; i++)
                newVector[i] = v1[i] * v2[i];
            return newVector;
        }
        
        public double Norm
        {
            get
            {
                return _vect.Sum(e => Math.Abs(e));
            }

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
