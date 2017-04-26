using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    class Program
    {
        static void Main(string[] args)
        {

            CyclicToBottom ctb = new CyclicToBottom();
            Console.WriteLine("Введите границы:");
            ctb.A = double.Parse(Console.ReadLine());
            ctb.B = double.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Циклический покоординатный спуск:");
            Console.WriteLine();
            Console.WriteLine("Функция: 2*x1*x2*x3 - 4*x1*x3 - 2*x2*x3 - x1*x1 + x2*x2 + x3*x3 -2*x1 - 4*x2 + 4*x3");
            ctb.Calculate(new double[] { 3, 3, 3 });
            Console.WriteLine("Минимум:   " + ctb.Min);
            Console.WriteLine(string.Format("Вектор: ({0},{1},{2})", Math.Round(ctb.VMin[0], 2), Math.Round(ctb.VMin[1], 2), Math.Round(ctb.VMin[2], 2)));
            Console.WriteLine();
            Console.WriteLine("Функция: exp(x2) - cos(x1*x1 - x2)");
            ctb.Calculate(new double[] { 0, 0 }, 2);
            Console.WriteLine("Минимум:   " + ctb.Min);
            Console.WriteLine(string.Format("Вектор: ({0},{1})", Math.Round(ctb.VMin[0], 2), Math.Round(ctb.VMin[1], 2)));
        }
    }
}
