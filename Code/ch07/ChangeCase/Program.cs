using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace ChangeCase
{
    class Program
    {
        static void Main(string[] args)
        {
            string original = "file";
            Console.WriteLine("Original: "+original);
            Console.WriteLine("Uppercase (invariant): " + original.ToUpperInvariant());
            Console.WriteLine("Uppercase (Turkish): " + original.ToUpper(CultureInfo.CreateSpecificCulture("tr-TR")));

            byte[] bytesInvariant = Encoding.Unicode.GetBytes(original.ToUpperInvariant());
            byte[] bytesTurkish = Encoding.Unicode.GetBytes(original.ToUpper(CultureInfo.CreateSpecificCulture("tr-TR")));

            Console.WriteLine("Bytes (invariant): " + BitConverter.ToString(bytesInvariant));
            Console.WriteLine("Bytes   (Turkish): " + BitConverter.ToString(bytesTurkish));

            Console.ReadKey();
        }
    }
}
