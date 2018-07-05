using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace TrieDemo
{
    class Info
    {
        static Random rand = new Random();

        public string Key { get; set; }
        public double SomeValue
        {
            get
            {
                return rand.NextDouble();
            }
        }
        public Info(string key)
        {
            this.Key = key;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string[] words = File.ReadAllLines("American-All.txt");

            Trie<Info> trie = new Trie<Info>();

            foreach (string word in words)
            {
                trie.AddValue(word, new Info(word));
            }

            Console.WriteLine("Non-recursive lookup:");
            ICollection<Info> info = trie.FindValues("agonize", false);
           
            foreach (var i in info)
            {
                Console.WriteLine(i.Key);
            }

            Console.WriteLine();
            Console.WriteLine("Recursive lookup:");
            info = trie.FindValues("agonize", true);
            foreach (var i in info)
            {
                Console.WriteLine(i.Key);
            }

            Console.WriteLine();
            Console.WriteLine("Non-existent lookup:");
            info = trie.FindValues("zzfff", true);
            Console.WriteLine("Found {0} values", info.Count);
                        
            Console.ReadKey();
        }
    }
}
