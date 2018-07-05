using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace NaturalSort
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] originals = new string[]
            {
                "Part 1", "Part 2", "Part 3", "Part 4", "Part 5",
                "Part 6", "Part 7", "Part 8", "Part 9", "Part 10",
                "Part 11", "Part 12", "Part 13", "Part 14", "Part 15",
                "Part 16", "Part 17", "Part 18", "Part 19", "Part 20"
            };

            Console.WriteLine("Naive sort:");
            List<string> copy = new List<string>(originals);
            copy.Sort((string x, string y) => {
                //essentially same comparison as Sort() with no arguments
                return x.CompareTo(y);
            });
            foreach (string s in copy)
            {
                Console.WriteLine("\t{0}", s);
            }

            Console.WriteLine();
            Console.WriteLine("Natural Sort:");
            copy = new List<string>(originals);
            copy.Sort(new NaturalSorter());
            foreach (string s in copy)
            {
                Console.WriteLine("\t{0}", s);
            }

            Console.ReadKey();
        }
    }

    class NaturalSorter : IComparer<string>
    {
        private char[] _splitBuffer = new char[256];

        public int Compare(string x, string y)
        {
            //first split each string into segments of non-numbers and numbers
            IList<string> a = SplitByNumbers(x);
            IList<string> b = SplitByNumbers(y);

            int aInt, bInt;
            int numToCompare = (a.Count < b.Count) ? a.Count : b.Count;
            for (int i = 0; i < numToCompare; i++)
            {
                if (a[i].Equals(b[i]))
                    continue;

                bool aIsNumber = Int32.TryParse(a[i], out aInt);
                bool bIsNumber = Int32.TryParse(b[i], out bInt);
                bool bothNumbers = aIsNumber && bIsNumber;
                bool bothNotNumbers = !aIsNumber && !bIsNumber;
                //do an integer compare
                if (bothNumbers) return aInt.CompareTo(bInt);
                //do a string compare
                if (bothNotNumbers) return a[i].CompareTo(b[i]);
                //only one is a number, and it's less than the non-number
                if (aIsNumber) return -1;
                return 1;
            }
            //only get here if one string is empty
            return a.Count.CompareTo(b.Count);
        }

        private IList<string> SplitByNumbers(string val)
        {
            Debug.Assert(val.Length <= 256);
            List<string> list = new List<string>();
            int current = 0;
            int dest = 0;
            while (current < val.Length)
            {
                //accumulate non-numbers
                while (current < val.Length && !char.IsDigit(val[current]))
                {
                    _splitBuffer[dest++] = val[current++];
                }
                if (dest > 0)
                {
                    list.Add(new string(_splitBuffer, 0, dest));
                    dest = 0;
                }
                //accumulate numbers
                while (current < val.Length && char.IsDigit(val[current]))
                {
                    _splitBuffer[dest++] = val[current++];
                }
                if (dest > 0)
                {
                    list.Add(new string(_splitBuffer, 0, dest));
                    dest = 0;
                }
            }

            return list;
        }
    }
}
