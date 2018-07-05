using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DynamicTypes
{
    class Program
    {
        class Person
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
        }

        class Company
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public bool IsBig { get; set; }
        }
        
        static void Main(string[] args)
        {
            //declare three types that have nothing to do with each other, but all have an Id and Name property
            Person p = new Person() { Id = 1, Name = "Ben", Address = "Redmond, WA" };
            Company c = new Company() { Id = 1313, Name = "Microsoft", IsBig = true };
            var v = new { Id = 13, Name = "Widget", Silly = true };

            PrintInfo(p);
            PrintInfo(c);
            PrintInfo(v);

            try
            {
                PrintInfo(13);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops...can't call PrintInfo(13)");
                Console.WriteLine(ex);
            }

            Console.ReadKey();
        }

        static void PrintInfo(dynamic data)
        {
            //will print anything that has an Id and Name property
            Console.WriteLine("ID: {0}, Name: {1}", data.Id, data.Name);
        }
    }
}
