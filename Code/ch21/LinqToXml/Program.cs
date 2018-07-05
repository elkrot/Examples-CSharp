using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace LinqToXml
{
    class Program
    {
        static void Main(string[] args)
        {
            //simple query
            XDocument doc = XDocument.Load("LesMis.xml");
            var chaptersWithHe = from chapter in doc.Descendants("Chapter")
                                 where chapter.Value.Contains(" he ")
                                 select chapter.Value;

            Console.WriteLine("Chapters with 'he':");
            foreach (var title in chaptersWithHe)
            {
                Console.WriteLine(title);
            }
            Console.WriteLine();
            
            //create a new XML fragment
            XElement xml = new XElement("Books",
                new XElement("Book",
                    new XAttribute("year",1856),
                    new XElement("Title", "Les Contemplations")),
                new XElement("Book",
                    new XAttribute("year", 1843),
                    new XElement("Title", "Les Burgraves"))
                    );
            
            Console.WriteLine(xml);
            Console.WriteLine();

            Console.ReadKey();
        }
    }
}
