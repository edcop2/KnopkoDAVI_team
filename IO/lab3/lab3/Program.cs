using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            FastAndSteepest fas = new FastAndSteepest();
            Console.WriteLine("Метод наискорейшего спуска:");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Функция: 5(x1 - 3)^2 + (x2  -5)^2");
            fas.Calculate(new double[] { -1.2, 1 });
            Console.WriteLine();
            Console.WriteLine("Кол-во итераций:" + fas.It);
            Console.WriteLine("Минимум:   " + fas.Min);
            Console.WriteLine(string.Format("Вектор: ({0})", fas.MinV));
            Console.WriteLine();
            Console.WriteLine("Функция: (x1 - 1)^2 + 0,5(x2 - 1)^2 + 2(x3 - 1)^2 + 0,3(x4 - 1)^2 + 0,5(x5 - 1)^2 "
                + "+ 0,5(x6 - 1)^2 + (x7 - 1)^2 + 0,7(x8 - 1)^2 + 0,9(x9 - 1)^2 + 6(x10 - 1)^2");
            fas.Calculate(new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 1);
            Console.WriteLine();
            Console.WriteLine("Кол-во итераций:" + fas.It);
            Console.WriteLine("Минимум:   " + fas.Min);
            Console.WriteLine(string.Format("Вектор: ({0})", fas.MinV));


            Console.WriteLine("=================================");
            Console.WriteLine();

            GradChop gc = new GradChop();
            Console.WriteLine("Градиентный метод с дроблением шага:");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Функция: 5(x1 - 3)^2 + (x2 - 5)^2");
            gc.Calculate(new double[] { -1.2, 1 });
            Console.WriteLine();
            Console.WriteLine("Кол-во итераций:" + gc.It);
            Console.WriteLine("Минимум:   " + gc.Min);
            Console.WriteLine(string.Format("Вектор: ({0})", gc.MinV));
            Console.WriteLine();
            Console.WriteLine("Функция: (x1 - 1)^2 + 0,5(x2 - 1)^2 + 2(x3 - 1)^2 + 0,3(x4 - 1)^2 + 0,5(x5 - 1)^2 "
                + "+ 0,5(x6 - 1)^2 + (x7 - 1)^2 + 0,7(x8 - 1)^2 + 0,9(x9 - 1)^2 + 6(x10 - 1)^2");
            gc.Calculate(new double[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }, 1);
            Console.WriteLine();
            Console.WriteLine("Кол-во итераций:" + gc.It);
            Console.WriteLine("Минимум:   " + gc.Min);
            Console.WriteLine(string.Format("Вектор: ({0})", gc.MinV));

        }
    }
}
