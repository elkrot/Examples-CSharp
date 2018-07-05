using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringsAndBytes
{
    class Program
    {
        static void Main(string[] args)
        {
            string myString = "C# Rocks!";
            
            Console.WriteLine("Original string: " + myString);
            //ASCII
            byte[] bytes = Encoding.ASCII.GetBytes(myString);
            Console.WriteLine("ASCII bytes: " + BitConverter.ToString(bytes));

            //Unicode
            bytes = Encoding.Unicode.GetBytes(myString);
            Console.WriteLine("Unicode bytes: " + BitConverter.ToString(bytes));

            //UTF32
            bytes = Encoding.UTF32.GetBytes(myString);
            Console.WriteLine("UTF32 bytes: " + BitConverter.ToString(bytes));

            //round-trip: string --> ASCII bytes --> string
            bytes = Encoding.ASCII.GetBytes(myString);
            string result = Encoding.ASCII.GetString(bytes);
            Console.WriteLine("Round trip: {0}->{1}->{2}", myString, BitConverter.ToString(bytes), result);
            
            myString = "C# Rocks!♫";
            Console.WriteLine("With Unicode-only characters: " + myString);

            //ASCII
            bytes = Encoding.ASCII.GetBytes(myString);
            Console.WriteLine("ASCII bytes: " + BitConverter.ToString(bytes));

            //Unicode
            bytes = Encoding.Unicode.GetBytes(myString);
            Console.WriteLine("Unicode bytes: " + BitConverter.ToString(bytes));

            //UTF32
            bytes = Encoding.UTF32.GetBytes(myString);
            Console.WriteLine("UTF32 bytes: " + BitConverter.ToString(bytes));

            //round-trip: string --> ASCII bytes --> string
            bytes = Encoding.ASCII.GetBytes(myString);
            result = Encoding.ASCII.GetString(bytes);
            Console.WriteLine("Round trip: {0}->{1}->{2}", myString, BitConverter.ToString(bytes), result);
            //round-trip: string --> Unicode bytes --> string
            bytes = Encoding.Unicode.GetBytes(myString);
            result = Encoding.Unicode.GetString(bytes);
            Console.WriteLine("Round trip: {0}->{1}->{2}", myString, BitConverter.ToString(bytes), result);

            
            Console.ReadKey();
        }
                

    }
}
