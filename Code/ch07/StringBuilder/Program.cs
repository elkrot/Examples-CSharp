using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace StringBuilderTime
{

    class Program
    {
        static Random rand = new Random();
        static void Main(string[] args)
        {
            int numWords = 0;
            if (args.Length < 1 || !int.TryParse(args[0], out numWords))
            {
                Console.WriteLine("Usage: StringBuilderTime [numWords]");
                return;
            }

            double[] timings = new double[3];
            Console.WriteLine("Generating {0:N0} random words...", numWords);
            string[] wordList = GenerateWords(numWords);
            
            Console.WriteLine("Timing AppendWithStringConcatenation");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            string result = AppendWithStringConcatenation(wordList);
            watch.Stop();
            timings[0] = watch.Elapsed.TotalSeconds;

            watch.Reset();
            
            Console.WriteLine("Timing AppendWithStringBuilder");
            watch.Start();
            result = AppendWithStringBuilder(wordList);
            watch.Stop();
            timings[1] = watch.Elapsed.TotalSeconds;
            
            watch.Reset();
            int totalLength = 0;
            foreach (string s in wordList) { totalLength += s.Length; }
            Console.WriteLine("Timing AppendWithStringBuilder (preallocate)");
            watch.Start();
            result = AppendWithStringBuilderPreallocate(wordList, totalLength);
            watch.Stop();
            timings[2] = watch.Elapsed.TotalSeconds;
            Console.WriteLine("Timings (s): \t{0}\t{1}\t{2}", timings[0], timings[1], timings[2]);
        }

        private static string AppendWithStringBuilderPreallocate(string[] wordList, int totalLength)
        {
            StringBuilder sb = new StringBuilder(totalLength);
            foreach (string s in wordList)
            {
                sb.Append(s);
            }
            return sb.ToString();
        }

        private static string AppendWithStringBuilder(string[] wordList)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string s in wordList)
            {
                sb.Append(s);
            }
            return sb.ToString();
        }

        private static string AppendWithStringConcatenation(string[] wordList)
        {
            string result = "";
            foreach(string s in wordList)
            {
                result += s;
            }
            return result;
        }

        private static string[] GenerateWords(int numWords)
        {
            string[] list = new string[numWords];
            for (int i = 0; i < numWords; i++)
            {
                list[i] = GenerateWord(rand.Next(3,12));
            }
            return list;
        }

        private static string GenerateWord(int length)
        {
            char[] chars = new char[length];
            for (int i = 0; i < chars.Length; i++)
            {
                chars[i] = (char)rand.Next('a', 'z');
            }
            return new string(chars);
        }
    }
}
