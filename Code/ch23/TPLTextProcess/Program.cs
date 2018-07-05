using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace TPLTextProcess
{
    class Program
    {
        static System.Collections.Concurrent.ConcurrentDictionary<char, UInt64> charCount = 
            new System.Collections.Concurrent.ConcurrentDictionary<char, UInt64>(2, 255);
        static System.Collections.Concurrent.ConcurrentDictionary<string, UInt64> wordCount =
            new System.Collections.Concurrent.ConcurrentDictionary<string, UInt64>(2, 100000, StringComparer.InvariantCultureIgnoreCase);

        static void Main(string[] args)
        {
            string[] inputFiles = 
            {
                "decline1.txt", "decline2.txt", "decline3.txt",
                "decline4.txt", "decline5.txt", "decline6.txt"
            };

            Stopwatch watch = new Stopwatch();
            Console.WriteLine("Iterative");
            watch.Start();
            
            foreach (string file in inputFiles)
            {
                string content = File.ReadAllText(file);
                CountCharacters(content);
                CountWords(content);
            }

            watch.Stop();
            Console.WriteLine("Elapsed: {0}", watch.Elapsed);
            Console.WriteLine("Unique chars: {0}", charCount.Keys.Count);
            Console.WriteLine("Unique words: {0}", wordCount.Keys.Count);

            watch.Reset();

            Console.WriteLine();
            Console.WriteLine("Parallel");
            watch.Start();
            foreach(string file in inputFiles)
            {
                string content = File.ReadAllText(file);
                Parallel.Invoke( 
                    () => CountCharacters(content), 
                    () => CountWords(content)
                    );
            }
            watch.Stop();
            Console.WriteLine("Elapsed: {0}", watch.Elapsed);
            Console.WriteLine("Unique chars: {0}", charCount.Keys.Count);
            Console.WriteLine("Unique words: {0}", wordCount.Keys.Count);

            Console.ReadKey();

        }

        static private void CountCharacters(string content)
        {
            for (int i = 0; i < content.Length; ++i)
            {
                char c = content[i];
                if (charCount.ContainsKey(c))
                {
                    ++charCount[c];
                }
                else 
                {
                    charCount[c] = 1;
                }
            }
        }

        static private void CountWords(string content)
        {
            List<char> splitChars = new List<char>();
            //for simplicity, consider everything that isn't a letter to be a separator
            //since input is ASCII, only need up to 255
            for (int c = 0; c <= 255; ++c)
            {
                if (!char.IsLetter((char)c))
                {
                    splitChars.Add((char)c);
                }
            }
            string[] words = content.Split(splitChars.ToArray<char>(), StringSplitOptions.RemoveEmptyEntries);
            foreach (string word in words)
            {
                if (wordCount.ContainsKey(word))
                {
                    ++wordCount[word];
                }
                else
                {
                    wordCount[word] = 1;
                }
            }
        }
    }
}
