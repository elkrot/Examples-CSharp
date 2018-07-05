using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtensionMethods
{
    //extension methods must be defined in a static class
    static class IntMethods
    {
        //extension methods must be static
        //this keyword tells C# that this is an extension method
        public static bool IsPrime(this int number)
        {
            //check for evenness
            if (number % 2 == 0)
            {
                if (number == 2)
                {
                    return true;
                }
                return false;
            }
            //don't need to check past the square root
            int max = (int)Math.Sqrt(number);
            for (int i = 3; i <= max; i += 2)
            {
                if ((number % i) == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 100; ++i)
            {
                if (i.IsPrime())
                {
                    Console.WriteLine(i);
                }
            }
            Console.ReadKey();
        }
    }
}
