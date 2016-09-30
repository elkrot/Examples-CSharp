// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SampleSupport;
using System.Xml;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace SampleQueries {
    [Title("Примеры запросов 101+ Linq To Xml")]
    [Prefix("XLinq")]
    public class LinqToXmlSamples : SampleHarness {
        public string dataPath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\Data\"));

        [Category("Загрузить")]
        [Title("Загрузить документ из файла")]
        [Description("Загрузить XML-документ из файла")]
        public void XLinq1() {
            XDocument doc = XDocument.Load(dataPath + "bib.xml");
            Console.WriteLine(doc);
        }

        [Category("Загрузить")]
        [Title("Загрузить документ из строки")]
        [Description("Загрузить документ из строки")]
        public void XLinq2() {
            string xml = "<book price='100' isbn='1002310'>" +
                            "<title>XClarity Samples</title>" +
                            "<author>Matt</author>" +
                         "</book>";
            XDocument doc = XDocument.Parse(xml);
            Console.WriteLine(doc);

        }

        //Загрузить XML-документ из XmlReader
        [Category("Загрузить")]
        [Title("Загрузить документ из XmlReader")]
        [Description("Загрузить XML-документ из XmlReader")]
        public void XLinq3() {
            XmlReader reader = XmlReader.Create(dataPath + "bib.xml");
            XDocument doc = XDocument.Load(reader);
            Console.WriteLine(doc);

        }

        [Category("Загрузить")]
        [Title("Элемент из XmlReader - 1")]
        [Description("Формирование XElement из XmlReader, указывающего на элемент")]
        public void XLinq4() {
            XmlReader reader = XmlReader.Create(dataPath + "nw_customers.xml");
            reader.Read();//переместить в корень
            reader.Read(); // переход к первому заказчику
            XElement c = (XElement)XNode.ReadFrom(reader);
            Console.WriteLine(c);

        }

        [Category("Загрузить")]
        [Title("Элемент из XmlReader - 2")]
        [Description("Чтение содержимого XElement из XmlReader")]
        public void XLinq5() {
            XmlReader reader = XmlReader.Create(dataPath + "config.xml");
            //в начале файла есть примечания и пробелы
            reader.Read();
            reader.Read();
            XElement config = new XElement("appSettings",
                                           "This content will be replaced");
            config.RemoveAll();
            while (!reader.EOF)
                config.Add(XNode.ReadFrom(reader));
            Console.WriteLine(config);
        }

        [Category("Создание")]
        [Title("Построить XElement из строки")]
        [Description("Построить XElement из строки")]
        public void XLinq6() {
            string xml = "<purchaseOrder price='100'>" +
                            "<item price='50'>Motor</item>" +
                            "<item price='50'>Cable</item>" +
                          "</purchaseOrder>";
            XElement po = XElement.Parse(xml);
            Console.WriteLine(po);

        }

        [Category("Создание")]
        [Title("Добавить объявление XML в документ")]
        [Description("Добавить объявление XML в документ")]
        public void XLinq7() {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-16", "Yes"),
                                          new XElement("foo"));
            StringWriter sw = new StringWriter();
            doc.Save(sw);
            Console.WriteLine(sw);

        }

        [Category("Создание")]
        [Title("Рассчитанное имя элемента")]
        [Description("Рассчитанное имя элемента")]
        public void XLinq8() {
            XDocument customers = XDocument.Load(dataPath + "nw_customers.xml");
            string name = (string)customers.Elements("Root")
                                           .Elements("Customers")
                                           .First()
                                           .Attribute("CustomerID");
            XElement result = new XElement(name,
                                            "Element with a computed name");
            Console.WriteLine(result);

        }
        [Category("Construction")]
        [Title("Document creation")]
        [Description("Create a simple config file")]
        public void XLinq9() {
            XDocument myDocument =
            new XDocument(
              new XElement("configuration",
                new XElement("system.web",
                  new XElement("membership",
                    new XElement("providers",
                      new XElement("add",
                        new XAttribute("name",
                                       "WebAdminMembershipProvider"),
                        new XAttribute("type",
                                       "System.Web.Administration.WebAdminMembershipProvider")))),
                  new XElement("httpModules",
                    new XElement("add",
                      new XAttribute("name",
                                      "WebAdminModule"),
                      new XAttribute("type",
                                      "System.Web.Administration.WebAdminModule"))),
                  new XElement("authentication",
                    new XAttribute("mode", "Windows")),
                  new XElement("authorization",
                    new XElement("deny",
                      new XAttribute("users", "?"))),
                  new XElement("identity",
                    new XAttribute("impersonate", "true")),
                  new XElement("trust",
                    new XAttribute("level", "full")),
                  new XElement("pages",
                    new XAttribute("validationRequest", "true")))));

            Console.WriteLine(myDocument);

        }

        // <xsd:schema xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
        //             xmlns:sql="urn:schemas-microsoft-com:mapping-schema">

        // <xsd:element name="root" sql:is-constant="1">
        // <xsd:complexType>
        // <xsd:sequence>
        // <xsd:element name="Customers" minOccurs="100" maxOccurs="unbounded">
        //     <xsd:complexType>
        //     <xsd:sequence>
        //         <xsd:element name="CompanyName" type="xsd:string" /> 
        //         <xsd:element name="ContactName" type="xsd:string" /> 
        //         <xsd:element name="ContactTitle" type="xsd:string" /> 
        //         <xsd:element name="Phone" type="xsd:string" /> 
        //         <xsd:element name="Fax" type="xsd:string"/> 
        //         <xsd:element ref="FullAddress" maxOccurs="3"/>
        //         <xsd:element name="Date" type="xsd:date"/>
        //     </xsd:sequence>
        //     <xsd:attribute name="CustomerID" type="xsd:integer" /> 
        //     </xsd:complexType>
        // </xsd:element>
        // </xsd:sequence>
        // </xsd:complexType>
        // </xsd:element>
        // <xsd:element name="FullAddress" sql:relation="Customers" sql:relationship="CustAdd" sql:key-fields="CustomerID" >
        //     <xsd:complexType>
        //     <xsd:sequence>
        //         <xsd:element name="Address" type="xsd:string" /> 
        //         <xsd:element name="City" type="xsd:string" /> 
        //         <xsd:element name="Region" type="xsd:string" /> 
        //         <xsd:element name="PostalCode" type="xsd:string" /> 
        //         <xsd:element name="Country" type="xsd:string" /> 
        //     </xsd:sequence>
        //     </xsd:complexType>
        // </xsd:element>       
        //</xsd:schema>

        [Category("Создание")]
        [Title("Создать схему Xml")]
        [Description("Создать схему Xml")]
        public void XLinq10() {
            XNamespace XSD = "http://www.w3.org/2001/XMLSchema";
            XNamespace SQL = "urn:schemas-microsoft-com:mapping-schema";
            XElement result =
                new XElement(XSD + "schema",
                        new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                        new XAttribute(XNamespace.Xmlns + "sql", "urn:schemas-microsoft-com:mapping-schema"),
                        new XElement(XSD + "element",
                            new XAttribute("name", "root"),
                            new XAttribute(SQL + "is-constant", "1"),
                            new XElement(XSD + "complexType",
                                new XElement(XSD + "sequence",
                                    new XElement(XSD + "element",
                                        new XAttribute("name", "Customers"),
                                        new XAttribute("minOccurs", "100"),
                                        new XAttribute("maxOccurs", "unbounded"),
                                        new XElement(XSD + "complexType",
                                            new XElement(XSD + "sequence",
                                                new XElement(XSD + "element",
                                                    new XAttribute("name", "CompanyName"),
                                                    new XAttribute("type", "xsd:string")),
                                                new XElement(XSD + "element",
                                                    new XAttribute("name", "ContactName"),
                                                    new XAttribute("type", "xsd:string")),
                                                new XElement(XSD + "element",
                                                    new XAttribute("name", "ContactTitle"),
                                                    new XAttribute("type", "xsd:string")),
                                                new XElement(XSD + "element",
                                                    new XAttribute("name", "Phone"),
                                                    new XAttribute("type", "xsd:string")),
                                                new XElement(XSD + "element",
                                                    new XAttribute("name", "Fax"),
                                                    new XAttribute("type", "xsd:string")),
                                                new XElement(XSD + "element",
                                                    new XAttribute("ref", "FullAddress"),
                                                    new XAttribute("maxOccurs", "3")),
                                                new XElement(XSD + "element",
                                                    new XAttribute("name", "Date"),
                                                    new XAttribute("type", "xsd:date"))),
                                            new XElement(XSD + "attribute",
                                                    new XAttribute("name", "CustomerID"),
                                                    new XAttribute("type", "xsd:integer"))))))),
                        new XElement(XSD + "element",
                                new XAttribute("name", "FullAddress"),
                                new XAttribute(SQL + "relation", "Customers"),
                                new XAttribute(SQL + "relationship", "CustAdd"),
                                new XAttribute(SQL + "key-fields", "CustomerID"),
                                new XElement(XSD + "complexType",
                                    new XElement(XSD + "sequence",
                                        new XElement(XSD + "element",
                                            new XAttribute("name", "Address"),
                                            new XAttribute("type", "xsd:string")),
                                        new XElement(XSD + "element",
                                            new XAttribute("name", "City"),
                                            new XAttribute("type", "xsd:string")),
                                        new XElement(XSD + "element",
                                            new XAttribute("name", "Region"),
                                            new XAttribute("type", "xsd:string")),
                                        new XElement(XSD + "element",
                                            new XAttribute("name", "PostalCode"),
                                            new XAttribute("type", "xsd:string")),
                                        new XElement(XSD + "element",
                                            new XAttribute("name", "Country"),
                                            new XAttribute("type", "xsd:string"))))));

            Console.WriteLine(result);

        }

        [Category("Создание")]
        [Title("Построить инструкцию по обработке (PI)")]
        [Description("Создать XML-документ с инструкцией по обработке (PI) XSLT")]
        public void XLinq11() {
            XDocument result = new XDocument(
                                new XProcessingInstruction("xml-stylesheet",
                                                           "type='text/xsl' href='diff.xsl'"),
                                new XElement("foo"));
            Console.WriteLine(result);
        }

        [Category("Создание")]
        [Title("Создание комментария XML")]
        [Description("Создание комментария XML")]
        public void XLinq12() {
            XDocument result =
              new XDocument(
                new XComment("My phone book"),
                new XElement("phoneBook",
                  new XComment("My friends"),
                  new XElement("Contact",
                    new XAttribute("name", "Ralph"),
                    new XElement("homephone", "425-234-4567"),
                    new XElement("cellphone", "206-345-75656")),
                  new XElement("Contact",
                    new XAttribute("name", "Dave"),
                    new XElement("homephone", "516-756-9454"),
                    new XElement("cellphone", "516-762-1546")),
                  new XComment("My family"),
                  new XElement("Contact",
                    new XAttribute("name", "Julia"),
                    new XElement("homephone", "425-578-1053"),
                    new XElement("cellphone", "")),
                  new XComment("My team"),
                  new XElement("Contact",
                    new XAttribute("name", "Robert"),
                    new XElement("homephone", "345-565-1653"),
                    new XElement("cellphone", "321-456-2567"))));

            Console.WriteLine(result);
        }

        [Category("Создание")]
        [Title("Создать секцию CData")]
        [Description("Создать секцию CData")]
        public void XLinq13() {
            XElement e = new XElement("Dump",
                          new XCData("<dump>this is some xml</dump>"),
                          new XText("some other text"));
            Console.WriteLine("Element Value: {0}", e.Value);
            Console.WriteLine("Text nodes collapsed!: {0}", e.Nodes().First());
            Console.WriteLine("CData preserved on serialization: {0}", e);
        }

        [Category("Создание")]
        [Title("Создать последовательность узлов")]
        [Description("Создать последовательность элементов заказчика")]
        public void XLinq14() {
            var cSequence = new[] {
                    new XElement("customer",
                                 new XAttribute("id","x"),"new customer"),
                    new XElement("customer",
                                 new XAttribute("id","y"),"new customer"),
                    new XElement("customer",
                                 new XAttribute("id","z"),"new customer")};
            foreach (var c in cSequence)
                Console.WriteLine(c);

        }

        [Category("Запись")]
        [Title("Записать элемент XElement в XmlWriter")]
        [Description("Записать элемент XElement в XmlWriter с помощью методом WriteTo")]
        public void XLinq15() {
            XElement po1 = new XElement("PurchaseOrder",
                            new XElement("Item", "Motor",
                              new XAttribute("price", "100")));

            XElement po2 = new XElement("PurchaseOrder",
                            new XElement("Item", "Cable",
                              new XAttribute("price", "10")));

            XElement po3 = new XElement("PurchaseOrder",
                            new XElement("Item", "Switch",
                              new XAttribute("price", "10")));

            StringWriter sw = new StringWriter();
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter w = XmlWriter.Create(sw, settings);
            w.WriteStartElement("PurchaseOrders");

            po1.WriteTo(w);
            po2.WriteTo(w);
            po3.WriteTo(w);

            w.WriteEndElement();
            w.Close();
            Console.WriteLine(sw.ToString());
        }


        [Category("Запись")]
        [Title("Запись содержимого XDocument в XmlWriter")]
        [Description("Запись содержимого XDocument в XmlWriter при помощи метода WriteTo")]
        public void XLinq16() {
            XDocument doc1 = new XDocument(
              new XElement("PurchaseOrders",
                new XElement("PurchaseOrder",
                  new XElement("Item", "Motor",
                    new XAttribute("price", "100"))),
                new XElement("PurchaseOrder",
                  new XElement("Item", "Cable",
                    new XAttribute("price", "10")))));
            XDocument doc2 = new XDocument(
              new XElement("PurchaseOrders",
                new XElement("PurchaseOrder",
                  new XElement("Item", "Switch",
                    new XAttribute("price", "10")))));

            StringWriter sw = new StringWriter();

            XmlWriter w = XmlWriter.Create(sw);

            w.WriteStartDocument();
            w.WriteStartElement("AllPurchaseOrders");
            doc1.Root.WriteTo(w);
            doc2.Root.WriteTo(w);
            w.WriteEndElement();
            w.WriteEndDocument();
            w.Close();
            Console.WriteLine(sw.ToString());
        }


        [Category("Запись")]
        [Title("Сохранить XDocument")]
        [Description("Сохранение XDocument при помощи XmlWriter/TextWriter/File")]
        public void XLinq17() {
            XDocument doc = new XDocument(
                new XElement("PurchaseOrders",
                    new XElement("PurchaseOrder",
                      new XElement("Item",
                        "Motor",
                        new XAttribute("price", "100"))),
                    new XElement("PurchaseOrder",
                      new XElement("Item",
                        "Switch",
                        new XAttribute("price", "10"))),
                    new XElement("PurchaseOrder",
                      new XElement("Item",
                        "Cable",
                        new XAttribute("price", "10")))));

            StringWriter sw = new StringWriter();
            //сохранить в XmlWriter
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            XmlWriter w = XmlWriter.Create(sw, settings);
            doc.Save(w);
            w.Close();
            Console.WriteLine(sw.ToString());

            //сохранить в файл
            doc.Save("out.xml");

        }

        [Category("Запрос")]
        [Title("Запросить дочерние элементы")]
        [Description("Выбрать всех клиентов в xml-документе")]
        public void XLinq18() {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");
            foreach (XElement result in doc.Elements("Root")
                                           .Elements("Customers"))
                Console.WriteLine(result);

        }

        [Category("Запрос")]
        [Title("Запросить все дочерние элементы")]
        [Description("Выбрать все дочерние элементы первого клиента")]
        public void XLinq19() {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");
            var query = doc.Element("Root")
                           .Element("Customers")
                           .Elements();
            foreach (XElement result in query)
                Console.WriteLine(result);
        }

        [Category("Запрос")]
        [Title("Запросить первый дочерний элемент - 1")]
        [Description("Выбрать первого клиента в документе")]
        public void XLinq20() {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");
            var result = doc.Element("Root")
                            .Element("Customers");
            Console.WriteLine(result);
        }

        [Category("Запрос")]
        [Title("Запросить первый дочерний элемент - 2")]
        [Description("Запросить один дочерний элемент в последовательности элементов")]
        public void XLinq21() {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");
            var result = doc.Elements()
                            .Elements("Customers")
                            .First()
                            .Element("CompanyName");
            Console.WriteLine(result);
        }

        [Category("Запрос")]
        [Title("Запросить атрибуты")]
        [Description("Выбор всех идентификаторов CustomerID в xml-документе")]
        public void XLinq22() {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");
            var query = doc.Element("Root")
                           .Elements("Customers")
                           .Attributes("CustomerID");
            foreach (XAttribute result in query)
                Console.WriteLine(result.Name + " = " + result.Value);

        }

        [Category("Запрос")]
        [Title("Привести атрибут к числу")]
        [Description("Найти заказы с ценой > 100")]
        public void XLinq23() {
            string xml = "<order >" +
                            "<item price='150'>Motor</item>" +
                            "<item price='50'>Cable</item>" +
                            "<item price='50'>Modem</item>" +
                            "<item price='250'>Monitor</item>" +
                            "<item price='10'>Mouse</item>" +
                        "</order>";
            XElement order = XElement.Parse(xml);
            var query =
                from
                    i in order.Elements("item")
                where
                    (int)i.Attribute("price") > 100
                select i;
            foreach (var result in query)
                Console.WriteLine("Expensive Item {0} costs {1}",
                                  (string)result,
                                  (string)result.Attribute("price"));
        }

        [Category("Запрос")]
        [Title("Получить корневой элемент документа")]
        [Description("Получить корневой элемент документа")]
        public void XLinq24() {
            XElement root = XDocument.Load(dataPath + "config.xml")
                                     .Root;
            Console.WriteLine("Name of root element is {0}", root.Name);
        }

        [Category("Запрос")]
        [Title("Фильтровать результаты запроса с использованием where")]
        [Description("Фильтровать результаты запроса с использованием where")]
        public void XLinq25() {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");
            var query =
                    from
                        c in doc.Element("Root")
                                .Elements("Customers")
                    where
                        c.Element("FullAddress")
                         .Element("Country")
                         .Value == "Germany"
                    select c;

            foreach (XElement result in query)
                Console.WriteLine(result);
        }

        [Category("Запрос")]
        [Title("Выбрать всех потомков элемента")]
        [Description("Выбрать все элементы имен контактов в документе")]
        public void XLinq26() {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");
            var query = doc.Descendants("ContactName");
            foreach (XElement result in query)
                Console.WriteLine(result);
        }

        [Category("Запрос")]
        [Title("Выбрать всех потомков данного типа")]
        [Description("Выделить весь текст в документе")]
        public void XLinq27() {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");
            var query = doc.DescendantNodes().OfType<XText>().Select(t => t.Value);
            foreach (string result in query)
                Console.WriteLine(result);
        }

        [Category("Запрос")]
        [Title("Выбрать всех предков")]
        [Description("Проверить, принадлежат ли два узла одному документу")]
        public void XLinq28() {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");
            XElement element1 = doc.Element("Root");
            XElement element2 = doc.Descendants("Customers")
                                .ElementAt(3);
            var query = from a in element1.AncestorsAndSelf()
                        from b in element2.AncestorsAndSelf()
                        where a == b
                        select a;
            Console.WriteLine(query.Any());
        }

        [Category("Запрос")]
        [Title("Запросить родительский элемент")]
        [Description("Запросить родительский элемент для элемента")]
        public void XLinq29() {
            XElement item = new XElement("item-01",
                                         "Computer");
            XElement order = new XElement("order", item);
            XElement p = item.Parent;
            Console.WriteLine(p.Name);
        }

        [Category("Запрос")]
        [Title("Соединение через две последовательности")]
        [Description("Добавить сведения о компании клиента в заказы первого клиента")]
        public void XLinq30() {
            XDocument customers = XDocument.Load(dataPath + "nw_customers.xml");
            XDocument orders = XDocument.Load(dataPath + "nw_orders.xml");

            var query =
                from customer in customers.Descendants("Customers").Take(1)
                join order in orders.Descendants("Orders")
                           on (string)customer.Attribute("CustomerID") equals
                              (string)order.Element("CustomerID")
                select
                    new XElement("Order",
                                order.Nodes(),
                                customer.Element("CompanyName"));

            foreach (var result in query)
                Console.WriteLine(result);

        }

        [Category("Запрос")]
        [Title("Запросить содержимое типа")]
        [Description("Содержимое запроса заданного типа для существующего элемента")]
        public void XLinq31() {
            XElement elem =
              new XElement("customer",
                           new XElement("name",
                                        "jack"),
                           "some text",
                           new XComment("new customer"),
                           new XAttribute("id",
                                          "abc"));

            //строковое содержимое
            foreach (XText s in elem.Nodes().OfType<XText>())
                Console.WriteLine("String content: {0}", s);

            //содержимое элемента
            foreach (XElement e in elem.Elements())
                Console.WriteLine("Element content: {0}", e);

            //содержимое комментария
            foreach (XComment c in elem.Nodes().OfType<XComment>())
                Console.WriteLine("Comment content: {0}", c);

        }

        [Category("Запрос")]
        [Title("Запрос с использованием XStreamingElement")]
        [Description("Запрос для всех заказов шведских клиентов и шведских заказов с ценой доставки > 250")]
        [LinkedMethod("GetSwedishFreightProfile")]
        [LinkedMethod("GetSwedishCustomerOrders")]
        [LinkedMethod("AddNewOrder")]
        public void XLinq32() {
            XDocument customers = XDocument.Load(dataPath + "nw_customers.xml");
            XDocument orders = XDocument.Load(dataPath + "nw_orders.xml");
            XStreamingElement summary = new XStreamingElement("Summary",
                new XAttribute("Country", "Sweden"),
                new XStreamingElement("SwedishCustomerOrders", GetSwedishCustomerOrders(customers, orders)),
                new XStreamingElement("Orders", GetSwedishFreightProfile(orders)));

            Console.WriteLine(summary);
            //Операция DML, добавляющая новый заказ для заказчика BERGS, транспортные расходы > 250  
            AddNewOrder(orders);
            Console.WriteLine("****XStreaming Output after DML reflects new order added!!****");
            Console.WriteLine(summary);
        }

        static void AddNewOrder(XDocument orders) {
            string order = @"<Orders>
                                   <CustomerID>BERGS</CustomerID>
                                   <ShipInfo ShippedDate='1997-12-09T00:00:00'>
                                   <Freight>301</Freight>
                                   <ShipCountry>Sweden</ShipCountry>
                                   </ShipInfo>
                                   </Orders>";
            XElement newOrder = XElement.Parse(order);
            orders.Root.Add(newOrder);
        }

        private static IEnumerable<XElement> GetSwedishFreightProfile(XDocument orders) {
            return
               from
                order in orders.Descendants("Orders")
               where
                (string)order.Element("ShipInfo").Element("ShipCountry") == "Sweden"
                  && (float)order.Element("ShipInfo").Element("Freight") > 250
               select
                  new XElement("Order",
                  new XAttribute("Freight",
                                 (string)order.Element("ShipInfo").Element("Freight")));

        }

        private static IEnumerable<XElement> GetSwedishCustomerOrders(XDocument customers, XDocument orders) {
            return
               from
                customer in customers.Descendants("Customers")
               where
                (string)customer.Element("FullAddress").Element("Country") == "Sweden"
               select
                 new XElement("Customer",
                 new XAttribute("Name",
                                (string)customer.Element("CompanyName")),
                 new XAttribute("OrderCount",
                                (from
                                  order in orders.Descendants("Orders")
                                 where
                                   (string)order.Element("CustomerID") == (string)customer.Attribute("CustomerID")
                                 select
                                   order).Count()));

        }

        [Category("Запрос")]
        [Title("Позиционный предикат")]
        [Description("Запрос 3-го заказчика в документе")]
        public void XLinq33() {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");
            var c = doc.Descendants("Customers")
                       .ElementAt(2);
            Console.WriteLine(c);
        }

        [Category("Запрос")]
        [Title("Объединение двух последовательностей узлов")]
        [Description("Книги объединения написаны двумя авторами: Сергеем и Петром")]
        public void XLinq34() {
            XDocument doc = XDocument.Load(dataPath + "bib.xml");
            var b1 = doc.Descendants("book")
                        .Where(b => b.Elements("author")
                                     .Elements("first")
                                     .Any(f => (string)f == "Serge"));
            var b2 = doc.Descendants("book")
                        .Where(b => b.Elements("author")
                                     .Elements("first")
                                     .Any(f => (string)f == "Peter"));
            var books = b1.Union(b2);
            foreach (var b in books)
                Console.WriteLine(b);
        }

        [Category("Запрос")]
        [Title("Пересечение двух последовательностей узлов")]
        [Description("Книги, общие для обоих авторов")]
        public void XLinq35() {
            XDocument doc = XDocument.Load(dataPath + "bib.xml");
            var b1 = doc.Descendants("book")
                        .Where(b => b.Elements("author")
                                        .Elements("first")
                                        .Any(f => (string)f == "Serge"));
            var b2 = doc.Descendants("book")
                        .Where(b => b.Elements("author")
                                        .Elements("first")
                                        .Any(f => (string)f == "Peter"));
            var books = b1.Intersect(b2);
            foreach (var b in books)
                Console.WriteLine(b);
        }

        [Category("Запрос")]
        [Title("Все узлы в последовательности 1 за исключением узлов в последовательности 2")]
        [Description("Найти книги, автором которых является Петр без соавтора Сергей")]
        public void XLinq36() {
            XDocument doc = XDocument.Load(dataPath + "bib.xml");
            var b1 = doc.Descendants("book")
                        .Where(b => b.Elements("author")
                                     .Elements("first")
                                     .Any(f => (string)f == "Serge"));
            var b2 = doc.Descendants("book")
                        .Where(b => b.Elements("author")
                                     .Elements("first")
                                     .Any(f => (string)f == "Peter"));
            var books = b2.Except(b1);
            foreach (var b in books)
                Console.WriteLine(b);
        }

        [Category("Запрос")]
        [Title("Изменить на обратный порядок узлов в последовательности")]
        [Description("Отобразить путь к узлу")]
        [LinkedMethod("PrintPath")]
        public void XLinq37() {
            XDocument doc = XDocument.Load(dataPath + "bib.xml");
            XElement e = doc.Descendants("last")
                            .First();
            PrintPath(e);
        }

        static void PrintPath(XElement e) {
            var nodes = e.AncestorsAndSelf()
                            .Reverse();
            foreach (var n in nodes)
                Console.Write(n.Name + (n == e ? "" : "->"));
        }


        [Category("Запрос")]
        [Title("Равенство последовательностей")]
        [Description("Проверить 2 последовательности узлов на идентичность. " +
                     "Являются ли Сергей и Петр соавторами всех их книг?")]
        public void XLinq38() {
            XDocument doc = XDocument.Load(dataPath + "bib.xml");
            var b1 = doc.Descendants("book")
                        .Where(b => b.Elements("author")
                                        .Elements("first")
                                        .Any(f => (string)f == "Serge"));
            var b2 = doc.Descendants("book")
                        .Where(b => b.Elements("author")
                                        .Elements("first")
                                        .Any(f => (string)f == "Peter"));
            bool result = b2.SequenceEqual(b1);
            Console.WriteLine(result);
        }


        [Category("Запрос")]
        [Title("Оператор TakeWhile")]
        [Description("List books until total price is less that $150")]
        public void XLinq39() {
            XDocument doc = XDocument.Load(dataPath + "bib.xml");
            double sum = 0;
            var query = doc.Descendants("book")
                           .TakeWhile(c => (sum += (double)c.Element("price")) <= 150);
            foreach (var result in query)
                Console.WriteLine(result);
        }

        [Category("Запрос")]
        [Title("Создать список номеров")]
        [Description("Создать 5 новых заказчиков с разными ИД")]
        public void XLinq40() {
            var query = from
                           i in Enumerable.Range(1, 5)
                        select
                            new XElement("Customer",
                                         new XAttribute("id", i),
                                         "New customer");
            foreach (var result in query)
                Console.WriteLine(result);
        }

        [Category("Запрос")]
        [Title("Оператор Repeat")]
        [Description("Инициализировать новые заказы с позициями")]
        public void XLinq41() {
            var orders = new XElement[] {
                new XElement("order", new XAttribute("itemCount",5)),
                new XElement("order", new XAttribute("itemCount",2)),
                new XElement("order", new XAttribute("itemCount",3))};

            //добавить пустые элементы
            foreach (var order in orders)
                order.Add(Enumerable.Repeat(new XElement("item", "New item"),
                                          (int)order.Attribute("itemCount")));

            foreach (var o in orders)
                Console.WriteLine(o);

        }

        [Category("Запрос")]
        [Title("Оператор Any")]
        [Description("Проверить, имеются ли заказчики в Аргентине")]
        public void XLinq42() {
            XDocument doc = XDocument.Load(dataPath + "nw_Customers.xml");
            if (doc.Descendants("Country").Any(c => (string)c == "Argentina"))
                Console.WriteLine("There are cusotmers in Argentina");
            else
                Console.WriteLine("There are no cusotmers in Argentina");
        }

        [Category("Запрос")]
        [Title("Оператор All")]
        [Description("Проверить, имеется ли у всех книг хотя бы один автор")]
        public void XLinq43() {
            XDocument doc = XDocument.Load(dataPath + "bib.xml");
            bool query = doc.Descendants("book")
                            .All(b => b.Descendants("author").Count() > 0);
            if (query)
                Console.WriteLine("All books have authors");
            else
                Console.WriteLine("Some books dont have authors");
        }

        [Category("Запрос")]
        [Title("Оператор Count")]
        [Description("Найти число заказов для данного клиента")]
        public void XLinq44() {
            XDocument doc = XDocument.Load(dataPath + "nw_Orders.xml");
            var query = doc.Descendants("Orders")
                           .Where(o => (string)o.Element("CustomerID") == "VINET");
            Console.WriteLine("Customer has {0} orders", query.Count());
        }

        [Category("Запрос")]
        [Title("Статистический оператор")]
        [Description("Найти налог на заказ")]
        [LinkedMethod("Tax")]
        public void XLinq45() {
            string xml = "<order >" +
                            "<item price='150'>Motor</item>" +
                            "<item price='50'>Cable</item>" +
                         "</order>";

            XElement order = XElement.Parse(xml);
            double tax = order.Elements("item")
                              .Aggregate((double)0, Tax);

            Console.WriteLine("The total tax on the order @10% is ${0}", tax);

        }
        static double Tax(double seed, XElement item) {
            return seed + (double)item.Attribute("price") * 0.1;
        }

        [Category("Запрос")]
        [Title("Оператор Distinct")]
        [Description("Найти все страны, в которых имеются заказчики")]
        public void XLinq46() {
            XDocument doc = XDocument.Load(dataPath + "nw_Customers.xml");
            var countries = doc.Descendants("Country")
                               .Select(c => (string)c)
                               .Distinct()
                               .OrderBy(c => c);
            foreach (var c in countries)
                Console.WriteLine(c);
        }

        [Category("Запрос")]
        [Title("Оператор Concat")]
        [Description("Перечислить все книги Сергея и Петра с повторяющимися книгами в соавторстве")]
        public void XLinq47() {
            XDocument doc = XDocument.Load(dataPath + "bib.xml");
            var b1 = doc.Descendants("book")
                        .Where(b => b.Elements("author")
                                     .Elements("first")
                                     .Any(f => (string)f == "Serge"));
            var b2 = doc.Descendants("book")
                        .Where(b => b.Elements("author")
                                     .Elements("first")
                                     .Any(f => (string)f == "Peter"));
            var books = b1.Concat(b2);
            foreach (var b in books)
                Console.WriteLine(b);

        }
        [Category("Запрос")]
        [Title("Оператор Take")]
        [Description("Запросить первых двух заказчиков")]
        public void XLinq48() {
            XDocument doc = XDocument.Load(dataPath + "nw_Customers.xml");
            var customers = doc.Descendants("Customers").Take(2);
            foreach (var c in customers)
                Console.WriteLine(c);
        }

        [Category("Запрос")]
        [Title("Оператор Skip")]
        [Description("Пропустить первые 3 книги")]
        public void XLinq49() {
            XDocument doc = XDocument.Load(dataPath + "bib.xml");
            var books = doc.Descendants("book").Skip(3);
            foreach (var b in books)
                Console.WriteLine(b);
        }

        [Category("Запрос")]
        [Title("Пропуск узлов с учетом условия")]
        [Description("Печать элементов, не уложившихся в бюджет")]
        public void XLinq50() {
            string xml = "<order >" +
                            "<item price='150'>Motor</item>" +
                            "<item price='50'>Cable</item>" +
                            "<item price='50'>Modem</item>" +
                            "<item price='250'>Monitor</item>" +
                            "<item price='10'>Mouse</item>" +
                        "</order>";
            XElement order = XElement.Parse(xml);
            int sum = 0;
            var items = order.Descendants("item")
                             .SkipWhile(i => (sum += (int)i.Attribute("price")) < 300);
            foreach (var i in items)
                Console.WriteLine("{0} does not fit in you budget", (string)i);
        }

        [Category("Запрос")]
        [Title("Оператор SelectMany")]
        [Description("Получить все книги с именами авторов Сергей и Петр")]
        [LinkedMethod("GetBooks")]
        public void XLinq51() {
            string[] authors = { "Serge", "Peter" };
            var books = authors.SelectMany(a => GetBooks(a));
            foreach (var b in books)
                Console.WriteLine(b);

        }

        public IEnumerable<XElement> GetBooks(string author) {
            XDocument doc = XDocument.Load(dataPath + "bib.xml");
            var query = doc.Descendants("book")
                            .Where(b => b.Elements("author")
                                         .Elements("first")
                                         .Any(f => (string)f == author));

            return query;
        }

        [Category("Запрос")]
        [Title("Документ-контейнер")]
        [Description("Найдите документ, содержащий элемент")]
        public void XLinq52() {
            XElement c = XDocument.Load(dataPath + "bib.xml").Descendants("book").First();
            XDocument container = c.Document;
            Console.WriteLine(container);
        }

        [Category("Группировка")]
        [Title("Группировать заказы по заказчикам")]
        [Description("Группировать заказы по заказчикам")]
        public void XLinq53() {
            XDocument doc = XDocument.Load(dataPath + "nw_orders.xml");
            var query =
                from
                    o in doc.Descendants("Orders")
                group o by
                    (string)o.Element("CustomerID") into oGroup
                select
                    oGroup;
            foreach (var result in query.SelectMany(g => g))
                Console.WriteLine(result);
        }

        [Category("Группировка")]
        [Title("Группировать заказчиков по странам и городам")]
        [Description("Создание каталога пользователей, сгруппированных по странам и городам")]
        public void XLinq54() {
            XDocument customers = XDocument.Load(dataPath + "nw_customers.xml");
            XElement directory =
             new XElement("directory",
                from customer in customers.Descendants("Customers")
                from country in customer.Elements("FullAddress").Elements("Country")
                group customer by (string)country into countryGroup
                let country = countryGroup.Key
                select
                    new XElement("Country",
                                 new XAttribute("name",
                                                country),
                                 new XAttribute("numberOfCustomers",
                                                countryGroup.Count()),
                                 from customer in countryGroup
                                 from city in customer.Descendants("City")
                                 group customer by (string)city into cityGroup
                                 let city = cityGroup.Key
                                 select
                                    new XElement("City",
                                                 new XAttribute("name",
                                                                city),
                                                 new XAttribute("numberOfCustomers",
                                                                cityGroup.Count()),
                                                                cityGroup.Elements("ContactName"))));
            Console.WriteLine(directory);

        }

        [Category("Группировка")]
        [Title("Группировать заказы по заказчикам")]
        [Description("Группировать заказы по клиентам и вернуть всех клиентов (и заказы) для клиентов, имеющих более 25 заказов ")]
        public void XLinq55() {
            XDocument customers = XDocument.Load(dataPath + "nw_customers.xml");
            XDocument orders = XDocument.Load(dataPath + "nw_orders.xml");
            XElement custOrder = new XElement("CustomerOrders",
                from
                    order in orders.Descendants("Orders")
                group order by
                    order.Element("CustomerID") into cust_orders
                where
                    cust_orders.Count() > 25
                select
                    new XElement("Customer",
                        new XAttribute("CustomerID", cust_orders.Key.Value),
                        from
                            customer in customers.Descendants("Customers")
                        where
                            (string)customer.Attribute("CustomerID") == (string)cust_orders.Key.Value
                        select
                            customer.Nodes(),
                        cust_orders));

            Console.WriteLine(custOrder);
        }

        [Category("Сортировка")]
        [Title("Сортировка клиентов по имени")]
        [Description("Сортировка клиентов по имени по возрастанию")]
        public void XLinq56() {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");
            var query =
                from
                    customer in doc.Descendants("Customers")
                orderby
                    (string)customer.Elements("ContactName").First()
                select
                    customer.Element("ContactName");
            XElement result =
                new XElement("SortedCustomers", query);
            Console.WriteLine(result);
        }

        [Category("Сортировка")]
        [Title("Сортировка заказов по дате")]
        [Description("Сортировка заказов по дате по возрастанию")]
        public void XLinq57() {
            XDocument doc = XDocument.Load(dataPath + "nw_orders.xml");
            var query =
                from order in doc.Descendants("Orders")
                let date = (DateTime)order.Element("OrderDate")
                orderby date
                select
                    new XElement("Order",
                                 new XAttribute("date",
                                                date),
                                 new XAttribute("custid",
                                                (string)order.Element("CustomerID")));
            XElement result = new XElement("SortedOrders", query);
            Console.WriteLine(result);

        }

        [Category("Сортировка")]
        [Title("По убыванию")]
        [Description("Сортировка клиентов по имени по убыванию")]
        public void XLinq58() {
            XDocument doc = XDocument.Load(dataPath + "nw_Customers.xml");
            var query =
                from
                    customer in doc.Descendants("Customers")
                orderby
                    (string)customer.Elements("ContactName").First() descending
                select
                    customer.Element("ContactName");
            foreach (var result in query)
                Console.WriteLine(result);

        }

        [Category("Сортировка")]
        [Title("Несколько ключей сортировки")]
        [Description("Сортировка клиентов по странам и городам")]
        public void XLinq59() {
            XDocument doc = XDocument.Load(dataPath + "nw_Customers.xml");
            var query =
                from
                    customer in doc.Descendants("Customers")
                orderby
                    (string)customer.Descendants("Country").First(),
                    (string)customer.Descendants("City").First()
                select
                    customer;
            foreach (var result in query)
                Console.WriteLine(result);

        }
        [Category("DML")]
        [Title("Добавить элемент как последний дочерний объект")]
        [Description("Добавить элемент как последний дочерний объект")]
        public void XLinq60() {
            XDocument doc = XDocument.Load(dataPath + "config.xml");
            XElement config = doc.Element("config");
            config.Add(new XElement("logFolder", "c:\\log"));
            Console.WriteLine(config);

        }

        [Category("DML")]
        [Title("Добавить элемент как первый дочерний объект")]
        [Description("Добавить элемент как первый дочерний объект")]
        public void XLinq61() {
            XDocument doc = XDocument.Load(dataPath + "config.xml");
            XElement config = doc.Element("config");
            config.AddFirst(new XElement("logFolder", "c:\\log"));
            Console.WriteLine(config);

        }

        [Category("DML")]
        [Title("Добавить несколько элементов как дочерние")]
        [Description("Добавить несколько элементов как дочерние")]
        public void XLinq62() {
            XDocument doc = XDocument.Load(dataPath + "config.xml");
            XElement[] first = { 
                new XElement("logFolder", "c:\\log"), 
                new XElement("resultsFolders", "c:\\results")};
            XElement[] last = { 
                new XElement("mode", "client"), 
                new XElement("commPort", "2")};
            XElement config = doc.Element("config");
            config.AddFirst(first);
            config.Add(last);
            Console.WriteLine(config);
        }


        [Category("DML")]
        [Title("Добавить атрибут к элементу")]
        [Description("Добавить атрибут к элементу")]
        public void XLinq63() {
            XElement elem = new XElement("customer",
                                         "this is an XElement",
                                         new XAttribute("id", "abc"));
            Console.WriteLine("Original element {0}", elem);

            elem.Add(new XAttribute("name", "Jack"));
            Console.WriteLine("Updated element {0}", elem);
        }


        [Category("DML")]
        [Title("Добавить содержимое в существующий элемент")]
        [Description("Добавить атрибуты и элементы")]
        public void XLinq64() {
            XElement elem = new XElement("customer",
                                         "this is an XElement",
                                         new XAttribute("id", "abc"));
            Console.WriteLine("Original element {0}", elem);

            object[] additionalContent = { 
                new XElement("phone", "555-555-5555"), 
                new XComment("new customer"), 
                new XAttribute("name", "Jack")};

            elem.Add(additionalContent);
            Console.WriteLine("Updated element {0}", elem);
        }

        [Category("DML")]
        [Title("Заменить содержимое контейнера (элемент или документ")]
        [Description("Заменить содержимое в существующем элементе")]
        public void XLinq65() {
            XElement elem = new XElement("customer",
                                         "this is an XElement",
                                         new XAttribute("id", "abc"));
            Console.WriteLine("Original element {0}", elem);

            elem.ReplaceNodes("this is a coustomer element");
            Console.WriteLine("Updated element {0}", elem);

            object[] newContent = { 
                "this is a customer element", 
                new XElement("phone", "555-555-5555"), 
                new XComment("new customer"), 
                new XAttribute("name", "Jack") };

            elem.ReplaceNodes(newContent);
            Console.WriteLine("Updated element {0}", elem);

        }

        [Category("DML")]
        [Title("Удалить содержимое элемента")]
        [Description("Удалить содержимое элемента")]
        public void XLinq66() {
            XElement elem = new XElement("customer",
                                         "this is an XElement",
                                         new XAttribute("id", "abc"));
            Console.WriteLine("Original element {0}", elem);

            elem.RemoveNodes();
            Console.WriteLine("Updated element {0}", elem);

        }

        [Category("DML")]
        [Title("Удалить все содержимое")]
        [Description("Удалить все содержимое и атрибуты элемента")]
        public void XLinq67() {
            XElement elem = new XElement("customer",
                                         new XElement("name", "jack"),
                                         "this is an XElement",
                                         new XComment("new customer"),
                                         new XAttribute("id", "abc"));
            Console.WriteLine("Original element {0}", elem);

            elem.RemoveAll();
            Console.WriteLine("Stripped element {0}", elem);
        }

        [Category("DML")]
        [Title("Удалить все атрибуты")]
        [Description("Удалить все атрибуты элемента")]
        public void XLinq68() {
            XElement elem = new XElement("customer",
                                         new XAttribute("name", "jack"),
                                         "this is an XElement",
                                         new XComment("new customer"),
                                         new XAttribute("id", "abc"));
            Console.WriteLine("Original element {0}", elem);

            elem.RemoveAttributes();
            Console.WriteLine("Stripped element {0}", elem);

        }

        [Category("DML")]
        [Title("Удалить атрибут элемента")]
        [Description("Удалить атрибут элемента")]
        public void XLinq69() {
            XElement elem = new XElement("customer",
                                         new XAttribute("name", "jack"),
                                         "this is an XElement",
                                         new XComment("new customer"),
                                         new XAttribute("id", "abc"));
            Console.WriteLine("Original element {0}", elem);

            elem.SetAttributeValue("id", null);
            Console.WriteLine("Updated element {0}", elem);

        }

        [Category("DML")]
        [Title("Обновить атрибут")]
        [Description("Обновить значение атрибута")]
        public void XLinq70() {
            XElement elem = new XElement("customer",
                                         new XAttribute("name", "jack"),
                                         "this is an XElement",
                                         new XComment("new customer"),
                                         new XAttribute("id", "abc"));
            Console.WriteLine("Original element {0}", elem);

            elem.SetAttributeValue("name", "David");
            Console.WriteLine("Updated attribute {0}", elem);

        }


        [Category("DML")]
        [Title("Удалить элемент по имени")]
        [Description("Удалить дочерний элемент по имени")]
        public void XLinq71() {
            XElement elem = new XElement("customer",
                                         new XElement("name", "jack"),
                                         "this is an XElement",
                                         new XComment("new customer"),
                                         new XAttribute("id", "abc"));
            Console.WriteLine("Original element {0}", elem);

            elem.SetElementValue("name", null);
            Console.WriteLine("Updated element {0}", elem);
        }

        [Category("DML")]
        [Title("Обновление дочернего элемента по имени")]
        [Description("Обновление дочернего элемента по имени")]
        public void XLinq72() {
            XElement elem = new XElement("customer",
                                         new XElement("name", "jack"),
                                         "this is an XElement",
                                         new XComment("new customer"),
                                         new XAttribute("id", "abc"));
            Console.WriteLine("Original element {0}", elem);

            elem.SetElementValue("name", "David");
            Console.WriteLine("Updated element {0}", elem);
        }

        [Category("DML")]
        [Title("Удалить список элементов")]
        [Description("Удалить список элементов")]
        public void XLinq73() {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");
            var elems = doc.Descendants("Customers");
            Console.WriteLine("Before count {0}", elems.Count());

            elems.Take(15).Remove();
            Console.WriteLine("After count {0}", elems.Count());
        }

        [Category("DML")]
        [Title("Удалить список атрибутов")]
        [Description("Удалить список атрибутов")]
        public void XLinq74() {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");
            var attrs = doc.Descendants("Customers")
                           .Attributes();
            Console.WriteLine("Before count {0}", attrs.Count());

            attrs.Take(15).Remove();
            Console.WriteLine("After count {0}", attrs.Count());
        }


        [Category("DML")]
        [Title("Добавить элемент без родителей в элемент")]
        [Description("Добавить элемент без родителей в элемент")]
        public void XLinq75() {
            XElement e = new XElement("foo",
                                      "this is an element");
            Console.WriteLine("Parent : " +
                (e.Parent == null ? "null" : e.Parent.Value));

            XElement p = new XElement("bar", e); //добавить в документ
            Console.WriteLine("Parent : " +
                (e.Parent == null ? "null" : e.Parent.Name));
        }

        [Category("DML")]
        [Title("Добавить родительский элемент в документ")]
        [Description("Добавление элемента с родителями в другой контейнер приводит к его клонированию")]
        public void XLinq76() {
            XElement e = new XElement("foo",
                                      "this is an element");
            XElement p1 = new XElement("p1", e);
            Console.WriteLine("Parent : " + e.Parent.Name);

            XElement p2 = new XElement("p2", e);
            Console.WriteLine("Parent : " + e.Parent.Name);
        }

        [Category("Преобразование")]
        [Title("Создать таблицу заказчиков")]
        [Description("Создать html со списком пронумерованных заказчиков")]
        public void XLinq77() {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");

            var header = new[]{                 
                    new XElement("th","#"),
                    new XElement("th",
                                 "customer id"),
                    new XElement("th",
                                 "contact name")};
            int index = 0;
            var rows =
                from
                    customer in doc.Descendants("Customers")
                select
                    new XElement("tr",
                                 new XElement("td",
                                              ++index),
                                 new XElement("td",
                                              (string)customer.Attribute("CustomerID")),
                                 new XElement("td",
                                              (string)customer.Element("ContactName")));

            XElement html = new XElement("html",
                                  new XElement("body",
                                    new XElement("table",
                                      header,
                                      rows)));
            Console.Write(html);
        }

        [Category("Преобразование")]
        [Title("Создать html-таблицы книг")]
        [Description("Создать html-таблицы книг по авторам")]
        [LinkedMethod("GetBooksTable")]
        public void XLinq78() {
            XDocument doc = XDocument.Load(dataPath + "bib.xml");
            var content =
                from b in doc.Descendants("book")
                from a in b.Elements("author")
                group b by (string)a.Element("first") + " " + (string)a.Element("last") into authorGroup
                select new XElement("p", "Author: " + authorGroup.Key,
                                           GetBooksTable(authorGroup));

            XElement result =
                new XElement("html",
                            new XElement("body",
                             content));
            Console.WriteLine(result);
        }

        static XElement GetBooksTable(IEnumerable<XElement> books) {
            var header = new XElement[]{
                            new XElement("th","Title"), 
                            new XElement("th", "Year")};
            var rows =
                from
                    b in books
                select
                    new XElement("tr",
                                 new XElement("td",
                                              (string)b.Element("title")),
                                 new XElement("td",
                                              (string)b.Attribute("year")));

            return new XElement("table",
                                header,
                                rows);

        }
        [Category("Интеграция языка")]
        [Title("Найти все заказы для заказчиков в списке")]
        [Description("Найти все заказы для заказчиков в списке")]
        [LinkedMethod("SomeMethodToGetCustomers")]
        [LinkedClass("Customer")]
        public void XLinq79() {
            XDocument doc = XDocument.Load(dataPath + "nw_orders.xml");
            List<Customer> customers = SomeMethodToGetCustomers();
            var result =
                from customer in customers
                from order in doc.Descendants("Orders")
                where
                    customer.id == (string)order.Element("CustomerID")
                select
                    new { custid = (string)customer.id, orderdate = (string)order.Element("OrderDate") };
            foreach (var tuple in result)
                Console.WriteLine("Customer id = {0}, Order Date = {1}",
                                    tuple.custid, tuple.orderdate);

        }
        class Customer {
            public Customer(string id) { this.id = id; }
            public string id;
        }
        static List<Customer> SomeMethodToGetCustomers() {
            List<Customer> customers = new List<Customer>();
            customers.Add(new Customer("VINET"));
            customers.Add(new Customer("TOMSP"));
            return customers;
        }

        [Category("Интеграция языка")]
        [Title("Найти суммарную цену предметов в тележке для покупок")]
        [Description("Найти суммарную цену предметов в тележке для покупок")]
        [LinkedMethod("GetShoppingCart")]
        [LinkedClass("Item")]
        public void XLinq80() {
            XDocument doc = XDocument.Load(dataPath + "inventory.xml");
            List<Item> cart = GetShoppingCart();
            var subtotal =
                from item in cart
                from inventory in doc.Descendants("item")
                where item.id == (string)inventory.Attribute("id")
                select (double)item.quantity * (double)inventory.Element("price");
            Console.WriteLine("Total payment = {0}", subtotal.Sum());
        }
        class Item {
            public Item(string id, int quantity) {
                this.id = id;
                this.quantity = quantity;
            }
            public int quantity;
            public string id;
        }
        static List<Item> GetShoppingCart() {
            List<Item> items = new List<Item>();
            items.Add(new Item("1", 10));
            items.Add(new Item("5", 5));
            return items;
        }

        [Category("Интеграция языка")]
        [Title("Использовать файл конфигурации")]
        [Description("Загрузить и использовать файл конфигурации")]
        [LinkedMethod("Initialize")]
        public void XLinq81() {
            XElement config = XDocument.Load(dataPath + "config.xml").Element("config");
            Initialize((string)config.Element("rootFolder"),
                       (int)config.Element("iterations"),
                       (double)config.Element("maxMemory"),
                       (string)config.Element("tempFolder"));

        }
        static void Initialize(string root, int iter, double mem, string temp) {
            Console.WriteLine("Application initialized to root folder: " +
                              "{0}, iterations: {1}, max memory {2}, temp folder: {3}",
                              root, iter, Convert.ToString(mem, CultureInfo.InvariantCulture), temp);
        }

        [Category("Интеграция языка")]
        [Title("Преобразовать последовательность узлов в массив")]
        [Description("Преобразовать последовательность узлов в массив")]
        public void XLinq82() {
            XDocument doc = XDocument.Load(dataPath + "nw_Customers.xml");
            XElement[] custArray = doc.Descendants("Customers").ToArray();
            foreach (var c in custArray)
                Console.WriteLine(c);
        }

        [Category("Интеграция языка")]
        [Title("Преобразовать последовательность узлов в список")]
        [Description("Преобразовать последовательность узлов в список")]
        public void XLinq83() {
            XDocument doc = XDocument.Load(dataPath + "nw_Customers.xml");
            List<XElement> clist = doc.Descendants("Customers").ToList();
            foreach (var c in clist)
                Console.WriteLine(c);
        }

        [Category("Интеграция языка")]
        [Title("Создать словарь клиентов")]
        [Description("Создать словарь клиентов")]
        public void XLinq84() {
            XDocument doc = XDocument.Load(dataPath + "nw_Customers.xml");
            var dictionary = doc.Descendants("Customers")
                                .ToDictionary(c => (string)c.Attribute("CustomerID"));
            Console.WriteLine(dictionary["ALFKI"]);
        }


        [Category("Интеграция языка")]
        [Title("Использование анонимных типов ")]
        [Description("Пронумеровать все страны и перечислить их")]
        public void XLinq85() {
            XDocument doc = XDocument.Load(dataPath + "nw_Customers.xml");
            var countries = doc.Descendants("Country")
                                .Select(c => (string)c)
                                .Distinct()
                                .Select((c, index) => new { i = index, name = c });
            foreach (var c in countries)
                Console.WriteLine(c.i + " " + c.name);

        }

        [Category("XName")]
        [Title("Создать элементы и атрибуты в пространстве имен")]
        [Description("Создать элементы и атрибуты в пространстве имен")]
        public void XLinq86() {
            XNamespace ns = "http://myNamespace";
            XElement result = new XElement(ns + "foo",
                                           new XAttribute(ns + "bar", "attribute"));
            Console.WriteLine(result);
        }

        [Category("XName")]
        [Title("Запросить элементы в пространстве имен")]
        [Description("Найти xsd:element с name=FullAddress")]
        public void XLinq87() {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xsd");
            XNamespace XSD = "http://www.w3.org/2001/XMLSchema";
            XElement result = doc.Descendants(XSD + "element")
                                 .Where(e => (string)e.Attribute("name") == "FullAddress")
                                 .First();
            Console.WriteLine(result);
        }

        [Category("XName")]
        [Title("Создать объявление префикса пространства имен")]
        [Description("Создать объявление префикса пространства имен")]
        public void XLinq88() {
            XNamespace myNS = "http://myNamespace";
            XElement result = new XElement("myElement",
                                           new XAttribute(XNamespace.Xmlns + "myPrefix", myNS));
            Console.WriteLine(result);
        }

        [Category("XName")]
        [Title("Локальное имя и пространство имен")]
        [Description("Получить локальное имя и пространство имен элемента")]
        public void XLinq89() {
            XNamespace ns = "http://myNamespace";
            XElement e = new XElement(ns + "foo");

            Console.WriteLine("Local name of element: {0}", e.Name.LocalName);
            Console.WriteLine("Namespace of element : {0}", e.Name.Namespace.NamespaceName);

        }

        [Category("Прочее")]
        [Title("Получить внешний XML узла")]
        [Description("Получить внешний XML узла")]
        public void XLinq90() {
            string xml = "<order >" +
                            "<item price='150'>Motor</item>" +
                            "<item price='50'>Cable</item>" +
                            "<item price='50'>Modem</item>" +
                            "<item price='250'>Monitor</item>" +
                            "<item price='10'>Mouse</item>" +
                         "</order>";
            XElement order = XElement.Parse(xml);
            Console.WriteLine(order.ToString(SaveOptions.DisableFormatting));
        }

        [Category("Прочее")]
        [Title("Получить внутренний текст узла")]
        [Description("Получить внутренний текст узла")]
        public void XLinq91() {
            string xml = "<order >" +
                            "<item price='150'>Motor</item>" +
                            "<item price='50'>Cable</item>" +
                            "<item price='50'>Modem</item>" +
                            "<item price='250'>Monitor</item>" +
                            "<item price='10'>Mouse</item>" +
                         "</order>";
            XElement order = XElement.Parse(xml);
            Console.WriteLine(order.Value);

        }

        [Category("Прочее")]
        [Title("Проверить, имеются ли у элемента атрибуты")]
        [Description("Проверить, имеются ли у элемента атрибуты")]
        public void XLinq92() {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");
            XElement e = doc.Element("Root")
                            .Element("Customers");
            Console.WriteLine("Customers has attributes? {0}", e.HasAttributes);

        }

        [Category("Прочее")]
        [Title("Проверить, имеются ли у элемента дочерние элементы")]
        [Description("Проверить, имеются ли у элемента дочерние элементы")]
        public void XLinq93() {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");
            XElement e = doc.Element("Root")
                            .Element("Customers");
            Console.WriteLine("Customers has elements? {0}", e.HasElements);

        }

        [Category("Прочее")]
        [Title("Проверить, является ли элемент пустым")]
        [Description("Проверить, является ли элемент пустым")]
        public void XLinq94() {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");
            XElement e = doc.Element("Root")
                            .Element("Customers");
            Console.WriteLine("Customers element is empty? {0}", e.IsEmpty);

        }

        [Category("Прочее")]
        [Title("Получить имя элемента")]
        [Description("Получить имя элемента")]
        public void XLinq95() {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");
            XElement e = doc.Elements()
                            .First();
            Console.WriteLine("Name of element {0}", e.Name);
        }

        [Category("Прочее")]
        [Title("Получить имя атрибута")]
        [Description("Получить имя атрибута")]
        public void XLinq96() {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");
            XAttribute a = doc.Element("Root")
                              .Element("Customers")
                              .Attributes().First();
            Console.WriteLine("Name of attribute {0}", a.Name);
        }

        [Category("Прочее")]
        [Title("Получить объявление XML")]
        [Description("Получить объявление XML")]
        public void XLinq97() {
            XDocument doc = XDocument.Load(dataPath + "config.xml");
            Console.WriteLine("Version {0}", doc.Declaration.Version);
            Console.WriteLine("Encoding {0}", doc.Declaration.Encoding);
            Console.WriteLine("Standalone {0}", doc.Declaration.Standalone);
        }


        [Category("Прочее")]
        [Title("Найти тип узла")]
        [Description("Найти тип узла")]
        public void XLinq98() {
            XNode o = new XElement("foo");
            Console.WriteLine(o.NodeType);
        }

        [Category("Прочее")]
        [Title("Проверьте номера телефонов")]
        [Description("Убедитесь, что номера телефонов имеют формат xxx-xxx-xxxx")]
        [LinkedMethod("CheckPhone")]
        public void XLinq99() {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");
            var query =
                from customer in doc.Descendants("Customers")
                select
                    new XElement("customer",
                                 customer.Attribute("CustomerID"),
                                 customer.Descendants("Phone").First(),
                                 CheckPhone((string)customer.Descendants("Phone").First()));
            foreach (var result in query)
                Console.WriteLine(result);

        }
        static XElement CheckPhone(string phone) {
            Regex regex = new Regex("([0-9]{3}-)|('('[0-9]{3}')')[0-9]{3}-[0-9]{4}");
            return new XElement("isValidPhone", regex.IsMatch(phone));
        }

        [Category("Прочее")]
        [Title("Быстрая проверка")]
        [Description("Проверка структуры файла")]
        [LinkedMethod("VerifyCustomer")]
        public void XLinq100() {
            XDocument doc = XDocument.Load(dataPath + "nw_customers.xml");
            foreach (XElement customer in doc.Descendants("Customers")) {
                string err = VerifyCustomer(customer);
                if (err != "")
                    Console.WriteLine("Cusotmer {0} is invalid. Missing {1}",
                                        (string)customer.Attribute("CustomerID"), err);
            }

        }
        static string VerifyCustomer(XElement c) {
            if (c.Element("CompanyName") == null)
                return "CompanyName";
            if (c.Element("ContactName") == null)
                return "ContactName";
            if (c.Element("ContactTitle") == null)
                return "ContactTitle";
            if (c.Element("Phone") == null)
                return "Phone";
            if (c.Element("Fax") == null)
                return "Fax";
            if (c.Element("FullAddress") == null)
                return "FullAddress";
            return "";
        }


        [Category("Прочее")]
        [Title("Агрегатные функции")]
        [Description("Рассчитать сумму, среднее, минимум и максимум стоимости доставки для всех заказов")]
        public void XLinq101() {
            XDocument doc = XDocument.Load(dataPath + "nw_orders.xml");
            var query =
                from
                    order in doc.Descendants("Orders")
                where
                    (string)order.Element("CustomerID") == "VINET" &&
                    order.Elements("ShipInfo").Elements("Freight").Any()
                select
                    (double)order.Element("ShipInfo").Element("Freight");

            double sum = query.Sum();
            double average = query.Average();
            double min = query.Min();
            double max = query.Max();

            Console.WriteLine("Sum: {0}, Average: {1}, Min: {2}, Max: {3}", Convert.ToString(sum, CultureInfo.InvariantCulture),
                Convert.ToString(average, CultureInfo.InvariantCulture), Convert.ToString(min, CultureInfo.InvariantCulture),
                Convert.ToString(max, CultureInfo.InvariantCulture));
        }

        [Category("События")]
        [Title("Добавить или удалить события")]
        [Description("Подключение слушателей событий к корневому элементу и добавление (удаление) дочернего элемента")]
        [LinkedMethod("OnChanging")]
        [LinkedMethod("OnChanged")]
        public void XLinq102() {
            string xml = "<order >" +
                            "<item price='150'>Motor</item>" +
                            "<item price='50'>Cable</item>" +
                            "<item price='50'>Modem</item>" +
                            "<item price='250'>Monitor</item>" +
                            "<item price='10'>Mouse</item>" +
                         "</order>";
            XDocument order = XDocument.Parse(xml);
            // Подключение слушателей событий к заказу
            order.Changing += new EventHandler<XObjectChangeEventArgs>(OnChanging);
            order.Changed += new EventHandler<XObjectChangeEventArgs>(OnChanged);
            // Добавление в заказ нового элемента
            Console.WriteLine("Original Order:");
            Console.WriteLine("{0}", order.ToString(SaveOptions.None));
            order.Root.Add(new XElement("item", new XAttribute("price", 350), "Printer"));
            Console.WriteLine("After Add:");
            Console.WriteLine("{0}", order.ToString(SaveOptions.None));
            order.Root.Element("item").Remove();
            Console.WriteLine("After Remove:");
            Console.WriteLine("{0}", order.ToString(SaveOptions.None));
            // Удаление слушателей событий
            order.Changing -= new EventHandler<XObjectChangeEventArgs>(OnChanging);
            order.Changed -= new EventHandler<XObjectChangeEventArgs>(OnChanged);
            // Добавление другого элемента без генерации событий
            order.Root.Add(new XElement("item", new XAttribute("price", 350), "Printer"));
        }

        [Category("События")]
        [Title("События изменения имени или значения")]
        [Description("Подключение слушателей событий к корневому элементу и изменение имени или значения элемента")]
        [LinkedMethod("OnChanging")]
        [LinkedMethod("OnChanged")]
        public void XLinq103() {
            string xml = "<order >" +
                            "<item price='150'>Motor</item>" +
                            "<item price='50'>Cable</item>" +
                            "<item price='50'>Modem</item>" +
                            "<item price='250'>Monitor</item>" +
                            "<item price='10'>Mouse</item>" +
                         "</order>";
            XElement order = XElement.Parse(xml);
            order.Changing += new EventHandler<XObjectChangeEventArgs>(OnChanging);
            order.Changed += new EventHandler<XObjectChangeEventArgs>(OnChanged);
            Console.WriteLine("Original Order:");
            Console.WriteLine("{0}", order.ToString(SaveOptions.None));
            order.Name = "newOrder";
            Console.WriteLine("After Name Change:");
            Console.WriteLine("{0}", order.ToString(SaveOptions.None));
            order.Element("item").Value = "New Item";
            Console.WriteLine("After Value Change:");
            Console.WriteLine("{0}", order.ToString(SaveOptions.None));
        }

        [Category("События")]
        [Title("Обработчики множественных событий")]
        [Description("Подключение слушателей событий к множеству элементов дерева")]
        [LinkedMethod("OnChanging")]
        [LinkedMethod("OnChanged")]
        public void XLinq104() {
            string xml = "<order >" +
                            "<item price='150'>Motor</item>" +
                            "<item price='50'>Cable</item>" +
                            "<item price='50'>Modem</item>" +
                            "<item price='250'>Monitor</item>" +
                            "<item price='10'>Mouse</item>" +
                         "</order>";
            XDocument order = XDocument.Parse(xml);
            order.Changing += new EventHandler<XObjectChangeEventArgs>(OnChanging);
            order.Changed += new EventHandler<XObjectChangeEventArgs>(OnChanged);
            order.Root.Changing += new EventHandler<XObjectChangeEventArgs>(OnChanging);
            order.Root.Changed += new EventHandler<XObjectChangeEventArgs>(OnChanged);
            // Этот элемент не должен получить каких-либо событий, поскольку изменениям подвергнется
            // корневой элемент
            order.Root.Element("item").Changing += new EventHandler<XObjectChangeEventArgs>(OnChanging);
            order.Root.Element("item").Changed += new EventHandler<XObjectChangeEventArgs>(OnChanged);
            Console.WriteLine("Original Order:");
            Console.WriteLine("{0}", order.ToString(SaveOptions.None));
            // При этом не должны создаваться какие-либо события для 'order.Root.Element(\"item\")'
            order.Root.Add(new XElement("item", "Printer"));
            Console.WriteLine("After Add:");
            Console.WriteLine("{0}", order.ToString(SaveOptions.None));
        }

        public void OnChanging(object sender, XObjectChangeEventArgs e) {
            Console.WriteLine();
            Console.WriteLine("OnChanging:");
            Console.WriteLine("EventType: {0}", e.ObjectChange);
            Console.WriteLine("Object: {0}", ((XNode)sender).ToString(SaveOptions.None));
        }

        public void OnChanged(object sender, XObjectChangeEventArgs e) {
            Console.WriteLine();
            Console.WriteLine("OnChanged:");
            Console.WriteLine("EventType: {0}", e.ObjectChange);
            Console.WriteLine("Object: {0}", ((XNode)sender).ToString(SaveOptions.None));
            Console.WriteLine();
        }

        [Category("События")]
        [Title("Журнал заказов")]
        [Description("Регистрация заказов в журнале по мере их добавления")]
        public void XLinq105() {
            string xml = "<order >" +
                            "<item price='150'>Motor</item>" +
                            "<item price='50'>Cable</item>" +
                            "<item price='50'>Modem</item>" +
                            "<item price='250'>Monitor</item>" +
                            "<item price='10'>Mouse</item>" +
                         "</order>";
            XElement order = XElement.Parse(xml);
            int orderCount = 0;
            StringWriter log = new StringWriter();

            order.Changed += new EventHandler<XObjectChangeEventArgs>(
                    delegate(object sender, XObjectChangeEventArgs e) {
                        orderCount++;
                        log.WriteLine(((XNode)sender).ToString(SaveOptions.None));
                    });

            for (int i = 0; i < 100; i++) {
                order.Add(new XElement("item", new XAttribute("price", 350), "Printer"));
            }

            Console.WriteLine("Orders Received: {0}", orderCount);
            Console.WriteLine("Orders Log:");
            Console.WriteLine(log.ToString());
        }
    }
}