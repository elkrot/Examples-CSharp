using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CountBits
{
    class CountBitsProgram
    {
        static void Main(string[] args)
        {
            for (int i = 0; i < 25; i++)
            {
                Console.WriteLine("{0} bits set in {1} ({2})", CountBits(i), i, Convert.ToString(i,2));
            }
            Console.ReadKey();
        }

        static Int16 CountBits(Int32 number)
        {
            int accum = number;
            Int16 count = 0;
            while (accum > 0)
            {
                accum &= (accum - 1);
                count++;
            }
            return count;
        }
    }
}
