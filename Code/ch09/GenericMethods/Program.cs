using System;

namespace GenericMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            int aInt = 13;
            int bInt = 26;
            string aString = "Hello";
            string bString = "World";

            Console.WriteLine("aInt: {0}, bInt: {1}", aInt, bInt);
            Console.WriteLine("aString: {0}, bString: {1}", aString, bString);
            Console.WriteLine("Swap!");
            //call Swap with the specific types
            Swap<int>(ref aInt, ref bInt);
            Swap<string>(ref aString, ref bString);

            Console.WriteLine("aInt: {0}, bInt: {1}", aInt, bInt);
            Console.WriteLine("aString: {0}, bString: {1}", aString, bString);

            Console.ReadKey();
        }

        private static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }
    }
}
