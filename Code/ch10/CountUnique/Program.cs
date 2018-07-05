using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetUnique
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            list.AddRange(new int[] { 1, 1, 1, 2, 3, 4, 7, 7, 7, 7, 7, 7, 7, 5, 5, 10, 8, 8 });
            Console.Write("Original: ");
            PrintCollection<int>(list);
            ICollection<int> uniqueList = GetUniques<int>(list);
            Console.Write("Uniques: ");
            PrintCollection<int>(uniqueList);
            CountUniques<int>(list);
            Console.ReadKey();
        }

        private static ICollection<T> GetUniques<T>(ICollection<T> list)
        {
            Dictionary<T, bool> found = new Dictionary<T, bool>();
            List<T> uniques = new List<T>();
            //this algorithm will preserve the original order
            foreach (T val in list)
            {
                if (!found.ContainsKey(val))
                {
                    found[val] = true;
                    uniques.Add(val);
                }
            }
            return uniques;
        }

        private static void CountUniques<T>(ICollection<T> coll)
        {
            Dictionary<T, int> counts = new Dictionary<T, int>();
            
            foreach (T val in coll)
            {
                if (counts.ContainsKey(val))
                    counts[val]++;
                else
                {
                    counts[val] = 1;
                }
            }
            foreach (KeyValuePair<T, int> pair in counts)
            {
                Console.WriteLine("{0} appears {1} time(s)", pair.Key, pair.Value);
            }
        }

        static void PrintCollection<T>(ICollection<T> list)
        {
            foreach (T val in list)
            {
                Console.Write("{0} ", val);
            }
            Console.WriteLine();
        }
    }
}
