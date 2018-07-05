using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericDelegate
{
    class Program
    {
        delegate T DoMath<T>(T a, T b);

        static void Main(string[] args)
        {
            DoMath<int> delegateInt = Add;
            int result1 = delegateInt(1, 2);
            Console.WriteLine("results of delegateInt(1, 2): {0}", result1);

            DoMath<double> delegateDouble = Multiply;
            double result2 = delegateDouble(1.5, 10.0);
            Console.WriteLine("results of delegateDouble(1.5, 10.0): {0}", result2);

            Console.ReadKey();            
        }

        static int Add(int a, int b)
        {
            return a + b;
        }
        
        static int Multiply(int a, int b)
        {
            return a * b;
        }

        static double Add(double a, double b)
        {
            return a + b;
        }

        static double Multiply(double a, double b)
        {
            return a * b;
        }
    }
}
