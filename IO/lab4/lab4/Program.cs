using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            AgainThisNewton atn = new AgainThisNewton();
            Console.WriteLine("Метод Ньютона с регулированием шага:");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Функция: 5(x1 - 3)^2 + (x2  - 5)^2");
            atn.Calculate(new double[] { -1.2, 1 });
            Console.WriteLine();
            Console.WriteLine("Кол-во итераций:" + atn.It );
            Console.WriteLine("Минимум:   " + atn.Min);
            Console.WriteLine(string.Format("Вектор: ({0})", atn.MinV));
            Console.WriteLine();
            Console.WriteLine("Функция: (x1 - 1)^2 + 0,5(x2 - 1)^2 + 2(x3 - 1)^2 + 0,3(x4 - 1)^2 + 0,5(x5 - 1)^2 "
                + "+ 0,5(x6 - 1)^2 + (x7 - 1)^2 + 0,7(x8 - 1)^2 + 0,9(x9 - 1)^2 + 6(x10 - 1)^2");
            atn.Calculate(new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 1);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Кол-во итераций:" + atn.It);
            Console.WriteLine("Минимум:   " + atn.Min);
            Console.WriteLine(string.Format("Вектор: ({0})", atn.MinV));

            Console.WriteLine("=================================");
            Console.WriteLine();


            LevenTOR lt = new LevenTOR();
            Console.WriteLine("Метод Левенберга-Марквардта:");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Функция: 5(x1 - 3)^2 + (x2  - 5)^2");
            lt.Calculate(new double[] { -1.2, 1 });
            Console.WriteLine();
            Console.WriteLine("Кол-во итераций:" + lt.It);
            Console.WriteLine("Минимум:   " + lt.Min);
            Console.WriteLine(string.Format("Вектор: ({0})", lt.MinV));
            Console.WriteLine();
            Console.WriteLine("Функция: (x1 - 1)^2 + 0,5(x2 - 1)^2 + 2(x3 - 1)^2 + 0,3(x4 - 1)^2 + 0,5(x5 - 1)^2 "
                + "+ 0,5(x6 - 1)^2 + (x7 - 1)^2 + 0,7(x8 - 1)^2 + 0,9(x9 - 1)^2 + 6(x10 - 1)^2");
            lt.Calculate(new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 1);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Кол-во итераций:" + lt.It );
            Console.WriteLine("Минимум:   " + lt.Min);
            Console.WriteLine(string.Format("Вектор: ({0})", lt.MinV));




        }

    }
}
