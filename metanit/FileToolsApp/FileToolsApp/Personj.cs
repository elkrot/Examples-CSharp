using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FileToolsApp
{
    [DataContract]
    public class Personj
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Age { get; set; }
        [DataMember]
        public Companyj Company { get; set; }
        public Personj(string name, int year)
        {
            Name = name;
            Age = year;
        }
                public Personj(string name, int age, Companyj comp)
        {
            Name = name;
            Age = age;
            Company = comp;
        }
    }


    public class Companyj
    {
        public string Name { get; set; }

        public Companyj() { }

        public Companyj(string name)
        {
            Name = name;
        }
    }
}
