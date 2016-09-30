using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileToolsApp
{
    [Serializable]
    class Person
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public int Age { get; set; }
        public Company Company { get; set; }
        [NonSerialized]
        public string AccNumber { get; set; }
        // стандартный конструктор без параметров
        public Person()
        { }
        public Person(string name, int year)
        {
            Name = name;
            Year = year;
            Company = null;
        }
        public Person(string name, int year, Company comp)
        {
            Name = name;
            Year = year;
            Company = comp;
        }
    }

    [Serializable]
    public class Company
    {
        public string Name { get; set; }

        // стандартный конструктор без параметров
        public Company() { }

        public Company(string name)
        {
            Name = name;
        }
    }
}
