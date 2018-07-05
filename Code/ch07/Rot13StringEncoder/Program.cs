using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rot13StringEncoder
{
    class Program
    {
        static void Main(string[] args)
        {
            Rot13Encoder encoder = new Rot13Encoder();

            string original = "hellocsharp";

            byte[] bytes = encoder.GetBytes(original);

            //if we assumed ASCII, what does it look like?
            Console.WriteLine("Original: {0}", original);
            Console.WriteLine("ASCII interpretation: {0}", Encoding.ASCII.GetString(bytes));
            Console.WriteLine("Rot13 interpretation: {0}", encoder.GetString(bytes));

            Console.ReadKey();

        }
    }
}
