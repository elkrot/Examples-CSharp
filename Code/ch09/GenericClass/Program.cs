using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericClass
{
    class Program
    {
        static void Main(string[] args)
        {
            Indexer<Part> indexer = new Indexer<Part>();
            Part p1 = new Part("1", "Part01", "The first part", 1.5);
            Part p2 = new Part("2", "Part02", "The second part", 2.0);

            indexer.Add(p1.PartId, p1);
            indexer.Add(p2.PartId, p2);

            Part p = indexer.Find("2");
            Console.WriteLine("Found: {0}", p.ToString());

            Console.ReadKey();
        }

        class Temp<T, S>
            where T : IComparable<T>, new()
            where S : class, new() { }
    }
}
