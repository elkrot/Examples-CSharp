using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomNumbers
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("using System.Random:");

            Random rand = new Random();
            for (int i = 0; i < 5; i++)
            {
                Console.Write("{0} ", rand.Next());
            }
            Console.WriteLine();

            Console.WriteLine("using System.Random/numbers in range (-10, 10)");
            for (int i = 0; i < 5; i++)
            {
                Console.Write("{0} ", rand.Next(-10,10));
            }
            Console.WriteLine();
            
            Console.WriteLine("using RNGCryptoServiceProvider:");

            System.Security.Cryptography.RNGCryptoServiceProvider cryptRand = new System.Security.Cryptography.RNGCryptoServiceProvider();
            for (int i = 0; i < 5; i++)
            {
                byte[] bytes = new byte[4];
                cryptRand.GetBytes(bytes);
                Int32 number = BitConverter.ToInt32(bytes, 0);
                Console.Write("{0} ", number);
            }
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
