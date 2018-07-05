using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PowerOfTwo
{
    class PowerOfTwo
    {
        static void Main(string[] args)
        {
            for (Int64 i = 0; i < Int64.MaxValue; i++)
            {
                if (IsPowerOfTwo(i))
                {
                    Console.WriteLine("{0:N0} is a power of 2\t\t(Ctrl-C to stop)", i);
                }
            }
        }
        private static bool IsEven(Int64 number)
        {
            return ((number & 0x1)==0);
        }

        private static bool IsPowerOfTwo(Int64 number)
        {
            return (number != 0) && ((number & -number) == number);
        }
    }
}
