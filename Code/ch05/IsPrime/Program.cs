using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IsPrime
{
    class Program
    {
        static void Main(string[] args)
        {
            Int32 number = 0;
            if (args.Length > 0 && Int32.TryParse(args[0], out number))
            {
                Console.WriteLine("Is {0:N0} prime? ", IsPrime(number) ? "yes" : "no");
            }
            else
            {
                Console.WriteLine("Usage: IsPrime 12345");
            }
        }

        static bool IsPrime(int number)
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
}
