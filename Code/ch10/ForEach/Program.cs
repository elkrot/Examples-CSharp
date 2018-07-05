using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForEach
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = { 1, 2, 3, 4, 5 };
            foreach (int n in array)
            {
                Console.Write("{0} ", n);
            }
            Console.WriteLine();
            List<DateTime> times = new List<DateTime>(new DateTime[]{DateTime.Now, DateTime.UtcNow});
            foreach (DateTime time in times)
            {
                Console.WriteLine(time);
            }

            Dictionary<int, string> numbers = new Dictionary<int, string>();
            numbers[1] = "One"; numbers[2] = "Two"; numbers[3] = "Three";
            foreach (KeyValuePair<int, string> pair in numbers)
            {
                Console.WriteLine("{0}", pair);
            }

            Console.ReadKey();

        }
    }
}
