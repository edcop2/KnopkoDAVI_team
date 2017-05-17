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
          //  Console.WriteLine(new Vector(new double[] { 3,1,5,15}).CheckAB(-3,45));
            ctb.A = double.Parse(Console.ReadLine());
            ctb.B = double.Parse(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("Циклический покоординатный спуск:");
            Console.WriteLine();
            Console.WriteLine("Функция: 2*x1*x2*x3 - 4*x1*x3 - 2*x2*x3 - x1*x1 + x2*x2 + x3*x3 -2*x1 - 4*x2 + 4*x3");
            ctb.Calculate(new double[] { 3, 3, 3 });
            Console.WriteLine("Кол-во итераций: "+ctb.It);
            Console.WriteLine("Минимум:   " + ctb.Min);
            Console.WriteLine(string.Format("Вектор: ({0},{1},{2})", Math.Round(ctb.VMin[0], 2), Math.Round(ctb.VMin[1], 2), Math.Round(ctb.VMin[2], 2)));
            Console.WriteLine();
            Console.WriteLine("Функция: exp(x2) - cos(x1*x1 - x2)");
            ctb.Calculate(new double[] { 0, 0 }, 2);
            Console.WriteLine("Кол-во итераций: " + ctb.It);
            Console.WriteLine("Минимум:   " + ctb.Min);
            Console.WriteLine(string.Format("Вектор: ({0},{1})", Math.Round(ctb.VMin[0], 2), Math.Round(ctb.VMin[1], 2)));



            Rosenrot rose = new Rosenrot();
            rose.A = ctb.A;
            rose.B = ctb.B;
            Console.WriteLine();
            Console.WriteLine("Метод Розенброка:");
            Console.WriteLine();
            Console.WriteLine("Функция: 2*x1*x2*x3 - 4*x1*x3 - 2*x2*x3 - x1*x1 + x2*x2 + x3*x3 -2*x1 - 4*x2 + 4*x3");
            rose.Calculate(new double[] { 3, 3, 3 }, 1);
            Console.WriteLine("Кол-во итераций: " + rose.It);
            Console.WriteLine("Минимум:   " + rose.Min);
            Console.WriteLine(string.Format("Вектор: ({0})", rose.VMin));
            Console.WriteLine();
            Console.WriteLine("Функция: exp(x2) - cos(x1*x1 - x2)");
            rose.Calculate(new double[] { 0, 3 }, 2);
            Console.WriteLine("Кол-во итераций: " + rose.It);
            Console.WriteLine("Минимум:   " + rose.Min);
            Console.WriteLine(string.Format("Вектор: ({0})", rose.VMin));


            // Console.WriteLine(Math.Round(new Vector(new double[] { 3, 4, 5 }).Modus(),2));
        }
    }
}
