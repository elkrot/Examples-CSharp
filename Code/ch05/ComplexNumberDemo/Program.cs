using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace ComplexNumberDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Complex a = new Complex(2, 1);
            Complex b = new Complex(3, 2);
            Console.WriteLine("a = {0}", a);
            Console.WriteLine("b = {0}", b);
            Console.WriteLine("a + b = {0}", a + b);
            Console.WriteLine("pow(a,2) = {0}", Complex.Pow(a,2));
            Console.WriteLine("a / 0 = {0}", a / Complex.Zero);

            //this will assign -1 to the real part, and 0 to the imaginary part
            Complex c = -1;
            Console.WriteLine("c = {0}", c);
            Console.WriteLine("Sqrt(c) = {0}", Complex.Sqrt(c));
            Console.WriteLine("Sqrt(c) = {0}",
                string.Format(new ComplexFormatter(), "{0:i}", Complex.Sqrt(c)));
            

            Console.ReadKey();
        }
    }
}
