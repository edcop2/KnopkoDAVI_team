using MathParserNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab2.Algorithms
{

    public class MyPoint
    {
        public double X { get; set; }
        public double Y { get; set; }

        public MyPoint()
        {
            X = 0;
            Y = 0;
        }
        public MyPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return string.Format("[{0};{1}]", X, Y);
        }
    }


    public interface IFunction
    {
        double F(double x);

        double dF(double x);
    }

    public class PolyFunc : IFunction
    {
        public string StringFunction { get; set; }

        public bool IsReverse { get; set; }

        public double Eps { get; set; }

        public PolyFunc() : base()
        {
            IsReverse = false;
        }

        public bool CheckFunc()
        {
            Parser parser = new Parser();
            parser.AddVariable("x", 2);
            double solution;
            try
            {
                solution = parser.SimplifyDouble(StringFunction);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public double F(double x)
        {
            Parser parser = new Parser();
            parser.AddVariable("x", x);
            double solution;
            try
            {
                solution = parser.SimplifyDouble(StringFunction);
            }
            catch
            {
                return double.NaN;
            }
            if (IsReverse)
                return -solution;
            else

                return solution;
        }

        public double dF(double x)
        {
            return (F(x + Eps) - F(x)) / Eps;
        }

        public double ddF(double x)
        {
            return (dF(x + Eps) - dF(x)) / Eps;
        }
    }


    [Serializable]
    public class SerializedFunc
    {

        public PolyFunc pf;


        [XmlAttributeAttribute()]
        public string eps;

        [XmlAttributeAttribute()]
        public string xMin, xMax, yMin, yMax;

        public SerializedFunc()
        { }

        public SerializedFunc(PolyFunc pf, string eps, string xMin, string xMax, string yMin, string yMax)
        {
            this.pf = pf;
            this.eps = eps;
            this.xMin = xMin;
            this.xMax = xMax;
            this.yMin = yMin;
            this.yMax = yMax;
        }

        public void Write(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(SerializedFunc));
            using (TextWriter textWriter = new StreamWriter(fileName))
            {
                serializer.Serialize(textWriter, this);
            }
        }

        public void Read(string fileName)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(SerializedFunc));
            using (TextReader textReader = new StreamReader(fileName))
            {
                SerializedFunc sf = (SerializedFunc)deserializer.Deserialize(textReader);
                this.pf = sf.pf;
                this.eps = sf.eps;
                this.xMin = sf.xMin;
                this.xMax = sf.xMax;
                this.yMax = sf.yMax;
                this.yMin = sf.yMin;
            }
        }
    }
}
