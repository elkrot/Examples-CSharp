using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace XmlSerializationModDemo
{
    public class Person
    {
        public string FirstName { get; set; }
        [XmlIgnore]
        public char MiddleInitial { get; set; }
        public string LastName { get; set; }

        [XmlElement("DOB")]
        public DateTime BirthDate { get; set; }

        [XmlAttribute("GPA")]
        public double HighschoolGPA { get; set; }

        
        public Address Address { get; set; }

        //to be XML serialized, the type must have a parameterless constructor
        public Person() { }

        public Person(string firstName, char middleInitial, string lastName,
            DateTime birthDate, double highSchoolGpa, Address address)
        {
            this.FirstName = firstName;
            this.MiddleInitial = middleInitial;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.HighschoolGPA = highSchoolGpa;
            this.Address = address;
        }

        public override string ToString()
        {
            return FirstName + " " + MiddleInitial + ". " + LastName + ", DOB:" +
                BirthDate.ToShortDateString() + ", GPA: " + this.HighschoolGPA
                 + Environment.NewLine + Address.ToString();

        }
    }

    //sorry, don't feel like listing out 50 states :)
    public enum StateAbbreviation { RI, VA, SC, CA, TX, UT, WA };

    public class Address
    {
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public StateAbbreviation State { get; set; }
        public string ZipCode { get; set; }

        public Address() { }

        public Address(string addressLine1, string addressLine2,
            string city, StateAbbreviation state, string zipCode)
        {
            this.AddressLine1 = addressLine1;
            this.AddressLine2 = addressLine2;
            this.City = city;
            this.State = state;
            this.ZipCode = zipCode;
        }

        public override string ToString()
        {
            return AddressLine1 + Environment.NewLine + AddressLine2 +
                Environment.NewLine + City + ", " + State + " " + ZipCode;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person("John", 'Q', "Public",
                new DateTime(1776, 7, 4), 3.5,
                new Address("1234 Cherry Lane", null, "Smalltown", StateAbbreviation.VA,
                    "10000"));

            Console.WriteLine("Before serialize:" + Environment.NewLine + person.ToString());

            XmlSerializer serializer = new XmlSerializer(typeof(Person));
            //for demo purposes, just serialize to a string
            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                serializer.Serialize(sw, person);
                Console.WriteLine(Environment.NewLine + "XML:" + Environment.NewLine +
                    sb.ToString() + Environment.NewLine);
            }

            using (StringReader sr = new StringReader(sb.ToString()))
            {
                Person newPerson = serializer.Deserialize(sr) as Person;
                Console.WriteLine("After deserialize:" + Environment.NewLine + newPerson.ToString());
            }
            Console.ReadKey();
        }
    }
}
