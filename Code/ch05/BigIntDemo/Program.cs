using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Numerics;

namespace BigIntDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            BigInteger a = UInt64.MaxValue;
            BigInteger b = UInt64.MaxValue;

            Console.WriteLine("Really, Really Big: {0:R} x {1:R} = {2:R}", a, b, (a * b));
            Console.WriteLine("Painfully huge:  Pow({0:R}, {1}) = {2:R}", a, 100, BigInteger.Pow(a, 100));

            string numberToParse = "234743652378423045783479556793498547534684795672309482359874390";
            BigInteger bi = BigInteger.Parse(numberToParse);
            Console.WriteLine("N0: {0:N0}", bi);
            Console.WriteLine("R: {0:R}", bi);
            Console.ReadKey();
        }
     
    }
}
