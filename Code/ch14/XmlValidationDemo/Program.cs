using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Xml;
using System.IO;

namespace XmlValidationDemo
{
    class Program
    {
        //this XML fails validation because Author and Title elements
        //are out of order
        const string sourceXml =
            "<?xml version='1.0'?>" +
            "<Book PublishYear=\"2009\">" +
            "<Author>Billy Bob</Author>" +
            "<Title>Programming, art or engineering?</Title>" +
            "</Book>";

        static void Main(string[] args)
        {
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            schemaSet.Add(null, "XmlBookSchema.xsd");
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = schemaSet;
            settings.ValidationEventHandler += new ValidationEventHandler(Settings_ValidationEventHandler);
                
            using (StringReader reader = new StringReader(sourceXml))
            using (XmlReader xmlReader = XmlTextReader.Create(reader, settings))
            {
                while (xmlReader.Read()) ;
            }

            Console.WriteLine("Validation complete");

            Console.ReadKey();
        }

        static void Settings_ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            Console.WriteLine("Validation failed: "+e.Message);
        }
    }
}
