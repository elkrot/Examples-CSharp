// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using nwind;
using SampleSupport;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Transactions;
using System.Linq.Expressions;
using System.Data.Linq.Provider;
using System.Data.Linq.Mapping;
using System.Reflection;
using System.Data.Linq.SqlClient;
using System.Xml.Linq;

namespace SampleQueries {
    [Title("Примеры запросов 101+ Linq To Sql")]
    [Prefix("LinqToSql")]
    public class LinqToSqlSamples : SampleHarness {
        
        private readonly static string dbPath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\Data\NORTHWND.MDF"));
        private readonly static string sqlServerInstance = @".\SQLEXPRESS";
        private readonly static string connString = "AttachDBFileName='" + dbPath + "';Server='" + sqlServerInstance + "';user instance=true;Integrated Security=SSPI;Connection Timeout=60";

        private Northwind db;

        [Category("Where")]
        [Title("Where - 1")]
        [Description("В этом примере для отбора клиентов из Лондона используется предложение WHERE.")]
        public void LinqToSqlWhere01() {
            var q =
                from c in db.Customers
                where c.City == "London"
                select c;
            ObjectDumper.Write(q);
        }

        [Category("Where")]
        [Title("Where - 2")]
        [Description("В этом примере предложение WHERE используется для отбора сотрудников, нанятых " +
                     "в течение и после 1994 г.")]
        public void LinqToSqlWhere02() {
            var q =
                from e in db.Employees
                where e.HireDate >= new DateTime(1994, 1, 1)
                select e;

            ObjectDumper.Write(q);
        }

        [Category("Where")]
        [Title("Where - 3")]
        [Description("Образец использования предложения WHERE для отбора продуктов, чей запас ниже " +
                     "минимального запаса, а поставка не прерывается.")]
        public void LinqToSqlWhere03() {
            var q =
                from p in db.Products
                where p.UnitsInStock <= p.ReorderLevel && !p.Discontinued
                select p;

            ObjectDumper.Write(q);
        }

        [Category("Where")]
        [Title("Where - 4")]
        [Description("Пример использования WHERE для отбора продуктов, которые либо " +
                     "имеют UnitPrice больше 10, либо более не поставляются.")]
        public void LinqToSqlWhere04() {
            var q =
                from p in db.Products
                where p.UnitPrice > 10m || p.Discontinued
                select p;

            ObjectDumper.Write(q, 0);
        }

        [Category("Where")]
        [Title("Where - 5")]
        [Description("В этом примере WHERE вызывается дважды, чтобы отобрать продукты, чей UnitPrice больше 10" +
                     " и поставка прекращена.")]
        public void LinqToSqlWhere05() {
            var q =
                db.Products.Where(p=>p.UnitPrice > 10m).Where(p=>p.Discontinued);

            ObjectDumper.Write(q, 0);
        }

        [Category("Where")]
        [Title("First - простой")]
        [Description("Образец использования First для выбора первого поставщика в таблице.")]
        public void LinqToSqlWhere06() {
            Shipper shipper = db.Shippers.First();
            ObjectDumper.Write(shipper, 0);
        }

        [Category("Where")]
        [Title("First - элемент")]
        [Description("Пример использования First для выбора одного объекта Customer с CustomerID 'BONAP'.")]
        public void LinqToSqlWhere07() {
            Customer cust = db.Customers.First(c => c.CustomerID == "BONAP");
            ObjectDumper.Write(cust, 0);
        }

        [Category("Where")]
        [Title("First - условный")]
        [Description("Образец использования First для выбора первого заказа с фрахтом, превышающим 10,00.")]
        public void LinqToSqlWhere08() {
            Order ord = db.Orders.First(o => o.Freight > 10.00M);
            ObjectDumper.Write(ord, 0);
        }

        [Category("Select/Distinct")]
        [Title("Select - простой")]
        [Description("Образец использования SELECT для возврата последовательности " +
                     "контактных имен заказчиков.")]
        public void LinqToSqlSelect01() {
            var q =
                from c in db.Customers
                select c.ContactName;

            ObjectDumper.Write(q);
        }

        [Category("Select/Distinct")]
        [Title("Select - анонимный тип 1")]
        [Description("Образец использования SELECT и анонимных типов для возврата " +
                     "последовательности контактных имен заказчиков и номеров телефонов.")]
        public void LinqToSqlSelect02() {
            var q =
                from c in db.Customers
                select new {c.ContactName, c.Phone};

            ObjectDumper.Write(q);
        }

        [Category("Select/Distinct")]
        [Title("Select - анонимный тип 2")]
        [Description("Образец использования SELECT и анонимных типов для возврата " +
                     "последовательности имен сотрудников и номеров телефонов " +
                     "с полями \"Имя\" и \"Фамилия\", объединенными в единое поле \"Имя\", " +
                     "и полем \"Домашний телефон\", переименованным в \"Телефон\" в конечной последовательности.")]
        public void LinqToSqlSelect03() {
            var q =
                from e in db.Employees
                select new {Name = e.FirstName + " " + e.LastName, Phone = e.HomePhone};

            ObjectDumper.Write(q, 1);
        }

        [Category("Select/Distinct")]
        [Title("Select - анонимный тип 3")]
        [Description("Образец использования SELECT и анонимных типов для возврата " +
                     "последовательности всех идентификаторов продуктов и рассчитанной стоимости, " +
                     "названной HalfPrice, которая определяется как значение UnitPrice продукта, " +
                     "деленное на 2.")]
        public void LinqToSqlSelect04() {
            var q =
                from p in db.Products
                select new {p.ProductID, HalfPrice = p.UnitPrice / 2};
            ObjectDumper.Write(q, 1);
        }

        [Category("Select/Distinct")]
        [Title("Select - условный ")]
        [Description("Образец использования SELECT и условного выражения для возврата последовательности названий " +
                     " товаров и доступности товаров.")]
        public void LinqToSqlSelect05() {
            var q =
                from p in db.Products
                select new {p.ProductName, Availability = p.UnitsInStock - p.UnitsOnOrder < 0 ? "Out Of Stock": "In Stock"};

            ObjectDumper.Write(q, 1);
        }

        [Category("Select/Distinct")]
        [Title("Select - именованный тип")]
        [Description("Образец использования SELECT и известного типа для возврата последовательности имен сотрудников.")]
        public void LinqToSqlSelect06() {
            var q =
                from e in db.Employees                
                select new Name {FirstName = e.FirstName, LastName = e.LastName};

            ObjectDumper.Write(q, 1);
        }

        [Category("Select/Distinct")]
        [Title("Select - с фильтром")]
        [Description("Образец использования SELECT и WHERE для возврата последовательности " +
                     "имен контактных лиц лондонских клиентов.")]
        public void LinqToSqlSelect07() {
            var q =
                from c in db.Customers
                where c.City == "London"
                select c.ContactName;

            ObjectDumper.Write(q);
        }

        [Category("Select/Distinct")]
        [Title("Select - сформированные")]
        [Description("Образец использования SELECT и анонимных типов для возврата " +
                     "сформированного подмножества данных о заказчиках.")]
        public void LinqToSqlSelect08() {
            var q =
                from c in db.Customers
                select new {
                    c.CustomerID,
                    CompanyInfo = new {c.CompanyName, c.City, c.Country},
                    ContactInfo = new {c.ContactName, c.ContactTitle}
                };

            ObjectDumper.Write(q, 1);
        }

        [Category("Select/Distinct")]
        [Title("Select - вложенные ")]
        [Description("Образец использования вложенных запросов для возврата последовательности " +
                     "всех заказов, содержащей идентификатор заказа, последовательность " +
                     "позиций в заказе, имеющих скидку, и сэкономленных денег, " +
                     "если не включена доставка.")]
        public void LinqToSqlSelect09() {
            var q =
                from o in db.Orders
                select new {
                    o.OrderID,
                    DiscountedProducts =
                        from od in o.OrderDetails
                        where od.Discount > 0.0
                        select od,
                    FreeShippingDiscount = o.Freight
                };

            ObjectDumper.Write(q, 1);
        }

        // Преобразователь телефонов, преобразующий номер телефона в 
        // международный формат с учетом страны.
        // В этом примере поддерживаются только форматы США и Великобритании, для 
        // номеров телефонов из базы данных Northwind.
        public string PhoneNumberConverter(string Country, string Phone)
        {
            Phone = Phone.Replace(" ", "").Replace(")", ")-");
            switch (Country)
            {
                case "USA":
                    return "1-" + Phone;
                case "UK":
                    return "44-" + Phone;
                default:
                    return Phone;
            }
        }

        [Category("Select/Distinct")]
        [Title("Select - вызов локального метода 1")]
        [Description("Образец использования вызова локального метода " + 
                     "\"PhoneNumberConverter\" для преобразования телефонного номера " + 
                     "к международному формату.")]
        public void LinqToSqlLocalMethodCall01()
        {
            var q = from c in db.Customers
                    where c.Country == "UK" || c.Country == "USA"
                    select new { c.CustomerID, c.CompanyName, Phone = c.Phone, InternationalPhone = PhoneNumberConverter(c.Country, c.Phone) };

            ObjectDumper.Write(q);
        }

        [Category("Select/Distinct")]
        [Title("Select - вызов локального метода 2")]
        [Description("Образец использования вызова локального метода " + 
                     "для преобразования телефонных номеров к международному формату " + 
                     "и создания XDocument.")]
        public void LinqToSqlLocalMethodCall02()
        {
            XDocument doc = new XDocument(
                new XElement("Заказчики", from c in db.Customers
                                          where c.Country == "UK" || c.Country == "USA"
                                          select (new XElement("Customer",
                                              new XAttribute("CustomerID", c.CustomerID),
                                              new XAttribute("CompanyName", c.CompanyName),
                                              new XAttribute("InterationalPhone", PhoneNumberConverter(c.Country, c.Phone))
                                              ))));

            Console.WriteLine(doc.ToString());
        }


        [Category("Select/Distinct")]
        [Title("Distinct")]
        [Description("Образец использования Distinct для получения последовательности уникальных городов, " +
                     "в которых есть клиенты.")]
        public void LinqToSqlSelect10() {
            var q = (
                from c in db.Customers
                select c.City )
                .Distinct();

            ObjectDumper.Write(q);
        }

        [Category("Count/Sum/Min/Max/Avg")]
        [Title("Count - простой")]
        [Description("Образец использования Count для получения количества клиентов в базе данных.")]
        public void LinqToSqlCount01() {
            var q = db.Customers.Count();
            Console.WriteLine(q);
        }

        [Category("Count/Sum/Min/Max/Avg")]
        [Title("Count - условный")]
        [Description("Образец использования Count для получения количества продуктов в базе данных, " +
                     "поставка которых не прекращена.")]
        public void LinqToSqlCount02() {
            var q = db.Products.Count(p => !p.Discontinued);
            Console.WriteLine(q);
        }

        [Category("Count/Sum/Min/Max/Avg")]
        [Title("Sum - простой")]
        [Description("Образец использования Sum для получения общего фрахта по всем заказам.")]
        public void LinqToSqlCount03() {
            var q = db.Orders.Select(o => o.Freight).Sum();
            Console.WriteLine(q);
        }

        [Category("Count/Sum/Min/Max/Avg")]
        [Title("Сумма - сопоставленная")]
        [Description("Образец использования Sum для нахождения общего числа заказанных элементов среди всех продуктов.")]
        public void LinqToSqlCount04() {
            var q = db.Products.Sum(p => p.UnitsOnOrder);
            Console.WriteLine(q);
        }

        [Category("Count/Sum/Min/Max/Avg")]
        [Title("Min - простой")]
        [Description("Образец использования Min для получения наименьшей цены за единицу товара.")]
        public void LinqToSqlCount05() {
            var q = db.Products.Select(p => p.UnitPrice).Min();
            Console.WriteLine(q);
        }

        [Category("Count/Sum/Min/Max/Avg")]
        [Title("Min - с сопоставлением")]
        [Description("Образец использования Min для поиска наименьшей стоимости доставки любого заказа.")]
        public void LinqToSqlCount06() {
            var q = db.Orders.Min(o => o.Freight);
            Console.WriteLine(q);
        }

        [Category("Count/Sum/Min/Max/Avg")]
        [Title("Min - элементы")]
        [Description("Образец использования Min для получения товаров с наименьшей ценой " +
                     "в каждой категории.")]
        public void LinqToSqlCount07() {
            var categories =
                from p in db.Products
                group p by p.CategoryID into g
                select new {
                    CategoryID = g.Key,
                    CheapestProducts =
                        from p2 in g
                        where p2.UnitPrice == g.Min(p3 => p3.UnitPrice)
                        select p2
                };

            ObjectDumper.Write(categories, 1);
        }

        [Category("Count/Sum/Min/Max/Avg")]
        [Title("Max - простой")]
        [Description("Образец использования Max для получения последней даты приема любого сотрудника.")]
        public void LinqToSqlCount08() {
            var q = db.Employees.Select(e => e.HireDate).Max();
            Console.WriteLine(q);
        }

        [Category("Count/Sum/Min/Max/Avg")]
        [Title("Max - с сопоставлением")]
        [Description("Образец использования Max для получения наибольшего количества единиц в запасе любого продукта.")]
        public void LinqToSqlCount09() {
            var q = db.Products.Max(p => p.UnitsInStock);
            Console.WriteLine(q);
        }

        [Category("Count/Sum/Min/Max/Avg")]
        [Title("Max - элементы")]
        [Description("Образец использования Max для получения продуктов с наибольшей ценой за единицу " +
                     "в каждой категории.")]
        public void LinqToSqlCount10() {
            var categories =
                from p in db.Products
                group p by p.CategoryID into g
                select new {
                    g.Key,
                    MostExpensiveProducts =
                        from p2 in g
                        where p2.UnitPrice == g.Max(p3 => p3.UnitPrice)
                        select p2
                };

            ObjectDumper.Write(categories, 1);
        }

        [Category("Count/Sum/Min/Max/Avg")]
        [Title("Average - простой")]
        [Description("Образец использования Average для получения среднего фрахта по всем заказам.")]
        public void LinqToSqlCount11() {
            var q = db.Orders.Select(o => o.Freight).Average();
            Console.WriteLine(q);
        }

        [Category("Count/Sum/Min/Max/Avg")]
        [Title("Average - с сопоставлением")]
        [Description("Образец использования Average для получения средней цены за единицу по всем продуктам.")]
        public void LinqToSqlCount12() {
            var q = db.Products.Average(p => p.UnitPrice);
            Console.WriteLine(q);
        }

        [Category("Count/Sum/Min/Max/Avg")]
        [Title("Average - элементы")]
        [Description("Образец использования Average для получения продуктов, цена за единицу которых превышает " +
                     "среднюю цену за единицу в категории для каждой категории.")]
        public void LinqToSqlCount13() {
            var categories =
                from p in db.Products
                group p by p.CategoryID into g
                select new {
                    g.Key, 
                    ExpensiveProducts =
                        from p2 in g
                        where p2.UnitPrice > g.Average(p3 => p3.UnitPrice)
                        select p2
                };

            ObjectDumper.Write(categories, 1);
        }

        [Category("Join")]
        [Title("SelectMany - 1 ко многим - 1")]
        [Description("В этом примере используется навигация внешних ключей во втором " +
                     "предложении clause для создания равномерной последовательности всех заказов для клиентов в Лондоне")]
        public void LinqToSqlJoin01() {
            var q =
                from c in db.Customers
                where c.City == "London"
                from o in c.Orders
                select o;

            ObjectDumper.Write(q);
        }

        [Category("Join")]
        [Title("SelectMany - 1 ко многим - 2")]
        [Description("Образец использования навигации внешних ключей в " +
                     "предложении Where для отбора товаров, поставщик которых находится в США, и " +
                     "которых нет на складе.")]
        public void LinqToSqlJoin02() {
            var q =
                from p in db.Products
                where p.Supplier.Country == "USA" && p.UnitsInStock == 0
                select p;

            ObjectDumper.Write(q);
        }

        [Category("Join")]
        [Title("SelectMany - многие ко многим")]
        [Description("Образец использования навигации внешних ключей в " +
                     "предложении From для отбора сотрудников из Сиэтла, " +
                     "а также перечисления их территорий.")]
        public void LinqToSqlJoin03() {
            var q =
                from e in db.Employees
                from et in e.EmployeeTerritories
                where e.City == "Seattle"
                select new {e.FirstName, e.LastName, et.Territory.TerritoryDescription};

            ObjectDumper.Write(q);
        }

        [Category("Join")]
        [Title("SelectMany - самообъединение")]
        [Description("Образец использования навигации внешних ключей в " +
                     "предложении Select для отбора пар сотрудников, в которых " +
                     "один сотрудник подотчетен другому, и " +
                     "оба сотрудника из одного города.")]
        public void LinqToSqlJoin04() {
            var q =
                from e1 in db.Employees
                from e2 in e1.Employees
                where e1.City == e2.City
                select new {
                    FirstName1 = e1.FirstName, LastName1 = e1.LastName,
                    FirstName2 = e2.FirstName, LastName2 = e2.LastName,
                    e1.City
                };

            ObjectDumper.Write(q);
        }

        [Category("Join")]
        [Title("GroupJoin - двустороннее соединение")]
        [Description("Образец явного соединения двух таблиц и отображения результатов из обеих таблиц.")]
        public void LinqToSqlJoin05() {
            var q =
                from c in db.Customers
                join o in db.Orders on c.CustomerID equals o.CustomerID into orders
                select new {c.ContactName, OrderCount = orders.Count()};

            ObjectDumper.Write(q);
        }

        [Category("Join")]
        [Title("GroupJoin - трехстороннее соединение")]
        [Description("Образец явного соединения трех таблиц и отображения результатов из каждой.")]
        public void LinqToSqlJoin06() {
            var q =
                from c in db.Customers
                join o in db.Orders on c.CustomerID equals o.CustomerID into ords
                join e in db.Employees on c.City equals e.City into emps
                select new {c.ContactName, ords=ords.Count(), emps=emps.Count()};

            ObjectDumper.Write(q);
        }

        [Category("Join")]
        [Title("GroupJoin - Left Outer Join")] 
        [Description("Пример, демонстрирующий, как получить LEFT OUTER JOIN при помощи DefaultIfEmpty(). Метод DefaultIfEmpty() возвращает null, если для сотрудника нет заказов." )]
        public void LinqToSqlJoin07() {
            var q =
                from e in db.Employees
                join o in db.Orders on e equals o.Employee into ords
                from o in ords.DefaultIfEmpty()
                select new {e.FirstName, e.LastName, Order = o};

            ObjectDumper.Write(q);
        }

        [Category("Join")]
        [Title("GroupJoin - проекция назначения let")]
        [Description("Образец проекции выражения Let в результате объединения.")]
        public void LinqToSqlJoin08() {
            var q = 
                from c in db.Customers
                join o in db.Orders on c.CustomerID equals o.CustomerID into ords
                let z = c.City + c.Country
                from o in ords                  
                select new {c.ContactName, o.OrderID, z};

            ObjectDumper.Write(q);
        }

        [Category("Join")]
        [Title("GroupJoin - составной ключ")]
        [Description("Образец объединения с составным ключом.")]
        public void LinqToSqlJoin09() {
            var q =
                from o in db.Orders
                from p in db.Products
                join d in db.OrderDetails 
                    on new {o.OrderID, p.ProductID} equals new {d.OrderID, d.ProductID}
                    into details
                from d in details
                select new {o.OrderID, p.ProductID, d.UnitPrice};

            ObjectDumper.Write(q);
        }

        [Category("Join")]
        [Title("GroupJoin - связи по ключу, допускающие и не допускающие Null")]
        [Description("Образец построения соединения, в котором одна сторона может иметь значение NULL, а другая нет.")]
        public void LinqToSqlJoin10() {
            var q =
                from o in db.Orders
                join e in db.Employees 
                    on o.EmployeeID equals (int?)e.EmployeeID into emps
                from e in emps
                select new {o.OrderID, e.FirstName};

            ObjectDumper.Write(q);
        }

        [Category("Order By")]
        [Title("OrderBy - простой")]
        [Description("Образец использования Order By для сортировки сотрудников " +
                     "по дате найма.")]
        public void LinqToSqlOrderBy01() {
            var q =
                from e in db.Employees
                orderby e.HireDate
                select e;

            ObjectDumper.Write(q);
        }

        [Category("Order By")]
        [Title("OrderBy - с Where")]
        [Description("Образец использования Where и OrderBy для сортировки заказов, " +
                     "отправленных в Лондон фрахтом.")]
        public void LinqToSqlOrderBy02() {
            var q =
                from o in db.Orders
                where o.ShipCity == "London"
                orderby o.Freight
                select o;

            ObjectDumper.Write(q);
        }

        [Category("Order By")]
        [Title("OrderByDescending")]
        [Description("Образец использования OrderBy для сортировки сотрудников " +
                     "по цене за единицу от самой высокой до самой низкой.")]
        public void LinqToSqlOrderBy03() {
            var q =
                from p in db.Products
                orderby p.UnitPrice descending
                select p;

            ObjectDumper.Write(q);
        }

        [Category("Order By")]
        [Title("ThenBy")]
        [Description("Образец использования составного предложения OrderBy для сортировки сотрудников " +
                     "по городу и затем по имени контактного лица.")]
        public void LinqToSqlOrderBy04() {
            var q =
                from c in db.Customers
                orderby c.City, c.ContactName
                select c;

            ObjectDumper.Write(q);
        }

        [Category("Order By")]
        [Title("ThenByDescending")]
        [Description("Образец использования OrderBy для сортировки заказов из EmployeeID 1 " +
                     "по стране назначения, а затем по стоимости доставки от наибольшей к наименьшей.")]
        public void LinqToSqlOrderBy05() {
            var q =
                from o in db.Orders
                where o.EmployeeID == 1
                orderby o.ShipCountry, o.Freight descending
                select o;

            ObjectDumper.Write(q);
        }


        [Category("Order By")]
        [Title("OrderBy - Group By")]
        [Description("Образец использования OrderBy, Max и Group By для поиска продуктов, имеющих " +
                     "самую высокую цену в каждой категории, и сортировки группы по идентификатору категории.")]
        public void LinqToSqlOrderBy06() {
            var categories =
                from p in db.Products
                group p by p.CategoryID into g
                orderby g.Key
                select new {
                    g.Key,
                    MostExpensiveProducts =
                        from p2 in g
                        where p2.UnitPrice == g.Max(p3 => p3.UnitPrice)
                        select p2
                };

            ObjectDumper.Write(categories, 1);
        }

        [Category("Group By/Having")]
        [Title("GroupBy - простой")]
        [Description("Образец использования Group By для разделения продуктов по " +
                     "идентификатору категории.")]
        public void LinqToSqlGroupBy01() {
            var q =
                from p in db.Products
                group p by p.CategoryID into g
                select g;

            ObjectDumper.Write(q, 1);
        }

        [Category("Group By/Having")]
        [Title("GroupBy - Max")]
        [Description("Образец использования Group By и Max " +
                     "для поиска максимальной цены за единицу для каждого идентификатора категории.")]
        public void LinqToSqlGroupBy02() {
            var q =
                from p in db.Products
                group p by p.CategoryID into g
                select new {
                    g.Key,
                    MaxPrice = g.Max(p => p.UnitPrice)
                };

            ObjectDumper.Write(q, 1);
        }

        [Category("Group By/Having")]
        [Title("GroupBy - Min")]
        [Description("Образец использования Group By и Min " +
                     "для поиска минимальной цены за единицу для каждого идентификатора категории.")]
        public void LinqToSqlGroupBy03() {
            var q =
                from p in db.Products
                group p by p.CategoryID into g
                select new {
                    g.Key,
                    MinPrice = g.Min(p => p.UnitPrice)
                };

            ObjectDumper.Write(q, 1);
        }

        [Category("Group By/Having")]
        [Title("GroupBy - среднее")]
        [Description("Образец использования Group By и Average " +
                     "для поиска средней цены за единицу для каждого идентификатора категории.")]
        public void LinqToSqlGroupBy04() {
            var q =
                from p in db.Products
                group p by p.CategoryID into g
                select new {
                    g.Key,
                    AveragePrice = g.Average(p => p.UnitPrice)
                };

            ObjectDumper.Write(q, 1);
        }

        [Category("Group By/Having")]
        [Title("GroupBy - Sum")]
        [Description("Образец использования Group By и Sum " +
                     "для поиска суммарной цены за единицу для идентификатора кода категории.")]
        public void LinqToSqlGroupBy05() {
            var q =
                from p in db.Products
                group p by p.CategoryID into g
                select new {
                    g.Key,
                    TotalPrice = g.Sum(p => p.UnitPrice)
                };

            ObjectDumper.Write(q, 1);
        }

        [Category("Group By/Having")]
        [Title("GroupBy - Count")]
        [Description("Образец использования Group By и Count " +
                     "для поиска числа продуктов в каждой категории.")]
        public void LinqToSqlGroupBy06() {
            var q =
                from p in db.Products
                group p by p.CategoryID into g
                select new {
                    g.Key,
                    NumProducts = g.Count()
                };

            ObjectDumper.Write(q, 1);
        }

        [Category("Group By/Having")]
        [Title("GroupBy с условным счетчиком")]
        [Description("Образец использования Group By и Count " +
                     "для поиска числа продуктов в каждой категории, " +
                     "поставка которых прекращена.")]
        public void LinqToSqlGroupBy07() {
            var q =
                from p in db.Products
                group p by p.CategoryID into g
                select new {
                    g.Key,
                    NumProducts = g.Count(p => p.Discontinued)
                };

            ObjectDumper.Write(q, 1);
        }

        [Category("Group By/Having")]
        [Title("GroupBy - с последующим Where")]
        [Description("Образец использования предложения Where после предложения Group By " +
                     "для поиска всех категорий, в которых есть не менее 10 продуктов.")]
        public void LinqToSqlGroupBy08() {
            var q =
                from p in db.Products
                group p by p.CategoryID into g
                where g.Count() >= 10
                select new {
                    g.Key,
                    ProductCount = g.Count()
                };

            ObjectDumper.Write(q, 1);
        }

        [Category("Group By/Having")]
        [Title("GroupBy - несколько столбцов")]
        [Description("Образец использования Group By для группировки продуктов по идентификатору категории и идентификатору поставщика.")]
        public void LinqToSqlGroupBy09() {
            var categories =
                from p in db.Products
                group p by new { p.CategoryID, p.SupplierID } into g
                select new {g.Key, g};

            ObjectDumper.Write(categories, 1);
        }

        [Category("Group By/Having")]
        [Title("GroupBy - выражение")]
        [Description("Образец использования Group By для возврата двух последовательностей продуктов. " +
                     "В первой содержатся продукты с ценой за единицу, " +
                     "превышающей 10. Во второй " +
                     "с ценой за единицу меньшей или равной 10.")]
        public void LinqToSqlGroupBy10() {
            var categories =
                from p in db.Products
                group p by new { Criterion = p.UnitPrice > 10 } into g
                select g;

            ObjectDumper.Write(categories, 1);
        }

        [Category("Exists/In/Any/All/Contains")]
        [Title("Any - простой")]
        [Description("Образец использования оператора Any для возврата клиентов, не имеющих заказов.")]
        public void LinqToSqlExists01() {
            var q =
                from c in db.Customers
                where !c.Orders.Any()
                select c;

            ObjectDumper.Write(q);
        }

        [Category("Exists/In/Any/All/Contains")]
        [Title("Any - условный")]
        [Description("Образец использования оператора Any для возврата категорий, в которых имеется " +
                     "по крайней мере один товар, поставки которого прекращены.")]
        public void LinqToSqlExists02() {
            var q =
                from c in db.Categories
                where c.Products.Any(p => p.Discontinued)
                select c;

            ObjectDumper.Write(q);
        }

        [Category("Exists/In/Any/All/Contains")]
        [Title("All - условный")]
        [Description("Образец использования All для возврата клиентов, у которых все их заказы " +
                     "отправлены в их город, или тех, у кого нет заказов.")]
        public void LinqToSqlExists03() {
            var q =
                from c in db.Customers
                where c.Orders.All(o => o.ShipCity == c.City)
                select c;

            ObjectDumper.Write(q);
        }

        [Category("Exists/In/Any/All/Contains")]
        [Title("Contains - один объект")]
        [Description("Образец использования Contains для поиска клиента, имеющего заказ с ИД 10248.")] 
        public void LinqToSqlExists04()
        {
            var order = (from o in db.Orders
                         where o.OrderID == 10248
                         select o).First();

            var q = db.Customers.Where(p => p.Orders.Contains(order)).ToList();

            foreach (var cust in q)
            {
                foreach (var ord in cust.Orders)
                {
                    Console.WriteLine("У заказчика {0} есть заказ с OrderID {1}.", cust.CustomerID, ord.OrderID);
                }
            }
        }

        [Category("Exists/In/Any/All/Contains")]
        [Title("Contains - несколько значений")]
        [Description("Образец использования Contains для поиска клиентов из городов Сиэтл, Лондон, Париж или Ванкувер.")]
        public void LinqToSqlExists05()
        {
            string[] cities = new string[] { "Seattle", "London", "Vancouver", "Paris" };
            var q = db.Customers.Where(p=>cities.Contains(p.City)).ToList();

            ObjectDumper.Write(q);
        }


        [Category("Union All/Union/Intersect")]
        [Title("Concat - простой")]
        [Description("Образец использования Concat, чтобы вернуть для всех клиентов и сотрудников " +
                     "номера телефонов/факсов.")]
        public void LinqToSqlUnion01() {
            var q = (
                     from c in db.Customers
                     select c.Phone
                    ).Concat(
                     from c in db.Customers
                     select c.Fax
                    ).Concat(
                     from e in db.Employees
                     select e.HomePhone
                    );

            ObjectDumper.Write(q);
        }

        [Category("Union All/Union/Intersect")]
        [Title("Concat - составной")]
        [Description("Образец использования Concat, чтобы вернуть для всех клиентов и сотрудников " +
                     "сопоставление имен и номеров телефонов.")]
        public void LinqToSqlUnion02() {
            var q = (
                     from c in db.Customers
                     select new {Name = c.CompanyName, c.Phone}
                    ).Concat(
                     from e in db.Employees
                     select new {Name = e.FirstName + " " + e.LastName, Phone = e.HomePhone}
                    );

            ObjectDumper.Write(q);
        }

        [Category("Union All/Union/Intersect")]
        [Title("Union")]
        [Description("Образец использования Union для возврата последовательности всех стран, в которых живут " +
                     "клиенты или сотрудники.")]
        public void LinqToSqlUnion03() {
            var q = (
                     from c in db.Customers
                     select c.Country
                    ).Union(
                     from e in db.Employees
                     select e.Country
                    );

            ObjectDumper.Write(q);
        }

        [Category("Union All/Union/Intersect")]
        [Title("Intersect")]
        [Description("Образец использования  Intersect для возврата последовательности всех стран, в которых живут " +
                     "и заказчики, и сотрудники.")]
        public void LinqToSqlUnion04() {
            var q = (
                     from c in db.Customers
                     select c.Country
                    ).Intersect(
                     from e in db.Employees
                     select e.Country
                    );

            ObjectDumper.Write(q);
        }

        [Category("Union All/Union/Intersect")]
        [Title("Except")]
        [Description("Образец использования Except для возврата последовательности всех стран, в которых " +
                     "живут заказчики, но не живут сотрудники.")]
        public void LinqToSqlUnion05() {
            var q = (
                     from c in db.Customers
                     select c.Country
                    ).Except(
                     from e in db.Employees
                     select e.Country
                    );

            ObjectDumper.Write(q);
        }

        [Category("Top/Bottom")]
        [Title("Take")]
        [Description("Образец использования Take для выбора первых 5 нанятых сотрудников.")]
        public void LinqToSqlTop01() {
            var q = (
                from e in db.Employees
                orderby e.HireDate
                select e)
                .Take(5);

            ObjectDumper.Write(q);
        }

        [Category("Top/Bottom")]
        [Title("Skip")]
        [Description("Образец использования Skip для выбора всех продуктов, кроме 10 наиболее дорогих.")]
        public void LinqToSqlTop02() {
            var q = (
                from p in db.Products
                orderby p.UnitPrice descending
                select p)
                .Skip(10);

            ObjectDumper.Write(q);
        }

        [Category("Paging")]
        [Title("Постраничный просмотр - индекс")]
        [Description("Образец использования операторов Skip и Take для разбивки на страницы, " +
                     "пропустив первые 50 записей и возвратив следующие 10, таким образом " +
                     "предоставляя данные для страницы 6 таблицы продуктов.")]
        public void LinqToSqlPaging01() {
            var q = (
                from c in db.Customers
                orderby c.ContactName
                select c)
                .Skip(50)
                .Take(10);

            ObjectDumper.Write(q);
        }

        [Category("Paging")]
        [Title("Постраничный просмотр - упорядоченный уникальный ключ")]
        [Description("Образец использования предложения Where и оператора Take для разбивки на страницы, " +
                     "сначала фильтруя для получения идентификатора продукта выше 50 (последний код продукта " +
                     "со страницы 5), затем упорядочивая по идентификатору продукта, а затем отбирая первые 10 результатов, " +
                     "предоставляя таким образом данные для страницы 6 таблицы продуктов.  " +
                     "Следует учитывать, что этот метод работает только при упорядочении по уникальному ключу.")]
        public void LinqToSqlPaging02() {
            var q = (
                from p in db.Products
                where p.ProductID > 50
                orderby p.ProductID
                select p)
                .Take(10);

            ObjectDumper.Write(q);
        }
        [Category("SqlMethods")]
        [Title("SqlMethods - Like")]
        [Description("Образец использования методов SQL для отбора клиентов, идентификаторы которых начинаются с \"C\".")]
        public void LinqToSqlSqlMethods01()
        {

            var q = from c in db.Customers
                    where SqlMethods.Like(c.CustomerID, "C%")
                    select c;

            ObjectDumper.Write(q);

        }

        [Category("SqlMethods")]
        [Title("SqlMethods - DateDiffDay")]
        [Description("В этом примере методы SQL используются для поиска всех заказов, отправленных в течение 10 дней после создания")]
        public void LinqToSqlSqlMethods02()
        {

            var q = from o in db.Orders
                    where SqlMethods.DateDiffDay(o.OrderDate, o.ShippedDate) < 10
                    select o;

            ObjectDumper.Write(q);

        }

        [Category("Компилированный запрос")]
        [Title("Компилированный запрос - 1")]
        [Description("Образец создания компилированного запроса и его последующего использования для отбора клиентов во введенном городе")]
        public void LinqToSqlCompileQuery01()
        {
            //Создать скомпилированный запрос
            var fn = CompiledQuery.Compile((Northwind db2, string city) =>
                from c in db2.Customers
                where c.City == city
                select c);

            Console.WriteLine("****** Call compiled query to retrieve customers from London ******");
            var LonCusts = fn(db, "London");
            ObjectDumper.Write(LonCusts);

            Console.WriteLine();

            Console.WriteLine("****** Call compiled query to retrieve customers from Seattle ******");
            var SeaCusts = fn(db, "Seattle");
            ObjectDumper.Write(SeaCusts);

        }


        [Category("Insert/Update/Delete")]
        [Title("Insert - простой")]
        [Description("Пример использования метода InsertOnSubmit для добавления нового заказчика в " +
                     "объект таблицы Customers.  Вызов SubmitChanges приводит к сохранению этого " +
                     "нового заказчика в базе данных.")]
        public void LinqToSqlInsert01() {
            var q =
                from c in db.Customers
                where c.Region == "WA"
                select c;

            Console.WriteLine("*** BEFORE ***");
            ObjectDumper.Write(q);


            Console.WriteLine();
            Console.WriteLine("*** INSERT ***");
            var newCustomer = new Customer { CustomerID = "MCSFT",
                                             CompanyName = "Microsoft",
                                             ContactName = "John Doe",
                                             ContactTitle = "Sales Manager",
                                             Address = "1 Microsoft Way",
                                             City = "Redmond",
                                             Region = "WA",
                                             PostalCode = "98052",
                                             Country = "USA",
                                             Phone = "(425) 555-1234",
                                             Fax = null
                                           };
            db.Customers.InsertOnSubmit(newCustomer);
            db.SubmitChanges();


            Console.WriteLine();
            Console.WriteLine("*** AFTER ***");
            ObjectDumper.Write(q);



            Cleanup64();  // Восстановить предыдущее состояние базы данных
        }

        private void Cleanup64() {
            SetLogging(false);

            db.Customers.DeleteAllOnSubmit(from c in db.Customers where c.CustomerID == "MCSFT" select c);
            db.SubmitChanges();
        }

        [Category("Insert/Update/Delete")]
        [Title("Insert - 1-ко-многим")]
        [Description("Пример использования метода InsertOnSubmit для добавления новой категории в " +
                     "объект таблицы Categories и в объект таблицы Products нового продукта, " +
                     "связанного с новой категорией отношением внешнего ключа.  Вызов " +
                     "SubmitChanges сохраняет эти новые объекты и их отношения " +
                     "в базе данных.")]
        public void LinqToSqlInsert02() {

            Northwind db2 = new Northwind(connString);

            DataLoadOptions ds = new DataLoadOptions();

            ds.LoadWith<nwind.Category>(p => p.Products);
            db2.LoadOptions = ds;

            var q = (
                from c in db2.Categories
                where c.CategoryName == "Widgets"
                select c);


            Console.WriteLine("*** BEFORE ***");
            ObjectDumper.Write(q, 1);


            Console.WriteLine();
            Console.WriteLine("*** INSERT ***");
            var newCategory = new Category { CategoryName = "Widgets",
                                             Description = "Widgets are the customer-facing analogues " +
                                                           "to sprockets and cogs."
                                           };
            var newProduct = new Product { ProductName = "Blue Widget",
                                           UnitPrice = 34.56M,
                                           Category = newCategory
                                         };
            db2.Categories.InsertOnSubmit(newCategory);
            db2.SubmitChanges();


            Console.WriteLine();
            Console.WriteLine("*** AFTER ***");
            ObjectDumper.Write(q, 1);

            Cleanup65();  // Восстановить предыдущее состояние базы данных
        }

        private void Cleanup65() {
            SetLogging(false);

            db.Products.DeleteAllOnSubmit(from p in db.Products where p.Category.CategoryName == "Widgets" select p);
            db.Categories.DeleteAllOnSubmit(from c in db.Categories where c.CategoryName == "Widgets" select c);
            db.SubmitChanges();
        }

        [Category("Insert/Update/Delete")]
        [Title("Insert - многие-ко-многим")]
        [Description("Пример использования метода InsertOnSubmit для добавления нового сотрудника в " +
                     "объект таблицы Employees, новой территории в объект таблицы Territories " +
                     "и в объект таблицы EmployeeTerritories нового объекта EmployeeTerritory, " +
                     "связанного с новым сотрудником и новой территорией отношениями внешнего ключа.  " +
                     "Вызов SubmitChanges выполнит добавление этих новых объектов и их " +
                     "отношений в базу данных.")]
        public void LinqToSqlInsert03() {

            Northwind db2 = new Northwind(connString);

            DataLoadOptions ds = new DataLoadOptions();
            ds.LoadWith<nwind.Employee>(p => p.EmployeeTerritories);
            ds.LoadWith<nwind.EmployeeTerritory>(p => p.Territory);

            db2.LoadOptions = ds;
            var q = (
                from e in db.Employees
                where e.FirstName == "Nancy"
                select e);



            Console.WriteLine("*** BEFORE ***");
            ObjectDumper.Write(q, 1);


            Console.WriteLine();
            Console.WriteLine("*** INSERT ***");
            var newEmployee = new Employee { FirstName = "Kira",
                                             LastName = "Smith"
                                           };
            var newTerritory = new Territory { TerritoryID = "12345",
                                               TerritoryDescription = "Anytown",
                                               Region = db.Regions.First()
                                             };
            var newEmployeeTerritory = new EmployeeTerritory { Employee = newEmployee,
                                                               Territory = newTerritory
                                                             };
            db.Employees.InsertOnSubmit(newEmployee);
            db.Territories.InsertOnSubmit(newTerritory);
            db.EmployeeTerritories.InsertOnSubmit(newEmployeeTerritory);
            db.SubmitChanges();


            Console.WriteLine();
            Console.WriteLine("*** AFTER ***");
            ObjectDumper.Write(q, 2);



            Cleanup66();  // Восстановить предыдущее состояние базы данных
        }

        private void Cleanup66() {
            SetLogging(false);

            db.EmployeeTerritories.DeleteAllOnSubmit(from et in db.EmployeeTerritories where et.TerritoryID == "12345" select et);
            db.Employees.DeleteAllOnSubmit(from e in db.Employees where e.FirstName == "Kira" && e.LastName == "Smith" select e);
            db.Territories.DeleteAllOnSubmit(from t in db.Territories where t.TerritoryID == "12345" select t);
            db.SubmitChanges();
        }

        [Category("Insert/Update/Delete")]
        [Title("Update - простой")]
        [Description("Образец использования SubmitChanges для записи обновления полученного " +
                     "заказчика в базу данных.")]
        public void LinqToSqlInsert04() {
            var q =
                from c in db.Customers
                where c.CustomerID == "ALFKI"
                select c;

            Console.WriteLine("*** BEFORE ***");
            ObjectDumper.Write(q);


            Console.WriteLine();
            Console.WriteLine("*** UPDATE ***");
            Customer cust = db.Customers.First(c => c.CustomerID == "ALFKI");
            cust.ContactTitle = "Vice President";
            db.SubmitChanges();


            Console.WriteLine();
            Console.WriteLine("*** AFTER ***");
            ObjectDumper.Write(q);

            Cleanup67();  // Восстановить предыдущее состояние базы данных
        }

        private void Cleanup67() {
            SetLogging(false);

            Customer cust = db.Customers.First(c => c.CustomerID == "ALFKI");
            cust.ContactTitle = "Sales Representative";
            db.SubmitChanges();
        }

        [Category("Insert/Update/Delete")]
        [Title("Update - множественное обновление и отображение изменений")]
        [Description("Образец использования SubmitChanges для записи обновлений нескольких полученных " +
                     "продуктов в базу данных. Также показано, как определить количество " +
                     "измененных объектов, какие объекты были изменены, а также какие члены объектов были изменены.")]
        public void LinqToSqlInsert05() {
            var q = from p in db.Products
                    where p.CategoryID == 1
                    select p;

            Console.WriteLine("*** BEFORE ***");
            ObjectDumper.Write(q);


            Console.WriteLine();
            Console.WriteLine("*** UPDATE ***");
            foreach (var p in q)
            {
                p.UnitPrice += 1.00M;
            }

            //
            ChangeSet cs = db.GetChangeSet();
            Console.WriteLine("*** CHANGE SET ***");
            Console.WriteLine("Number of entities inserted: {0}", cs.Inserts.Count);
            Console.WriteLine("Number of entities updated:  {0}", cs.Updates.Count);
            Console.WriteLine("Number of entities deleted:  {0}", cs.Deletes.Count);
            Console.WriteLine();

            Console.WriteLine("*** GetOriginalEntityState ***");
            foreach (object o in cs.Updates)
            {
                Product p = o as Product;
                if (p != null)
                {
                    Product oldP = db.Products.GetOriginalEntityState(p);
                    Console.WriteLine("** Current **");
                    ObjectDumper.Write(p);
                    Console.WriteLine("** Original **");
                    ObjectDumper.Write(oldP);
                    Console.WriteLine();
                }
            }
            Console.WriteLine();

            Console.WriteLine("*** GetModifiedMembers ***");
            foreach (object o in cs.Updates)
            {
                Product p = o as Product;
                if (p != null)
                {
                    foreach (ModifiedMemberInfo mmi in db.Products.GetModifiedMembers(p))
                    {
                        Console.WriteLine("Member {0}", mmi.Member.Name);
                        Console.WriteLine("\tOriginal value: {0}", mmi.OriginalValue);
                        Console.WriteLine("\tCurrent  value: {0}", mmi.CurrentValue);
                    }
                }
            }

            db.SubmitChanges();

            Console.WriteLine();
            Console.WriteLine("*** AFTER ***");
            ObjectDumper.Write(q);

            Cleanup68();  // Восстановить предыдущее состояние базы данных
        }

        private void Cleanup68() {
            SetLogging(false);

            var q =
                from p in db.Products
                where p.CategoryID == 1
                select p;

            foreach (var p in q) {
                p.UnitPrice -= 1.00M;
            }
            db.SubmitChanges();
        }

        [Category("Insert/Update/Delete")]
        [Title("Delete - простой")]
        [Description("Пример использования метода DeleteOnSubmit для удаления сведений о заказе из " +
                     "объекта таблицы OrderDetail.  Вызов SubmitChanges приводит к фиксации этого " +
                     "удаления в базе данных.")]
        public void LinqToSqlInsert06() {
            Console.WriteLine("*** BEFORE ***");
            ObjectDumper.Write(from c in db.OrderDetails where c.OrderID == 10255 select c);


            Console.WriteLine();
            Console.WriteLine("*** DELETE ***");
            //Beverages
            OrderDetail orderDetail = db.OrderDetails.First(c => c.OrderID == 10255 && c.ProductID == 36);

            db.OrderDetails.DeleteOnSubmit(orderDetail);
            db.SubmitChanges();


            Console.WriteLine();
            Console.WriteLine("*** AFTER ***");
            ClearDBCache();
            ObjectDumper.Write(from c in db.OrderDetails where c.OrderID == 10255 select c);



            Cleanup69();  // Восстановить предыдущее состояние базы данных
        }

        private void Cleanup69() {
            SetLogging(false);

            OrderDetail orderDetail = new OrderDetail()
                                      {
                                          OrderID = 10255,
                                          ProductID = 36,
                                          UnitPrice = 15.200M,
                                          Quantity = 25,
                                          Discount = 0.0F
                                      };
            db.OrderDetails.InsertOnSubmit(orderDetail);

            db.SubmitChanges();
        }

        [Category("Insert/Update/Delete")]
        [Title("Delete - один-ко-многим")]
        [Description("Пример использования метода DeleteOnSubmit для удаления заказа и сведений о нем " +
                     "из таблиц OrderDetails и Orders. Сначала удаляются сведения о заказе, а затем " +
                     "сам заказ из таблицы Orders. Вызов SubmitChanges фиксирует это удаление в базе данных.")]
        public void LinqToSqlInsert07() {
            var orderDetails =
                from o in db.OrderDetails
                where o.Order.CustomerID == "WARTH" && o.Order.EmployeeID == 3
                select o;

            Console.WriteLine("*** BEFORE ***");
            ObjectDumper.Write(orderDetails);

            Console.WriteLine();
            Console.WriteLine("*** DELETE ***");
            var order =
                (from o in db.Orders
                 where o.CustomerID == "WARTH" && o.EmployeeID == 3
                 select o).First();

            foreach (OrderDetail od in orderDetails)
            {
                db.OrderDetails.DeleteOnSubmit(od);
            }

            db.Orders.DeleteOnSubmit(order);

            db.SubmitChanges();


            Console.WriteLine();
            Console.WriteLine("*** AFTER ***");
            ObjectDumper.Write(orderDetails);



            Cleanup70();  // Восстановить предыдущее состояние базы данных
        }

        private void Cleanup70() {
            SetLogging(false);

            Order order = new Order()
                          {
                              CustomerID = "WARTH",
                              EmployeeID = 3,
                              OrderDate = new DateTime(1996, 7, 26),
                              RequiredDate = new DateTime(1996, 9, 6),
                              ShippedDate = new DateTime(1996, 7, 31),
                              ShipVia = 3,
                              Freight = 25.73M,
                              ShipName = "Wartian Herkku",
                              ShipAddress = "Torikatu 38",
                              ShipCity = "Oulu",
                              ShipPostalCode="90110",
                              ShipCountry = "Finland"
                          };

                              //Order, Cus, Emp, OrderD, ReqD, ShiD, ShipVia, Frei, ShipN, ShipAdd, ShipCi, ShipReg, ShipPostalCost, ShipCountry
                              //10266	WARTH	3	1996-07-26 00:00:00.000	1996-09-06 00:00:00.000	1996-07-31 00:00:00.000	3	25.73	Wartian Herkku	Torikatu 38	Oulu	NULL	90110	Finland

            OrderDetail orderDetail = new OrderDetail()
                                      {
                                          ProductID = 12,
                                          UnitPrice = 30.40M,
                                          Quantity = 12,
                                          Discount = 0.0F
                                      };
            order.OrderDetails.Add(orderDetail);

            db.Orders.InsertOnSubmit(order);
            db.SubmitChanges();
        }


        [Category("Insert/Update/Delete")]
        [Title("Удаление - зависимое удаление")]
        [Description("Пример, иллюстрирующий, как зависимое удаление приводит к операции фактического удаления " +
                     "объекта сущности, когда сущность, на которую он ссылается, удаляет объект из своего EntitySet. " +
                     "Ситуация с зависимым удалением возникает только тогда, когда сопоставление связей сущности должно" +
                     " Для DeleteOnNull задано значение true, а для CanBeNull - false.")]
        public void LinqToSqlInsert08()
        {
            Console.WriteLine("*** BEFORE ***");
       
            ObjectDumper.Write(from o in db.Orders where o.OrderID == 10248 select o);
            ObjectDumper.Write(from d in db.OrderDetails where d.OrderID == 10248 select d);

            Console.WriteLine();
            Console.WriteLine("*** INFERRED DELETE ***");
            Order order = db.Orders.First(x => x.OrderID == 10248);
            OrderDetail od = order.OrderDetails.First(d => d.ProductID == 11);
            order.OrderDetails.Remove(od);

            db.SubmitChanges();

            Console.WriteLine();
            Console.WriteLine("*** AFTER ***");
            ClearDBCache();
            ObjectDumper.Write(from o in db.Orders where o.OrderID == 10248 select o);
            ObjectDumper.Write(from d in db.OrderDetails where d.OrderID == 10248 select d);
            CleanupInsert08();  // Восстановить предыдущее состояние базы данных
        }

        private void CleanupInsert08()
        {
            SetLogging(false);
            OrderDetail od = new OrderDetail() { ProductID = 11, Quantity = 12, UnitPrice = 14, OrderID = 10248, Discount = 0 };
            db.OrderDetails.InsertOnSubmit(od);
            db.SubmitChanges();
        }

        [Category("Insert/Update/Delete")]
        [Title("Insert - переопределение с помощью динамического CUD")]
        [Description("Образец использования частичного метода InsertRegion с предоставленным контекстом данных для вставки региона. " +
                     "Обращение к SubmitChanges вызывает InsertRegion, использующий динамический CUD для вызова " +
                     "по ссылке по умолчанию на SQL созданного SQL-запроса")]
        public void LinqToSqlInsert09()
        {
            Console.WriteLine("*** BEFORE ***");
            ObjectDumper.Write(from c in db.Regions where c.RegionID == 32 select c);


            Console.WriteLine();
            Console.WriteLine("*** INSERT OVERRIDE ***");
            //Beverages
            Region nwRegion = new Region() { RegionID = 32, RegionDescription = "Rainy" };

            db.Regions.InsertOnSubmit(nwRegion);
            db.SubmitChanges();


            Console.WriteLine();
            Console.WriteLine("*** AFTER ***");
            ClearDBCache();
            ObjectDumper.Write(from c in db.Regions where c.RegionID == 32 select c);

            CleanupInsert09();  // Восстановить предыдущее состояние базы данных
        }

        private void CleanupInsert09()
        {
            SetLogging(false);

            db.Regions.DeleteAllOnSubmit(from r in db.Regions where r.RegionID == 32 select r);
            db.SubmitChanges();
        }

        [Category("Insert/Update/Delete")]
        [Title("Update с присоединением" )]
        [Description("Этот пример получает сущности из другого слоя и использует методы Attach и AttachAll " +
                     "присоединить десериализованные сущности к контексту данных, а затем обновить их. " +
                     "Изменения занесены в базу данных.")]
        public void LinqToSqlInsert10()
        {
           
            // Обычно сущности для присоединения от десериализующего XML будут получаться с другого уровня.
            // Присоединение сущностей из одного DataContext к другому DataContext не поддерживается.  
            // Поэтому для дублирования десериализации сущностей они будут созданы здесь повторно.
            Customer c1;
            List<Order> deserializedOrders = new List<Order>();
            Customer deserializedC1;

            using (Northwind tempdb = new Northwind(connString))
            {
                c1 = tempdb.Customers.Single(c => c.CustomerID == "ALFKI");
                Console.WriteLine("Customer {0}'s original address {1}", c1.CustomerID, c1.Address);
                Console.WriteLine();
                deserializedC1 = new Customer { Address = c1.Address, City = c1.City,
                                                CompanyName=c1.CompanyName, ContactName=c1.ContactName,
                                                ContactTitle=c1.ContactTitle, Country=c1.Country,
                                                CustomerID=c1.CustomerID, Fax=c1.Fax,
                                                Phone=c1.Phone, PostalCode=c1.PostalCode,
                                                Region=c1.Region};
                Customer tempcust = tempdb.Customers.Single(c => c.CustomerID == "ANTON");
                foreach (Order o in tempcust.Orders)
                {
                    Console.WriteLine("Order {0} belongs to customer {1}", o.OrderID, o.CustomerID);
                    deserializedOrders.Add(new Order {CustomerID=o.CustomerID, EmployeeID=o.EmployeeID,
                                                      Freight=o.Freight, OrderDate=o.OrderDate, OrderID=o.OrderID,
                                                      RequiredDate=o.RequiredDate, ShipAddress=o.ShipAddress,
                                                      ShipCity=o.ShipCity, ShipName=o.ShipName,
                                                      ShipCountry=o.ShipCountry, ShippedDate=o.ShippedDate,
                                                      ShipPostalCode=o.ShipPostalCode, ShipRegion=o.ShipRegion,
                                                      ShipVia=o.ShipVia});
                }
                
                Console.WriteLine();

                Customer tempcust2 = tempdb.Customers.Single(c => c.CustomerID == "CHOPS");
                var c3Orders = tempcust2.Orders.ToList();
                foreach (Order o in c3Orders)
                {
                    Console.WriteLine("Order {0} belongs to customer {1}", o.OrderID, o.CustomerID);
                }
                Console.WriteLine();
            }

            using (Northwind db2 = new Northwind(connString))
            {
                // Для отслеживания изменений присоедините первую сущность к текущему контексту данных.
                db2.Customers.Attach(deserializedC1);
                Console.WriteLine("***** Update Customer ALFKI's address ******");
                Console.WriteLine();
                // Измените отслеживаемую сущность.
                deserializedC1.Address = "123 First Ave";

                // Присоедините все сущности в списке заказов.
                db2.Orders.AttachAll(deserializedOrders);
                // Обновите заказы, указав принадлежность другому клиенту.
                Console.WriteLine("****** Assign all Orders belong to ANTON to CHOPS ******");
                Console.WriteLine();

                foreach (Order o in deserializedOrders)
                {
                    o.CustomerID = "CHOPS";
                }

                // Примените изменения в текущем контексте данных.
                db2.SubmitChanges();
            }

            // Проверьте, что заказы записаны должным образом.
            using (Northwind db3 = new Northwind(connString))
            {
                Customer dbC1 = db3.Customers.Single(c => c.CustomerID == "ALFKI");

                Console.WriteLine("Customer {0}'s new address {1}", dbC1.CustomerID, dbC1.Address);
                Console.WriteLine();

                Customer dbC2 = db3.Customers.Single(c => c.CustomerID == "CHOPS");

                foreach (Order o in dbC2.Orders)
                {
                    Console.WriteLine("Order {0} belongs to customer {1}", o.OrderID, o.CustomerID);
                }
              
            }

            CleanupInsert10();
        }

        private void CleanupInsert10()
        {
            int[] c2OrderIDs = { 10365, 10507, 10535, 10573, 10677, 10682, 10856 };
            using (Northwind tempdb = new Northwind(connString))
            {
                Customer c1 = tempdb.Customers.Single(c => c.CustomerID == "ALFKI");
                c1.Address = "Obere Str. 57";
                foreach (Order o in tempdb.Orders.Where(p => c2OrderIDs.Contains(p.OrderID)))
                    o.CustomerID = "ANTON";
                tempdb.SubmitChanges();
            }
        }

        [Category("Insert/Update/Delete")]
        [Title("Обновить и удалить с вложением")]
        [Description("В данном примере сущности берутся из одного контекста, используются Attach и AttachAll " +
                     "для присоединения сущностей из другого контекста. Затем две записи обновляются, " +
                     "одна сущность удалена, а другая сущность добавлена. Изменения применяются к " +
                     "базе данных")]
        public void LinqToSqlInsert11()
        {
            // Обычно сущности для присоединения от десериализующего
            // XML с другого уровня.
            // Образец использования LoadWith для безотложной загрузки клиента и заказов
            // в один запрос и отключения отложенной загрузки.
            Customer cust = null;
            using (Northwind tempdb = new Northwind(connString))
            {
                DataLoadOptions shape = new DataLoadOptions();
                shape.LoadWith<Customer>(c => c.Orders);
                // Загрузите первую пользовательскую сущность и ее заказы.
                tempdb.LoadOptions = shape;
                tempdb.DeferredLoadingEnabled = false;
                cust = tempdb.Customers.First(x => x.CustomerID == "ALFKI");
            }

            Console.WriteLine("Customer {0}'s original phone number {1}", cust.CustomerID, cust.Phone);
            Console.WriteLine();

            foreach (Order o in cust.Orders)
            {
                Console.WriteLine("Customer {0} has order {1} for city {2}", o.CustomerID, o.OrderID, o.ShipCity);
            }

            Order orderA = cust.Orders.First();
            Order orderB = cust.Orders.First(x => x.OrderID > orderA.OrderID);

            using (Northwind db2 = new Northwind(connString))
            {
                // Для отслеживания изменений присоедините первую сущность к текущему контексту данных.
                db2.Customers.Attach(cust);
                // Присоедините соответствующие заказы для отслеживания; иначе они будут добавлены при подтверждении.
                db2.Orders.AttachAll(cust.Orders.ToList());

                // Обновите клиента.
                cust.Phone = "2345 5436";
                // Обновите первый заказ.
                orderA.ShipCity = "Redmond";
                // Удалите второй заказ.
                cust.Orders.Remove(orderB);
                // Создайте новый заказ и добавьте его клиенту.
                Order orderC = new Order() { ShipCity = "New York" };
                Console.WriteLine("Adding new order");
                cust.Orders.Add(orderC);

                //Теперь запишите все изменения
                db2.SubmitChanges();
            }

            // Проверьте, что изменения внесены должным образом.
            using (Northwind db3 = new Northwind(connString))
            {
                Customer newCust = db3.Customers.First(x => x.CustomerID == "ALFKI");
                Console.WriteLine("Customer {0}'s new phone number {1}", newCust.CustomerID, newCust.Phone);
                Console.WriteLine();

                foreach (Order o in newCust.Orders)
                {
                    Console.WriteLine("Customer {0} has order {1} for city {2}", o.CustomerID, o.OrderID, o.ShipCity);
                }
            }

            CleanupInsert11();
        }

        private void CleanupInsert11()
        {
            int[] alfkiOrderIDs = { 10643, 10692, 10702, 10835, 10952, 11011 };

            using (Northwind tempdb = new Northwind(connString))
            {
                Customer c1 = tempdb.Customers.Single(c => c.CustomerID == "ALFKI");
                c1.Phone = "030-0074321";
                Order oa = tempdb.Orders.Single(o => o.OrderID == 10643);
                oa.ShipCity = "Berlin";
                Order ob = tempdb.Orders.Single(o => o.OrderID == 10692);
                ob.CustomerID = "ALFKI";
                foreach (Order o in c1.Orders.Where(p => !alfkiOrderIDs.Contains(p.OrderID)))
                    tempdb.Orders.DeleteOnSubmit(o);

                tempdb.SubmitChanges();
            }
        }
        [Category("Одновременные изменения")]
        [Title("Нежесткая блокировка - 1")]
        [Description("Этот и следующий образцы демонстрируют нежесткую блокировку. В этом образце " +
                     "другой пользователь выполняет и записывает свое обновление продукта 1 до чтения данных вами, " +
                     "так что конфликт не возникает.")]
        public void LinqToSqlSimultaneous01() {
            Console.WriteLine("OTHER USER: ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~");

            // Открыть второе подключение к базе данных для имитации другого пользователя,
            // который собирается внести изменения в таблицу \"Продукты\"
            Northwind otherUser_db = new Northwind(connString) { Log = db.Log };

            var otherUser_product = otherUser_db.Products.First(p => p.ProductID == 1);
            otherUser_product.UnitPrice = 999.99M;
            otherUser_db.SubmitChanges();

            Console.WriteLine("~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~");


            Console.WriteLine();
            Console.WriteLine("YOU:  ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~");

            var product = db.Products.First(p => p.ProductID == 1);
            product.UnitPrice = 777.77M;

            bool conflict = false;
            try {
                db.SubmitChanges();
            }
            //OptimisticConcurrencyException
            catch (ChangeConflictException) {
                conflict = true;
            }

            Console.WriteLine();
            if (conflict) {
                Console.WriteLine("* * * OPTIMISTIC CONCURRENCY EXCEPTION * * *");
                Console.WriteLine("Another user has changed Product 1 since it was first requested.");
                Console.WriteLine("Backing out changes.");
            }
            else {
                Console.WriteLine("* * * COMMIT SUCCESSFUL * * *");
                Console.WriteLine("Changes to Product 1 saved.");
            }

            Console.WriteLine("~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~");



            Cleanup71();  // Восстановить предыдущее состояние базы данных
        }

        private void Cleanup71() {
            ClearDBCache();
            SetLogging(false);

            var product = db.Products.First(p => p.ProductID == 1);
            product.UnitPrice = 18.00M;
            db.SubmitChanges();
        }

        [Category("Одновременные изменения")]
        [Title("Нежесткая блокировка - 2")]
        [Description("Этот и предыдущий образцы демонстрируют нежесткую блокировку. В этом образце " +
                     "другой пользователь выполняет и записывает свое обновление продукта 1 после чтения данных вами, " +
                     "но до завершения вашего обновления, что вызовет конфликт нежесткой блокировки.  " +
                     "Выполняется откат ваших изменений, что позволяет получить новые обновленные данные " +
                     "из базы данных и принять решение относительно собственного обновления.")]
        public void LinqToSqlSimultaneous02()
        {
            Console.WriteLine("YOU:  ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~");

            var product = db.Products.First(p => p.ProductID == 1);

            Console.WriteLine("~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~");


            Console.WriteLine();
            Console.WriteLine("OTHER USER: ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~");

            // Открыть второе подключение к базе данных для имитации другого пользователя,
            // который собирается внести изменения в таблицу \"Продукты\".
            Northwind otherUser_db = new Northwind(connString) { Log = db.Log };

            var otherUser_product = otherUser_db.Products.First(p => p.ProductID == 1);
            otherUser_product.UnitPrice = 999.99M;
            otherUser_db.SubmitChanges();

            Console.WriteLine("~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~");


            Console.WriteLine();
            Console.WriteLine("YOU (continued): ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~");

            product.UnitPrice = 777.77M;

            bool conflict = false;
            try {
                db.SubmitChanges();
            }
            //OptimisticConcurrencyException
            catch (ChangeConflictException) {
                conflict = true;
            }

            Console.WriteLine();
            if (conflict) {
                Console.WriteLine("* * * OPTIMISTIC CONCURRENCY EXCEPTION * * *");
                Console.WriteLine("Another user has changed Product 1 since it was first requested.");
                Console.WriteLine("Backing out changes.");
            }
            else {
                Console.WriteLine("* * * COMMIT SUCCESSFUL * * *");
                Console.WriteLine("Changes to Product 1 saved.");
            }

            Console.WriteLine("~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ");



            Cleanup72();  // Восстановить предыдущее состояние базы данных
        }

        private void Cleanup72() {
            ClearDBCache();
            SetLogging(false);

            // сбой транзакции приведет к автоматическому откату данных к прежнему состоянию
        }

        [Category("Одновременные изменения")]
        [Title("Транзакции - неявные")]
        [Description("Образец неявной транзакции, созданной с помощью " +
                     "SubmitChanges.  Обновление поля \"На складе\" до prod2 " +
                     "сделает его значение отрицательным, что нарушает ограничение при проверке " +
                     "на сервере. Транзакция, обновляющая " +
                     "оба товара, прерывается с ошибкой, что приводит к откату всех изменений.")]
        public void LinqToSqlSimultaneous03()
        {
            Console.WriteLine("*** BEFORE ***");
            ObjectDumper.Write(from p in db.Products where p.ProductID == 4 select p);
            ObjectDumper.Write(from p in db.Products where p.ProductID == 5 select p);


            Console.WriteLine();
            Console.WriteLine("*** UPDATE WITH IMPLICIT TRANSACTION ***");
            try {
                Product prod1 = db.Products.First(p => p.ProductID == 4);
                Product prod2 = db.Products.First(p => p.ProductID == 5);
                prod1.UnitsInStock -= 3;
                prod2.UnitsInStock -= 5;    // Ошибка: это приведет к отрицательному количеству товаров на складе
                // db.SubmitChanges неявно использует транзакцию, так что
                // либо оба обновления принимаются, либо оба отклоняются
                db.SubmitChanges();
            }
            catch (System.Data.SqlClient.SqlException e) {
                Console.WriteLine(e.Message);
            }


            Console.WriteLine();
            Console.WriteLine("*** AFTER ***");
            ClearDBCache();
            ObjectDumper.Write(from p in db.Products where p.ProductID == 4 select p);
            ObjectDumper.Write(from p in db.Products where p.ProductID == 5 select p);



            Cleanup73();  // Восстановить предыдущее состояние базы данных
        }

        private void Cleanup73() {
            SetLogging(false);

            // сбой транзакции приведет к автоматическому откату данных к прежнему состоянию
        }

        [Category("Одновременные изменения")]
        [Title("Транзакции - явные")]
        [Description("Образец использования явной транзакции.  Здесь " +
                     "предоставляется больше защиты, включая чтение данных в " +
                     "транзакции, чтобы предотвратить исключения нежесткой блокировки.  " +
                     "Как и в предыдущем запросе, обновление до prod2 поля \"На складе\" " +
                     "делает значение отрицательным, что нарушает ограничение при проверке " +
                     "в базе данных. Транзакция, обновляющая обе " +
                     "таблицы \"Продукты\" не выполняется, что приведет к откату всех изменений.")]
        public void LinqToSqlSimultaneous04()
        {
            Console.WriteLine("*** BEFORE ***");
            ObjectDumper.Write(from p in db.Products where p.ProductID == 4 select p);
            ObjectDumper.Write(from p in db.Products where p.ProductID == 5 select p);


            Console.WriteLine();
            Console.WriteLine("*** UPDATE WITH EXPLICIT TRANSACTION ***");
            // Явное использование TransactionScope гарантирует, что
            // данные в базе данных останутся неизменными в промежутке между
            // чтением и записью
            using (TransactionScope ts = new TransactionScope()) {
                try {
                    Product prod1 = db.Products.First(p => p.ProductID == 4);
                    Product prod2 = db.Products.First(p => p.ProductID == 5);
                    prod1.UnitsInStock -= 3;
                    prod2.UnitsInStock -= 5;    // Ошибка: это приведет к отрицательному количеству товаров на складе
                    db.SubmitChanges();
                }
                catch (System.Data.SqlClient.SqlException e) {
                    Console.WriteLine(e.Message);
                }
            }


            Console.WriteLine();
            Console.WriteLine("*** AFTER ***");
            ClearDBCache();
            ObjectDumper.Write(from p in db.Products where p.ProductID == 4 select p);
            ObjectDumper.Write(from p in db.Products where p.ProductID == 5 select p);



            Cleanup74();  // Восстановить предыдущее состояние базы данных
        }

        private void Cleanup74() {
            SetLogging(false);

            // сбой транзакции приведет к автоматическому откату данных к прежнему состоянию
        }

        [Category("Null")]
        [Title("null")]
        [Description("Пример использования значения null для поиска сотрудников, " +
                     "которые не подчинены другому сотруднику.")]
        public void LinqToSqlNull01() {
            var q =
                from e in db.Employees
                where e.ReportsToEmployee == null
                select e;

            ObjectDumper.Write(q);
        }

        [Category("Null")]
        [Title("Nullable<T>.HasValue")]
        [Description("Пример использования Nullable<T>.HasValue для поиска сотрудников, " +
                     "которые не подчинены другому сотруднику.")]
        public void LinqToSqlNull02()
        {
            var q =
                from e in db.Employees
                where !e.ReportsTo.HasValue
                select e;

            ObjectDumper.Write(q);
        }

        [Category("Null")]
        [Title("Nullable<T>.Value")]
        [Description("Пример использования Nullable<T>.Value сотрудников, " +
                     "подчиненных другому сотруднику, чтобы вернуть " +
                     "его номер EmployeeID.  Следует заметить, что " +
                     "использовать .Value необязательно.")]
        public void LinqToSqlNull03()
        {
            var q =
                from e in db.Employees
                where e.ReportsTo.HasValue
                select new {e.FirstName, e.LastName, ReportsTo = e.ReportsTo.Value};

            ObjectDumper.Write(q);
        }

        [Category("Функции для работы со строками и датами")]
        [Title("Слияние строк")]
        [Description("Образец использования оператора + для слияния строковых полей " +
                     "и строковых литералов при формировании расчетного значения " +
                     "расположения.")]
        public void LinqToSqlString01()
        {
            var q =
                from c in db.Customers
                select new { c.CustomerID, Location = c.City + ", " + c.Country };

            ObjectDumper.Write(q, 1);
        }

        [Category("Функции для работы со строками и датами")]
        [Title("String.Length")]
        [Description("Образец использования свойства Length для поиска продуктов, у которых " +
                     "название короче 10 знаков.")]
        public void LinqToSqlString02()
        {
            var q =
                from p in db.Products
                where p.ProductName.Length < 10
                select p;

            ObjectDumper.Write(q);
        }

        [Category("Функции для работы со строками и датами")]
        [Title("String.Contains(подстрока)")]
        [Description("Образец использования метода Contains для поиска клиентов, у которых " +
                     "контактное имя содержит \"Anders\".")]
        public void LinqToSqlString03()
        {
            var q =
                from c in db.Customers
                where c.ContactName.Contains("Anders")
                select c;

            ObjectDumper.Write(q);
        }

        [Category("Функции для работы со строками и датами")]
        [Title("String.IndexOf(подстрока)")]
        [Description("Образец использования метода IndexOf для поиска первого экземпляра " +
                     "пробела в имени контактного лица каждого заказчика.")]
        public void LinqToSqlString04()
        {
            var q =
                from c in db.Customers
                select new {c.ContactName, SpacePos = c.ContactName.IndexOf(" ")};

            ObjectDumper.Write(q);
        }

        [Category("Функции для работы со строками и датами")]
        [Title("String.StartsWith(prefix)")]
        [Description("Образец использования метода StartsWith для поиска клиентов, у которых " +
                     "контактное имя начинается на \"Maria\".")]
        public void LinqToSqlString05()
        {
            var q =
                from c in db.Customers
                where c.ContactName.StartsWith("Maria")
                select c;

            ObjectDumper.Write(q);
        }

        [Category("Функции для работы со строками и датами")]
        [Title("String.EndsWith(суффикс)")]
        [Description("В этом примере используется метод EndsWith для поиска клиентов, у которых " +
                     "контактное имя оканчивается на 'Anders'.")]
        public void LinqToSqlString06()
        {
            var q =
                from c in db.Customers
                where c.ContactName.EndsWith("Anders")
                select c;

            ObjectDumper.Write(q);
        }

        [Category("Функции для работы со строками и датами")]
        [Title("String.Substring(начало)")]
        [Description("Образец использования метода Substring для возврата названий продуктов, начиная с " +
                     "с четвертой буквы.")]
        public void LinqToSqlString07()
        {
            var q =
                from p in db.Products
                select p.ProductName.Substring(3);

            ObjectDumper.Write(q);
        }

        [Category("Функции для работы со строками и датами")]
        [Title("String.Substring(start, length)")]
        [Description("Образец использования метода Substring для поиска клиентов, у которых " +
                     "домашний телефон содержит \"555\" с седьмой по девятую цифру.")]
        public void LinqToSqlString08()
        {
            var q =
                from e in db.Employees
                where e.HomePhone.Substring(6, 3) == "555"
                select e;

            ObjectDumper.Write(q);
        }

        [Category("Функции для работы со строками и датами")]
        [Title("String.ToUpper()")]
        [Description("Образец использования метода ToUpper для возврата имен сотрудников, " +
                     "где фамилия преобразована к верхнему регистру.")]
        public void LinqToSqlString09()
        {
            var q =
                from e in db.Employees
                select new {LastName = e.LastName.ToUpper(), e.FirstName};

            ObjectDumper.Write(q);
        }

        [Category("Функции для работы со строками и датами")]
        [Title("String.ToLower()")]
        [Description("Образец использования метода ToLower для возврата имен категорий, " +
                     "которые преобразованы к нижнему регистру.")]
        public void LinqToSqlString10()
        {
            var q =
                from c in db.Categories
                select c.CategoryName.ToLower();

            ObjectDumper.Write(q);
        }

        [Category("Функции для работы со строками и датами")]
        [Title("String.Trim()")]
        [Description("Образец использования метода Trim для возврата первых пяти " +
                     "цифр домашнего телефона сотрудника с удалением начальных и " +
                     "конечных пробелов.")]
        public void LinqToSqlString11() {
            var q =
                from e in db.Employees
                select e.HomePhone.Substring(0, 5).Trim();

            ObjectDumper.Write(q);
        }

        [Category("Функции для работы со строками и датами")]
        [Title("String.Insert(pos, str)")]
        [Description("Образец использования метода Insert для возврата последовательности " +
                     "домашних номеров сотрудников, содержащих в пятой позиции ), и " +
                     "вставки : после ).")]
        public void LinqToSqlString12() {
            var q =
                from e in db.Employees
                where e.HomePhone.Substring(4, 1) == ")"
                select e.HomePhone.Insert(5, ":");

            ObjectDumper.Write(q);
        }

        [Category("Функции для работы со строками и датами")]
        [Title("String.Remove(начало)")]
        [Description("Пример использования метода Remove для возврата последовательности " +
                     "домашних номеров сотрудников, содержащих в пятой позиции ), и " +
                     "удаляя все знаки, начиная с десятого.")]
        public void LinqToSqlString13() {
            var q =
                from e in db.Employees
                where e.HomePhone.Substring(4, 1) == ")"
                select e.HomePhone.Remove(9);

            ObjectDumper.Write(q);
        }

        [Category("Функции для работы со строками и датами")]
        [Title("String.Remove(начало, длина)")]
        [Description("Пример использования метода Remove для возврата последовательности " +
                     "домашних номеров сотрудников, содержащих в пятой позиции ), и " +
                     "удаления первых шести знаков.")]
        public void LinqToSqlString14() {
            var q =
                from e in db.Employees
                where e.HomePhone.Substring(4, 1) == ")"
                select e.HomePhone.Remove(0, 6);

            ObjectDumper.Write(q);
        }

        [Category("Функции для работы со строками и датами")]
        [Title("String.Replace(поиск, замена)")]
        [Description("Образец использования метода Replace для возврата последовательности " +
                     "сведений о поставщиках, где в поле страны значение " +
                     "UK заменено на United Kingdom, а USA на " +
                     "Соединенные Штаты Америки.")]
        public void LinqToSqlString15() {
            var q =
                from s in db.Suppliers
                select new {
                    s.CompanyName,
                    Country = s.Country.Replace("UK", "United Kingdom")
                                       .Replace("USA", "United States of America")
                };

            ObjectDumper.Write(q);
        }

        
        [Category("Функции для работы со строками и датами")]
        [Title("DateTime.Year")]
        [Description("Образец использования свойства Year поля даты-времени для " +
                     "поиска заказов, размещенных в 1997 г.")]
        public void LinqToSqlString16() {
            var q =
                from o in db.Orders
                where o.OrderDate.Value.Year == 1997
                select o;

            ObjectDumper.Write(q);
        }

        [Category("Функции для работы со строками и датами")]
        [Title("DateTime.Month")]
        [Description("Образец использования свойства Month поля даты-времени для " +
                     "поиска заказов, размещенных в декабре.")]
        public void LinqToSqlString17() {
            var q =
                from o in db.Orders
                where o.OrderDate.Value.Month == 12
                select o;

            ObjectDumper.Write(q);
        }

        [Category("Функции для работы со строками и датами")]
        [Title("DateTime.Day")]
        [Description("Образец использования свойства Day поля даты-времени для " +
                     "поиска заказов, размещенных 31-го числа.")]
        public void LinqToSqlString18() {
            var q =
                from o in db.Orders
                where o.OrderDate.Value.Day == 31
                select o;

            ObjectDumper.Write(q);
        }

        [Category("Удостоверение объекта")]
        [Title("Кэширование объектов – 1")]
        [Description("Образец получения ссылки на один и тот же объект в памяти при " +
                     "повторном выполнении одного и того же запроса.")]
        public void LinqToSqlObjectIdentity01() {
            Customer cust1 = db.Customers.First(c => c.CustomerID == "BONAP");
            Customer cust2 = db.Customers.First(c => c.CustomerID == "BONAP");

            Console.WriteLine("cust1 and cust2 refer to the same object in memory: {0}",
                              Object.ReferenceEquals(cust1, cust2));
        }

        [Category("Удостоверение объекта")]
        [Title("Кэширование объектов – 2")]
        [Description("В этом примере показано, как при выполнении различных запросов, которые " +
                     "возвращают одну и ту же строку из базы данных, будет получаться " +
                     "ссылка каждый раз на тот же объект в памяти.")]
        public void LinqToSqlObjectIdentity02() {
            Customer cust1 = db.Customers.First(c => c.CustomerID == "BONAP");
            Customer cust2 = (
                from o in db.Orders
                where o.Customer.CustomerID == "BONAP"
                select o )
                .First()
                .Customer;

            Console.WriteLine("cust1 and cust2 refer to the same object in memory: {0}",
                              Object.ReferenceEquals(cust1, cust2));
        }

        [Category("Загрузка объекта")]
        [Title("Отложенная загрузка - 1")]
        [Description("В данном примере показано, как переходы по отношениям в " +
                     "полученных объектах могут привести к запуску новых запросов к базе данных, " +
                     "если данные не были запрошены в исходном запросе.")]
        public void LinqToSqlObject01() {
            var custs =
                from c in db.Customers
                where c.City == "Sao Paulo"
                select c;

            foreach (var cust in custs) {
                foreach (var ord in cust.Orders) {
                    Console.WriteLine("CustomerID {0} has an OrderID {1}.", cust.CustomerID, ord.OrderID);
                }
            }
        }

        [Category("Загрузка объекта")]
        [Title("LoadWith - Безотлагательная загрузка - 1")]
        [Description("Образец использования LoadWith для запроса связанных данных " +
                     "во время исполнения оригинального запроса, чтобы дополнительные циклы обращений к " +
                     "базе данных не требовались позднее при переходах " +
                     "по выбранным объектам.")]
        public void LinqToSqlObject02() {

            Northwind db2 = new Northwind(connString);
            db2.Log = this.OutputStreamWriter;

            DataLoadOptions ds = new DataLoadOptions();
            ds.LoadWith<nwind.Customer>(p => p.Orders);

            db2.LoadOptions = ds;

            var custs = (
                from c in db2.Customers
                where c.City == "Sao Paulo"
                select c);

            foreach (var cust in custs) {
                foreach (var ord in cust.Orders) {
                    Console.WriteLine("CustomerID {0} has an OrderID {1}.", cust.CustomerID, ord.OrderID);
                }
            }
        }

        [Category("Загрузка объекта")]
        [Title("Отложенная загрузка + AssociateWith")]
        [Description("В данном примере показано, как переходы по отношениям в " +
                     "полученных объектах могут привести к запуску новых запросов к базе данных, " +
                     "если данные не были запрошены в исходном запросе. Также этот пример показывает, как объекты отношений " +
                     "могут фильтроваться с помощью Assoicate With после отложенной загрузки.")]
        public void LinqToSqlObject03() {

            Northwind db2 = new Northwind(connString);
            db2.Log = this.OutputStreamWriter;

            DataLoadOptions ds = new DataLoadOptions();
            ds.AssociateWith<nwind.Customer>(p => p.Orders.Where(o=>o.ShipVia > 1));

            db2.LoadOptions = ds;
            var custs =
                from c in db2.Customers
                where c.City == "London"
                select c;

            foreach (var cust in custs) {
                foreach (var ord in cust.Orders) {
                    foreach (var orderDetail in ord.OrderDetails) {
                        Console.WriteLine("CustomerID {0} has an OrderID {1} that ShipVia is {2} with ProductID {3} that has name {4}.",
                            cust.CustomerID, ord.OrderID, ord.ShipVia, orderDetail.ProductID, orderDetail.Product.ProductName);
                    }
                }
            }
        }

        [Category("Загрузка объекта")]
        [Title("LoadWith - Безотлагательная загрузка  + AssoicateWith")]
        [Description("Образец использования LoadWith для запроса связанных данных " +
                     "во время исполнения оригинального запроса, чтобы дополнительные циклы обращений к " +
                     "базе данных не требовались позднее при переходах " +
                     "по выбранным объектам. В этом примере также показано, как" +
                     "объекты отношений могут быть упорядочены с помощью Assoicate With после безотлагательной загрузки.")]
        public void LinqToSqlObject04() {

            Northwind db2 = new Northwind(connString);
            db2.Log = this.OutputStreamWriter;

            DataLoadOptions ds = new DataLoadOptions();
            ds.LoadWith<Customer>(p => p.Orders);
            ds.LoadWith<Order>(p => p.OrderDetails);
            ds.AssociateWith<Order>(p=>p.OrderDetails.OrderBy(o=>o.Quantity));

            db2.LoadOptions = ds;
         
            var custs = (
                from c in db2.Customers
                where c.City == "London"
                select c );

            foreach (var cust in custs) {
                foreach (var ord in cust.Orders) {
                    foreach (var orderDetail in ord.OrderDetails) {
                        Console.WriteLine("CustomerID {0} has an OrderID {1} with ProductID {2} that has Quantity {3}.",
                            cust.CustomerID, ord.OrderID, orderDetail.ProductID, orderDetail.Quantity );
                    }
                }
            }
        }

        private bool isValidProduct(Product p) {
            return p.ProductName.LastIndexOf('C') == 0;
        }

        [Category("Загрузка объекта")]
        [Title("Отложенная загрузка - (1:M)")]
        [Description("В данном примере показано, как переходы по отношениям в " +
             "полученных объектах могут привести к запуску новых запросов к базе данных, " +
             "если данные не были запрошены в исходном запросе.")]
        public void LinqToSqlObject05() {
            var emps = from e in db.Employees
                       select e;

            foreach (var emp in emps) {
                foreach (var man in emp.Employees) {
                    Console.WriteLine("Employee {0} reported to Manager {1}.", emp.FirstName, man.FirstName);
                }
            }
        }

        [Category("Загрузка объекта")]
        [Title("Отложенная загрузка - (Blob)")]
        [Description("В данном примере показано, как переходы по ссылкам в " +
             "полученных объектах могут привести к запуску новых запросов к базе данных, " +
             "если типом данных является Link.")]
        public void LinqToSqlObject06() {
            var emps = from c in db.Employees
                       select c;

            foreach (Employee emp in emps) {
                Console.WriteLine("{0}", emp.Notes);
            }
        }


        [Category("Загрузка объекта")]
        [Title("Переопределение загрузки")]
        [Description("Этот пример переопределяет частичный метод LoadProducts в классе категории. После загрузки продуктов категории"+ 
        " вызывается LoadProducts для загрузки продуктов, которые не прекращены в этой категории. ")]
        public void LinqToSqlObject07()
        {
            Northwind db2 = new Northwind(connString);

            DataLoadOptions ds = new DataLoadOptions();

            ds.LoadWith<nwind.Category>(p => p.Products);
            db2.LoadOptions = ds;

            var q = (
                from c in db2.Categories
                where c.CategoryID < 3
                select c);

            foreach (var cat in q)
            {
                foreach (var prod in cat.Products)
                {
                    Console.WriteLine("Category {0} has a ProductID {1} that Discontined = {2}.", cat.CategoryID, prod.ProductID, prod.Discontinued);
                }
            }
        }

        [Category("Операторы преобразования")]
        [Title("AsEnumerable")]
        [Description("Пример применения AsEnumerable для того, чтобы использовать реализацию IEnumerable<T> " +
                     "для Where на стороне клиента, вместо реализации IQueryable<T> " +
                     "по умолчанию с преобразованием в SQL и выполнением " +
                     "на сервере.  Это необходимо, поскольку конструкция WHERE " +
                     "ссылается на определенный пользователем метод isValidProduct на стороне клиента, " +
                     "который нельзя преобразовать в SQL.")]
        [LinkedMethod("isValidProduct")]
        public void LinqToSqlConversion01() {
            var q =
                from p in db.Products.AsEnumerable()
                where isValidProduct(p)
                select p;

            ObjectDumper.Write(q);
        }

        [Category("Операторы преобразования")]
        [Title("ToArray")]
        [Description("Образец использования ToArray для немедленного расчета  массива в запросе " +
                     "и получения третьего элемента.")]
        public void LinqToSqlConversion02() {
            var q =
                from c in db.Customers
                where c.City == "London"
                select c;

            Customer[] qArray = q.ToArray();
            ObjectDumper.Write(qArray[3], 0);
        }

        [Category("Операторы преобразования")]
        [Title("ToList")]
        [Description("Пример использования ToList для немедленного занесения запроса в List<T>.")]
        public void LinqToSqlConversion03() {
            var q =
                from e in db.Employees
                where e.HireDate >= new DateTime(1994, 1, 1)
                select e;

            List<Employee> qList = q.ToList();
            ObjectDumper.Write(qList, 0);
        }

        [Category("Операторы преобразования")]
        [Title("ToDictionary")]
        [Description("Пример использования ToDictionary для немедленного занесения запроса и " +
                     "ключевого выражения в Dictionary<K, T>.")]
        public void LinqToSqlConversion04() {
            var q =
                from p in db.Products
                where p.UnitsInStock <= p.ReorderLevel && !p.Discontinued
                select p;

            Dictionary<int, Product> qDictionary = q.ToDictionary(p => p.ProductID);

            foreach (int key in qDictionary.Keys) {
                Console.WriteLine("Key {0}:", key);
                ObjectDumper.Write(qDictionary[key]);
                Console.WriteLine();
            }
        }

        [Category("Direct SQL")]
        [Title("Запрос SQL")]
        [Description("Пример использования ExecuteQuery<T> для выполнения произвольного SQL-запроса и " +
                     "сопоставления строк результата с последовательностью объектов Product.")]
        public void LinqToSqlDirect01() {
            var products = db.ExecuteQuery<Product>(
                "SELECT [Product List].ProductID, [Product List].ProductName " +
                "FROM Products AS [Product List] " +
                "WHERE [Product List].Discontinued = 0 " +
                "ORDER BY [Product List].ProductName;"
            );

            ObjectDumper.Write(products);
        }

        [Category("Direct SQL")]
        [Title("Команда SQL")]
        [Description("Образец использования ExecuteCommand для выполнения произвольной команды SQL, " +
                     "в этом случае выполняется массовое обновление для увеличения цены за единицу на 1,00 для всех продуктов.")]
        public void LinqToSqlDirect02() {
            Console.WriteLine("*** BEFORE ***");
            ObjectDumper.Write(from p in db.Products select p);


            Console.WriteLine();
            Console.WriteLine("*** UPDATE ***");
            db.ExecuteCommand("UPDATE Products SET UnitPrice = UnitPrice + 1.00");


            Console.WriteLine();
            Console.WriteLine("*** AFTER ***");
            ClearDBCache();
            ObjectDumper.Write(from p in db.Products select p);



            Cleanup110();  // Восстановить предыдущее состояние базы данных
        }

        private void Cleanup110() {
            SetLogging(false);

            db.ExecuteCommand("UPDATE Products SET UnitPrice = UnitPrice - 1.00");
        }

        [Category("Взаимодействие ADO.NET")]
        [Title("Взаимодействие при подключении")]
        [Description("Образец использует существующее подключение ADO.NET для создания объекта БД \"Борей\", " +
                     "который может быть использован для выполнения запросов, в данном случае - запроса на возврат " +
                     "всех заказов со стоимостью доставки 500,00 или больше.")]
        public void LinqToSqlAdo01() {
            // Создать стандартное подключение ADO.NET:
            SqlConnection nwindConn = new SqlConnection(connString);
            nwindConn.Open();

            // ... другой код доступа к базе данных ADO.NET ... //

            // Использовать заранее созданное подключение ADO.NET для создания контекста данных:
            Northwind interop_db = new Northwind(nwindConn) { Log = db.Log };

            var orders =
                from o in interop_db.Orders
                where o.Freight > 500.00M
                select o;

            ObjectDumper.Write(orders);

            nwindConn.Close();
        }

        [Category("Взаимодействие ADO.NET")]
        [Title("Взаимодействие транзакции")]
        [Description("Образец использует существующее подключение ADO.NET для создания объекта БД \"Борей\", " +
                     "а затем обеспечивает ему общий доступ к транзакции ADO.NET. Транзакция " +
                     "используется как для выполнения команд SQL через подключение ADO.NET, так и для записи изменений " +
                     "с помощью объекта БД \"Борей\". Когда транзакция отменяется из-за " +
                     "нарушения ограничения при проверке, выполняется откат всех изменений, включая " +
                     "изменения, сделанные с помощью команд Sql, а также с помощью " +
                     "объект БД \"Борей\".")]
        public void LinqToSqlAdo02() {
            var q =
                from p in db.Products
                where p.ProductID == 3
                select p;

            Console.WriteLine("*** BEFORE ***");
            ObjectDumper.Write(q);


            Console.WriteLine();
            Console.WriteLine("*** INSERT ***");

            // Создать стандартное подключение ADO.NET:
            SqlConnection nwindConn = new SqlConnection(connString);
            nwindConn.Open();

            // Использовать заранее созданное подключение ADO.NET для создания контекста данных:
            Northwind interop_db = new Northwind(nwindConn) { Log = db.Log };

            SqlTransaction nwindTxn = nwindConn.BeginTransaction();

            try {
                SqlCommand cmd = new SqlCommand("UPDATE Products SET QuantityPerUnit = 'single item' WHERE ProductID = 3");
                cmd.Connection = nwindConn;
                cmd.Transaction = nwindTxn;
                cmd.ExecuteNonQuery();

                // Общий доступ к уже существующей транзакции ADO.NET:
                //interop_db.LocalTransaction = nwindTxn;
                interop_db.Transaction = nwindTxn;

                Product prod1 = interop_db.Products.First(p => p.ProductID == 4);
                Product prod2 = interop_db.Products.First(p => p.ProductID == 5);
                prod1.UnitsInStock -= 3;
                prod2.UnitsInStock -= 5;    // Ошибка: это приведет к отрицательному количеству товаров на складе

                interop_db.SubmitChanges();

                nwindTxn.Commit();
            }
            catch (Exception e) {
                // Если произошла ошибка транзакции, выполняется откат всех изменений,
                // включая все изменения, сделанные непосредственно через подключение ADO.NET
                Console.WriteLine(e.Message);
                Console.WriteLine("Error submitting changes... all changes rolled back.");
            }

            nwindConn.Close();


            Console.WriteLine();
            Console.WriteLine("*** AFTER ***");
            ClearDBCache();
            ObjectDumper.Write(q);



            Cleanup112();  // Восстановить предыдущее состояние базы данных
        }
        private void Cleanup112() {
            SetLogging(false);

            // сбой транзакции приведет к автоматическому откату данных к прежнему состоянию
        }

        [Category("Хранимые процедуры")]
        [Title("Скалярный возврат")]
        [Description("Образец использования сохраненной процедуры для возвращения данных о количестве клиентов в штате Вашингтон.")]
        public void LinqToSqlStoredProc01() {
            int count = db.CustomersCountByRegion("WA");

            Console.WriteLine(count);
        }

        [Category("Хранимые процедуры")]
        [Title("Один набор результатов")]
        [Description("Пример использования хранимой процедуры, чтобы вернуть значения полей CustomerID, ContactName, CompanyName" +
        " и City для сотрудников, находящихся в Лондоне.")]
        public void LinqToSqlStoredProc02() {
            ISingleResult<CustomersByCityResult> result = db.CustomersByCity("London");

            ObjectDumper.Write(result);
        }

        [Category("Хранимые процедуры")]
        [Title("Одиночный результирующий набор - множество возможных форм")]
        [Description("Пример использования хранимой процедуры для возврата списка " +
        "заказчиков в регионе 'WA'.  Форма возвращаемого результирующего набора зависит от переданного параметра. " +
        "Если значение параметра равно 1, будут возвращены все свойства объекта Customer. " +
        "Если значение параметра равно 2, будут возвращены свойства CustomerID, ContactName и CompanyName.")]
        public void LinqToSqlStoredProc03() {
            Console.WriteLine("********** Whole Customer Result-set ***********");
            IMultipleResults result = db.WholeOrPartialCustomersSet(1);
            IEnumerable<WholeCustomersSetResult> shape1 = result.GetResult<WholeCustomersSetResult>();

            ObjectDumper.Write(shape1);

            Console.WriteLine();
            Console.WriteLine("********** Partial Customer Result-set ***********");
            result = db.WholeOrPartialCustomersSet(2);
            IEnumerable<PartialCustomersSetResult> shape2 = result.GetResult<PartialCustomersSetResult>();

            ObjectDumper.Write(shape2);
        }

        [Category("Хранимые процедуры")]
        [Title("Несколько наборов результатов")]
        [Description("Пример использования хранимой процедуры для возврата сведений о заказчике 'SEVES' и всех его заказах.")]
        public void LinqToSqlStoredProc04() {
            IMultipleResults result = db.GetCustomerAndOrders("SEVES");

            Console.WriteLine("********** Customer Result-set ***********");
            IEnumerable<CustomerResultSet> customer = result.GetResult<CustomerResultSet>();
            ObjectDumper.Write(customer);
            Console.WriteLine();

            Console.WriteLine("********** Orders Result-set ***********");
            IEnumerable<OrdersResultSet> orders = result.GetResult<OrdersResultSet>();
            ObjectDumper.Write(orders);
        }

        [Category("Хранимые процедуры")]
        [Title("Внешние параметры")]
        [Description("Образец использования хранимой процедуры, возвращающей внешний параметр.")]
        public void LinqToSqlStoredProc05() {
            decimal? totalSales = 0;
            string customerID = "ALFKI";

            // Внешние параметры передаются по ссылке для поддержки сценариев, где
            // параметр является внутренним или внешним. В данном случае параметр только
            // внешний.
            db.CustomerTotalSales(customerID, ref totalSales);

            Console.WriteLine("Total Sales for Customer '{0}' = {1:C}", customerID, totalSales);
        }


        [Category("Пользовательские функции")]
        [Title("Скалярная функция - Select")]
        [Description("Образец использования скалярной пользовательской функции в проекции.")]
        public void LinqToSqlUserDefined01() {
            var q = from c in db.Categories
                    select new {c.CategoryID, TotalUnitPrice = db.TotalProductUnitPriceByCategory(c.CategoryID)};

            ObjectDumper.Write(q);
        }

        [Category("Пользовательские функции")]
        [Title("Скалярная функция - Where")]
        [Description("Образец использования скалярной пользовательской функции в предложении Where.")]
        public void LinqToSqlUserDefined02()
        {
            var q = from p in db.Products
                    where p.UnitPrice == db.MinUnitPriceByCategory(p.CategoryID)
                    select p;

            ObjectDumper.Write(q);
        }

        [Category("Пользовательские функции")]
        [Title("Табличная функция")]
        [Description("Образец выборки из табличной пользовательской функции.")]
        public void LinqToSqlUserDefined03()
        {
            var q = from p in db.ProductsUnderThisUnitPrice(10.25M)
                    where !(p.Discontinued ?? false)
                    select p;

            ObjectDumper.Write(q);
        }

        [Category("Пользовательские функции")]
        [Title("Табличная функция - Join")]
        [Description("Образец присоединения к результатам табличной пользовательской функции.")]
        public void LinqToSqlUserDefined04()
        {
            var q = from c in db.Categories
                    join p in db.ProductsUnderThisUnitPrice(8.50M) on c.CategoryID equals p.CategoryID into prods
                    from p in prods
                    select new {c.CategoryID, c.CategoryName, p.ProductName, p.UnitPrice};

            ObjectDumper.Write(q);
        }

        [Category("Функции контекста данных")]
        [Title("CreateDatabase() и DeleteDatabase() ")]
        [Description("Образец использования CreateDatabase() для создания новой базы данных на основе схемы NewCreateDB в Mapping.cs,  " +
                     "и DeleteDatabase() для удаления созданной базы данных.")]
        public void LinqToSqlDataContext01() {

            // Создание временной папки для хранения созданной базы данных 
            string userTempFolder = Environment.GetEnvironmentVariable("SystemDrive") + @"\LinqToSqlSamplesTemp";
            Directory.CreateDirectory(userTempFolder);

            Console.WriteLine("********** Create NewCreateDB ***********");
            string userMDF = System.IO.Path.Combine(userTempFolder, @"NewCreateDB.mdf");
            string connStr = String.Format(@"Data Source=.\SQLEXPRESS;AttachDbFilename={0};Integrated Security=True;Connect Timeout=30;User Instance=True; Integrated Security = SSPI;", userMDF);
            NewCreateDB newDB = new NewCreateDB(connStr);

            newDB.CreateDatabase();

            if (newDB.DatabaseExists() && File.Exists(Path.Combine(userTempFolder, @"NewCreateDB.mdf")))
                Console.WriteLine("NewCreateDB is created");
            else
                Console.WriteLine("Error: NewCreateDB is not successfully created");

            Console.WriteLine();
            Console.WriteLine("********* Insert data and query *********");

            var newRow = new Person { PersonID = 1, PersonName = "Peter", Age = 28 };

            newDB.Persons.InsertOnSubmit(newRow);
            newDB.SubmitChanges();

            var q = from x in newDB.Persons
                    select x;

            ObjectDumper.Write(q);

            Console.WriteLine();
            Console.WriteLine("************ Delete NewCreateDB **************");
            newDB.DeleteDatabase();

            if (File.Exists(Path.Combine(userTempFolder, @"NewCreateDB.mdf")))
                Console.WriteLine("Error: NewCreateDB is not yet deleted");
            else
                Console.WriteLine("NewCreateDB is deleted");

            // Удаление временной папки, созданной для этого примера 
            Directory.Delete(userTempFolder);

        }

        [Category("Функции контекста данных")]
        [Title("DatabaseExists()")]
        [Description("Образец использования DatabaseExists() для проверки существования базы данных.")]
        public void LinqToSqlDataContext02() {

            Console.WriteLine("*********** Verify Northwind DB exists ***********");
            if (db.DatabaseExists())
                Console.WriteLine("Northwind DB exists");
            else
                Console.WriteLine("Error: Northwind DB does not exist");

            Console.WriteLine();
            Console.WriteLine("********* Verify NewCreateDB does not exist **********");
            string userTempFolder = Environment.GetEnvironmentVariable("Temp");
            string userMDF = System.IO.Path.Combine(userTempFolder, @"NewCreateDB.mdf");
            NewCreateDB newDB = new NewCreateDB(userMDF);

            if (newDB.DatabaseExists())
                Console.WriteLine("Error: NewCreateDB DB exists");
            else
                Console.WriteLine("NewCreateDB DB does not exist");
        }

        [Category("Функции контекста данных")]
        [Title("SubmitChanges()")]
        [Description("Образец необходимости вызова SubmitChanges() для того, чтобы  " +
         "занести обновление в базу данных.")]
        public void LinqToSql1DataContext03() {
            Customer cust = db.Customers.First(c=>c.CustomerID == "ALFKI");

            Console.WriteLine("********** Original Customer CompanyName **********");
            var q = from x in db.Customers
                     where x.CustomerID == "ALFKI"
                     select x.CompanyName;

            Console.WriteLine();
            ObjectDumper.Write(q);

            Console.WriteLine();
            Console.WriteLine("*********** Update and call SubmitChanges() **********");

            cust.CompanyName = "CSharp Programming Firm";
            db.SubmitChanges();

            Console.WriteLine();
            ObjectDumper.Write(q);

            Console.WriteLine();
            Console.WriteLine("*********** Update and did not call SubmitChanges() **********");

            cust.CompanyName = "LinqToSql Programming Firm";

            Console.WriteLine();
            ObjectDumper.Write(q);

            Cleanup122();  // Восстановить предыдущее состояние базы данных      
        }

        private void Cleanup122() {
            SetLogging(false);
            Customer cust = db.Customers.First(c=>c.CustomerID == "ALFKI");
            cust.CompanyName = "Alfreds Futterkiste";
            db.SubmitChanges();
        }
 
        [Category("Функции контекста данных")]
        [Title("CreateQuery()")]
        [Description("Пример использования метода CreateQuery() для создания IQueryable<T> из выражения.")]
        public void LinqToSqlDataContext04() {

            var c1 = Expression.Parameter(typeof(Customer), "c");
            PropertyInfo City = typeof(Customer).GetProperty("City");

            var pred = Expression.Lambda<Func<Customer, bool>>(
                Expression.Equal(
                Expression.Property(c1, City),
                  Expression.Constant("Seattle")
               ), c1
            );

            IQueryable custs = db.Customers;
            Expression expr = Expression.Call(typeof(Queryable), "Where",
                new Type[] { custs.ElementType }, custs.Expression, pred);
            IQueryable<Customer> q = db.Customers.AsQueryable().Provider.CreateQuery<Customer>(expr);

            ObjectDumper.Write(q);
        }

        [Category("Функции контекста данных")]
        [Title("Журнал")]
        [Description("Образец использования Db.Log для включения и выключения отображения регистрации в журнале базы данных.")]
        public void LinqToSqlDataContext05() {
            Console.WriteLine("**************** Turn off DB Log Display *****************");
            db.Log = null;
            var q = from c in db.Customers
                    where c.City == "London"
                    select c;

            ObjectDumper.Write(q);

            Console.WriteLine();
            Console.WriteLine("**************** Turn on DB Log Display  *****************");

            db.Log = this.OutputStreamWriter;
            ObjectDumper.Write(q);


        }

        [Category("Дополнительно")]
        [Title("Динамический запрос - Select")]
        [Description("Образец динамического построения запроса для возврата контактного имени каждого клиента. " + 
                     "Метод GetCommand используется для получения T-SQL, созданного для запроса.")]
        public void LinqToSqlAdvanced01()
        {
            ParameterExpression param = Expression.Parameter(typeof(Customer), "c");
            Expression selector = Expression.Property(param, typeof(Customer).GetProperty("ContactName"));
            Expression pred = Expression.Lambda(selector, param);

            IQueryable<Customer> custs = db.Customers;
            Expression expr = Expression.Call(typeof(Queryable), "Select", new Type[] { typeof(Customer), typeof(string) }, Expression.Constant(custs), pred);
            IQueryable<string> query = db.Customers.AsQueryable().Provider.CreateQuery<string>(expr);

            System.Data.Common.DbCommand cmd = db.GetCommand(query);
            Console.WriteLine("Generated T-SQL:");
            Console.WriteLine(cmd.CommandText);
            Console.WriteLine();

            ObjectDumper.Write(query);
        }

        [Category("Дополнительно")]
        [Title("Динамический запрос - Where")]
        [Description("Образец динамического построения запроса для отбора клиентов в Лондоне.")]
        public void LinqToSqlAdvanced02()
        {
            IQueryable<Customer> custs = db.Customers;
            ParameterExpression param = Expression.Parameter(typeof(Customer), "c");
            Expression right = Expression.Constant("London");
            Expression left = Expression.Property(param, typeof(Customer).GetProperty("City"));
            Expression filter = Expression.Equal(left, right);
            Expression pred = Expression.Lambda(filter, param);

            Expression expr = Expression.Call(typeof(Queryable), "Where", new Type[] { typeof(Customer) }, Expression.Constant(custs), pred);
            IQueryable<Customer> query = db.Customers.AsQueryable().Provider.CreateQuery<Customer>(expr);
            ObjectDumper.Write(query);
        }

        [Category("Дополнительно")]
        [Title("Динамический запрос - OrderBy")]
        [Description("Образец динамического построения запроса для отбора клиентов в Лондоне" +
                     " и отправки им заказа с использованием контактного имени.")]
        public void LinqToSqlAdvanced03()
        {
            ParameterExpression param = Expression.Parameter(typeof(Customer), "c");

            Expression left = Expression.Property(param, typeof(Customer).GetProperty("City"));
            Expression right = Expression.Constant("London");
            Expression filter = Expression.Equal(left, right);
            Expression pred = Expression.Lambda(filter, param);

            IQueryable custs = db.Customers;

            Expression expr = Expression.Call(typeof(Queryable), "Where",
                new Type[] { typeof(Customer) }, Expression.Constant(custs), pred);

            expr = Expression.Call(typeof(Queryable), "OrderBy",
                new Type[] { typeof(Customer), typeof(string) }, custs.Expression, Expression.Lambda(Expression.Property(param, "ContactName"), param));


            IQueryable<Customer> query = db.Customers.AsQueryable().Provider.CreateQuery<Customer>(expr);

            ObjectDumper.Write(query);
        }


        [Category("Дополнительно")]
        [Title("Динамический запрос - Union")]
        [Description("Образец автоматического построения запроса на объединение для возврата последовательности всех стран, в которых " +
                     "живет заказчик или сотрудник.")]
        public void LinqToSqlAdvanced04()
        {
            IQueryable<Customer> custs = db.Customers;
            ParameterExpression param1 = Expression.Parameter(typeof(Customer), "e");
            Expression left1 = Expression.Property(param1, typeof(Customer).GetProperty("City"));
            Expression pred1 = Expression.Lambda(left1, param1);

            IQueryable<Employee> employees = db.Employees;
            ParameterExpression param2 = Expression.Parameter(typeof(Employee), "c");
            Expression left2 = Expression.Property(param2, typeof(Employee).GetProperty("City"));
            Expression pred2 = Expression.Lambda(left2, param2);

            Expression expr1 = Expression.Call(typeof(Queryable), "Select", new Type[] { typeof(Customer), typeof(string) }, Expression.Constant(custs), pred1);
            Expression expr2 = Expression.Call(typeof(Queryable), "Select", new Type[] { typeof(Employee), typeof(string) }, Expression.Constant(employees), pred2);

            IQueryable<string> q1 = db.Customers.AsQueryable().Provider.CreateQuery<string>(expr1);
            IQueryable<string> q2 = db.Employees.AsQueryable().Provider.CreateQuery<string>(expr2);

            var q3 = q1.Union(q2);

            ObjectDumper.Write(q3);
        }

        [Category("Дополнительно")]
        [Title("Удостоверение")]
        [Description("Образец вставки нового контакта и получения " +
                     "вновь присвоенного идентификатора контакта из базы данных.")]
        public void LinqToSqlAdvanced05() {

            Console.WriteLine("ContactID is marked as an identity column");
            Contact con = new Contact() { CompanyName = "New Era", Phone = "(123)-456-7890" };

            db.Contacts.InsertOnSubmit(con);
            db.SubmitChanges();

            Console.WriteLine();
            Console.WriteLine("The ContactID of the new record is {0}", con.ContactID);

            cleanup130(con.ContactID);

        }
        void cleanup130(int contactID) {
            SetLogging(false);
            Contact con = db.Contacts.Where(c=>c.ContactID == contactID).First();
            db.Contacts.DeleteOnSubmit(con);
            db.SubmitChanges();
        }

        [Category("Дополнительно")]
        [Title("Вложение в предложение FROM")]
        [Description("Образец использования OrderByDescending и Take для возврата " +
                     "прекращенных поставок 10 самых дорогих продуктов.")]
        public void LinqToSqlAdvanced06() {
            var prods = from p in db.Products.OrderByDescending(p => p.UnitPrice).Take(10)
                    where p.Discontinued
                    select p;

            ObjectDumper.Write(prods, 0);
        }

        [Category("Вид")]
        [Title("Запрос - анонимный тип")]
        [Description("Образец использования SELECT и WHERE для возврата последовательности счетов, " +
                     " городом доставки для которых является Лондон.")]
        public void LinqToSqlView01() {
            var q =
                from i in db.Invoices
                where i.ShipCity == "London"
                select new {i.OrderID, i.ProductName, i.Quantity, i.CustomerName};

            ObjectDumper.Write(q, 1);
        }

        [Category("Вид")]
        [Title("Запрос - сопоставление удостоверений")]
        [Description("Образец использования SELECT для запроса заказов по кварталам.")]
        public void LinqToSqlView02() {
            var q =
                from qo in db.QuarterlyOrders
                select qo;

            ObjectDumper.Write(q, 1);
        }

        [Category("Наследование")]
        [Title("Простой")]
        [Description("Образец возвращает все контакты, для которых в качестве города указан Лондон.")]
        public void LinqToSqlInheritance01()
        {
            var cons = from c in db.Contacts                       
                       select c;

            foreach (var con in cons) {
                Console.WriteLine("Company name: {0}", con.CompanyName);
                Console.WriteLine("Phone: {0}", con.Phone);
                Console.WriteLine("This is a {0}", con.GetType());
                Console.WriteLine();
            }

        }

        [Category("Наследование")]
        [Title("OfType")]
        [Description("Образец использования OfType для возвращения всех контактов клиентов.")]
        public void LinqToSqlInheritance02()
        {
            var cons = from c in db.Contacts.OfType<CustomerContact>()
                       select c;

            ObjectDumper.Write(cons, 0);
        }

        [Category("Наследование")]
        [Title("IS")]
        [Description("Образец использования IS для возвращения всех контактов доставки.")]
        public void LinqToSqlInheritance03()
        {
            var cons = from c in db.Contacts
                       where c is ShipperContact
                       select c;

            ObjectDumper.Write(cons, 0);
        }

        [Category("Наследование")]
        [Title("AS")]
        [Description("Пример использования AS для возврата FullContact или null.")]
        public void LinqToSqlInheritance04()
        {
            var cons = from c in db.Contacts
                       select c as FullContact;

            ObjectDumper.Write(cons, 0);
        }

        [Category("Наследование")]
        [Title("Cast")]
        [Description("Образец использования cast для получения контактов клиентов, проживающих в Лондоне.")]
        public void LinqToSqlInheritance05()
        {
            var cons = from c in db.Contacts
                       where c.ContactType == "Customer" && ((CustomerContact)c).City == "London"
                       select c;

            ObjectDumper.Write(cons, 0);
        }

        [Category("Наследование")]
        [Title("UseAsDefault")]
        [Description("Этот пример показывает, что неизвестный тип контакта  " +
                     "будет автоматически преобразован в тип контакта по умолчанию.")]
        public void LinqToSqlInheritance06()
        {
            Console.WriteLine("***** INSERT Unknown Contact using normal mapping *****");
            Contact contact = new Contact() { ContactType = null, CompanyName = "Unknown Company", Phone = "333-444-5555" };
            db.Contacts.InsertOnSubmit(contact);
            db.SubmitChanges();

            Console.WriteLine();
            Console.WriteLine("***** Query Unknown Contact using inheritance mapping *****");
            var con = (from c in db.Contacts
                       where c.CompanyName == "Unknown Company" && c.Phone == "333-444-5555"
                       select c).First();

            Console.WriteLine("The base class nwind.BaseContact had been used as default fallback type");
            Console.WriteLine("The discriminator value for con is unknown. So, its type should be {0}", con.GetType().ToString());

            cleanup140(contact.ContactID);

        }

        void cleanup140(int contactID) {
            SetLogging(false);
            Contact con = db.Contacts.Where(c=>c.ContactID == contactID).First();
            db.Contacts.DeleteOnSubmit(con);
            db.SubmitChanges();

        }

        [Category("Наследование")]
        [Title("Вставка новой записи")]
        [Description("Образец создания нового контакта доставки.")]
        public void LinqToSqlInheritance07()
        {
            Console.WriteLine("****** Before Insert Record ******");
            var ShipperContacts = from sc in db.Contacts.OfType<ShipperContact>()
                                     where sc.CompanyName == "Northwind Shipper"
                                     select sc;

            Console.WriteLine();
            Console.WriteLine("There is {0} Shipper Contact matched our requirement", ShipperContacts.Count());

            ShipperContact nsc = new ShipperContact() { CompanyName = "Northwind Shipper", Phone = "(123)-456-7890" };
            db.Contacts.InsertOnSubmit(nsc);
            db.SubmitChanges();

            Console.WriteLine();
            Console.WriteLine("****** After Insert Record ******");
            ShipperContacts = from sc in db.Contacts.OfType<ShipperContact>()
                               where sc.CompanyName == "Northwind Shipper"
                               select sc;

            Console.WriteLine();
            Console.WriteLine("There is {0} Shipper Contact matched our requirement", ShipperContacts.Count());
            db.Contacts.DeleteOnSubmit(nsc);
            db.SubmitChanges();

        }

        [Category("Внешнее сопоставление")]
        [Title("Загрузить и использовать внешнее сопоставление")]
        [Description("Образец создания контекста данных, использующего внешний источник сопоставления XML.")]
        public void LinqToSqlExternal01()
        {
            // загрузить источник сопоставления
            string path = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\Data\NorthwindMapped.map"));

            XmlMappingSource mappingSource = XmlMappingSource.FromXml(File.ReadAllText(path));

            // создание контекста с помощью источника сопоставления
            Mapped.NorthwindMapped nw = new Mapped.NorthwindMapped(db.Connection, mappingSource);

            // демонстрация использования сущности с внешним сопоставлением 
            Console.WriteLine("****** Externally-mapped entity ******");
            Mapped.Order order = nw.Orders.First();
            ObjectDumper.Write(order, 1);

            // демонстрация использования иерархии наследования с внешним сопоставлением
            var contacts = from c in nw.Contacts
                           where c is Mapped.EmployeeContact
                           select c;
            Console.WriteLine();
            Console.WriteLine("****** Externally-mapped inheritance hierarchy ******");
            foreach (var contact in contacts)
            {
                Console.WriteLine("Company name: {0}", contact.CompanyName);
                Console.WriteLine("Phone: {0}", contact.Phone);
                Console.WriteLine("This is a {0}", contact.GetType());
                Console.WriteLine();
            }

            // демонстрация использования хранимой процедуры с внешним сопоставлением
            Console.WriteLine();
            Console.WriteLine("****** Externally-mapped stored procedure ******");
            foreach (Mapped.CustOrderHistResult result in nw.CustomerOrderHistory("ALFKI"))
            {
                ObjectDumper.Write(result, 0);
            }

            // демонстрация использования определяемой пользователем скалярной функции с внешним сопоставлением
            Console.WriteLine();
            Console.WriteLine("****** Externally-mapped scalar UDF ******");
            var totals = from c in nw.Categories
                         select new { c.CategoryID, TotalUnitPrice = nw.TotalProductUnitPriceByCategory(c.CategoryID) };
            ObjectDumper.Write(totals);

            // демонстрация использования определяемой пользователем табличной функции с внешним сопоставлением
            Console.WriteLine();
            Console.WriteLine("****** Externally-mapped table-valued UDF ******");
            var products = from p in nw.ProductsUnderThisUnitPrice(9.75M)
                           where p.SupplierID == 8
                           select p;
            ObjectDumper.Write(products);
        }

        [Category("Нежесткая блокировка")]
        [Title("Получение сведений о конфликтах")]
        [Description("Образец получения изменений, приведших к исключению нежесткой блокировки.")]
        public void LinqToSqlOptimistic01() {
            Console.WriteLine("YOU:  ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~");
            Product product = db.Products.First(p => p.ProductID == 1);
            Console.WriteLine("~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~");
            Console.WriteLine();
            Console.WriteLine("OTHER USER: ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~");
            // Открыть второе подключение к базе данных для имитации другого пользователя,
            // который собирается внести изменения в таблицу \"Продукты\"                
            Northwind otherUser_db = new Northwind(connString) { Log = db.Log };
            Product otherUser_product = otherUser_db.Products.First(p => p.ProductID == 1);
            otherUser_product.UnitPrice = 999.99M;
            otherUser_product.UnitsOnOrder = 10;
            otherUser_db.SubmitChanges();
            Console.WriteLine("~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~");
            Console.WriteLine("YOU (continued): ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~");
            product.UnitPrice = 777.77M;

            bool conflictOccurred = false;
            try {
                db.SubmitChanges(ConflictMode.ContinueOnConflict);
            } catch (ChangeConflictException) {
                Console.WriteLine("* * * OPTIMISTIC CONCURRENCY EXCEPTION * * *");
                foreach (ObjectChangeConflict aConflict in /*ex.Conflicts*/db.ChangeConflicts) {
                    Product prod = (Product)aConflict.Object;
                    Console.WriteLine("The conflicting product has ProductID {0}", prod.ProductID);
                    Console.WriteLine();
                    Console.WriteLine("Conflicting members:");
                    Console.WriteLine();
                    foreach (MemberChangeConflict memConflict in aConflict.MemberConflicts) {
                        string name = memConflict.Member/*MemberInfo*/.Name;
                        string yourUpdate = memConflict.CurrentValue.ToString();
                        string original = memConflict.OriginalValue.ToString();
                        string theirUpdate = memConflict.DatabaseValue.ToString();
                        if (memConflict.IsModified/*HaveModified*/) {
                            Console.WriteLine("'{0}' was updated from {1} to {2} while you updated it to {3}",
                                              name, original, theirUpdate, yourUpdate);
                        } else {
                            Console.WriteLine("'{0}' was updated from {1} to {2}, you did not change it.",
                                name, original, theirUpdate);
                        }
                    }
                    Console.WriteLine();
                }
                conflictOccurred = true;
            }

            Console.WriteLine();
            if (!conflictOccurred) {

                Console.WriteLine("* * * COMMIT SUCCESSFUL * * *");
                Console.WriteLine("Changes to Product 1 saved.");
            }
            Console.WriteLine("~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ~ ");

            ResetProducts(); // очистка
        }

        private void ResetProducts() {
            ClearDBCache();
            Northwind dbClean = new Northwind(connString);
            Product[] prod = new Product[4];
            decimal[] price = new decimal[4] { 18.00M, 19.00M, 10.00M, 22.00M };
            for (int i = 0; i < 4; i++) {
                prod[i] = dbClean.Products.First(p => p.ProductID == i + 1);
                prod[i].UnitPrice = price[i];
            }
            prod[0].UnitsInStock = 39;
            prod[0].UnitsOnOrder = 0;
            dbClean.SubmitChanges();
        }

        //OptimisticConcurrencyConflict
        private void WriteConflictDetails(IEnumerable<ObjectChangeConflict> conflicts) {
            //OptimisticConcurrencyConflict
            foreach (ObjectChangeConflict conflict in conflicts)
            {
                DescribeConflict(conflict);
            }
        }

        private void DescribeConflict(ObjectChangeConflict conflict) {
            Product prod = (Product)conflict.Object;
            Console.WriteLine("Optimistic Concurrency Conflict in product {0}", prod.ProductID);
            //OptimisticConcurrencyMemberConflict
            foreach (MemberChangeConflict memConflict in conflict.MemberConflicts/*GetMemberConflicts()*/) {
                string name = memConflict.Member.Name;
                string yourUpdate = memConflict.CurrentValue.ToString();
                string original = memConflict.OriginalValue.ToString();
                string theirUpdate = memConflict.DatabaseValue.ToString();
                if (memConflict.IsModified) {
                    Console.WriteLine("'{0}' was updated from {1} to {2} while you updated it to {3}",
                                      name, original, theirUpdate, yourUpdate);
                } else {
                    Console.WriteLine("'{0}' was updated from {1} to {2}, you did not change it.",
                        name, original, theirUpdate);
                }
            }
        }

        [Category("Нежесткая блокировка")]
        [Title("Разрешение конфликтов: перезаписать текущие значения")]
        [Description("Пример автоматического разрешения конфликтов при совместной работе.\r\num"
               + "Параметр \"перезаписать текущие значения\" задает запись новых значений базы данных в клиентские объекты.")]
        public void LinqToSqlOptimistic02()
        {
            Northwind otherUser_db = new Northwind(connString);
            db.Log = null;

            Product product = db.Products.First(p => p.ProductID == 1);
            Console.WriteLine("You retrieve the product 1, it costs {0}", product.UnitPrice);
            Console.WriteLine("There are {0} units in stock, {1} units on order", product.UnitsInStock, product.UnitsOnOrder);
            Console.WriteLine();

            Console.WriteLine("Another user changes the price to 22.22 and UnitsInStock to 22");
            Product otherUser_product = otherUser_db.Products.First(p => p.ProductID == 1);
            otherUser_product.UnitPrice = 22.22M;
            otherUser_product.UnitsInStock = 22;
            otherUser_db.SubmitChanges();

            Console.WriteLine("You set the price of product 1 to 1.01 and UnitsOnOrder to 11");
            product.UnitPrice = 1.01M;
            product.UnitsOnOrder = 11;
            try {
                Console.WriteLine("You submit");
                Console.WriteLine();
                db.SubmitChanges();
            } catch (ChangeConflictException) {
                WriteConflictDetails(db.ChangeConflicts); // записывать измененные объекты и члены на консоль
                Console.WriteLine();
                Console.WriteLine("Resolve by overwriting current values");
                db.ChangeConflicts.ResolveAll(RefreshMode.OverwriteCurrentValues);
                db.SubmitChanges();
            }
            Console.WriteLine();
            Northwind dbResult = new Northwind(connString);
            Product result = dbResult.Products.First(p => p.ProductID == 1);
            Console.WriteLine("Now product 1 has price={0}, UnitsInStock={1}, UnitsOnOrder={2}",
                result.UnitPrice, result.UnitsInStock, result.UnitsOnOrder);
            Console.WriteLine();
            ResetProducts(); // очистка
        }

        [Category("Нежесткая блокировка")]
        [Title("Разрешение конфликтов: сохранить текущее значения")]
        [Description("Пример автоматического разрешения конфликтов при совместной работе.\r\num"
             + "Параметр \"сохранить текущее значения\" задает замену всех значений на текущие значения этого клиента.")]
        public void LinqToSqlOptimistic03()
        {
            Northwind otherUser_db = new Northwind(connString);
            db.Log = null;

            Product product = db.Products.First(p => p.ProductID == 1);
            Console.WriteLine("You retrieve the product 1, it costs {0}", product.UnitPrice);
            Console.WriteLine("There are {0} units in stock, {1} units on order", product.UnitsInStock, product.UnitsOnOrder);
            Console.WriteLine();

            Console.WriteLine("Another user changes the price to 22.22 and UnitsInStock to 22");
            Product otherUser_product = otherUser_db.Products.First(p => p.ProductID == 1);
            otherUser_product.UnitPrice = 22.22M;
            otherUser_product.UnitsInStock = 22;
            otherUser_db.SubmitChanges();

            Console.WriteLine("You set the price of product 1 to 1.01 and UnitsOnOrder to 11");
            product.UnitPrice = 1.01M;
            product.UnitsOnOrder = 11;
            try {
                Console.WriteLine("You submit");
                Console.WriteLine();
                db.SubmitChanges();
            } catch (ChangeConflictException) {
                WriteConflictDetails(db.ChangeConflicts); // записывать измененные объекты и члены на консоль
                Console.WriteLine();
                Console.WriteLine("Resolve by keeping current values");
                db.ChangeConflicts.ResolveAll(RefreshMode.KeepCurrentValues);
                db.SubmitChanges();
            }
            Console.WriteLine();
            Northwind dbResult = new Northwind(connString);
            Product result = dbResult.Products.First(p => p.ProductID == 1);
            Console.WriteLine("Now product 1 has price={0}, UnitsInStock={1}, UnitsOnOrder={2}",
                result.UnitPrice, result.UnitsInStock, result.UnitsOnOrder);
            Console.WriteLine();
            ResetProducts(); // очистка
        }

        [Category("Нежесткая блокировка")]
        [Title("Разрешение конфликтов: сохранить изменения")]
        [Description("Пример автоматического разрешения конфликтов при совместной работе.\r\num"
           + "Параметр \"сохранить изменений\" задает сохранение всех изменений, внесенные текущим пользователем, "
           + "и слияние изменений от других пользователей, если соответствующее поле не было изменено текущим пользователем.")]
        public void LinqToSqlOptimistic04()
        {
            Northwind otherUser_db = new Northwind(connString);
            db.Log = null;

            Product product = db.Products.First(p => p.ProductID == 1);
            Console.WriteLine("You retrieve the product 1, it costs {0}", product.UnitPrice);
            Console.WriteLine("There are {0} units in stock, {1} units on order", product.UnitsInStock, product.UnitsOnOrder);
            Console.WriteLine();

            Console.WriteLine("Another user changes the price to 22.22 and UnitsInStock to 22");
            Product otherUser_product = otherUser_db.Products.First(p => p.ProductID == 1);
            otherUser_product.UnitPrice = 22.22M;
            otherUser_product.UnitsInStock = 22;
            otherUser_db.SubmitChanges();

            Console.WriteLine("You set the price of product 1 to 1.01 and UnitsOnOrder to 11");
            product.UnitPrice = 1.01M;
            product.UnitsOnOrder = 11;
            try {
                Console.WriteLine("You submit");
                Console.WriteLine();
                db.SubmitChanges();
            } catch (ChangeConflictException) {
                WriteConflictDetails(db.ChangeConflicts); // записывать измененные объекты и члены на консоль
                Console.WriteLine();
                Console.WriteLine("Resolve by keeping changes");
                db.ChangeConflicts.ResolveAll(RefreshMode.KeepChanges);
                db.SubmitChanges();
            }
            Console.WriteLine();
            Northwind dbResult = new Northwind(connString);
            Product result = dbResult.Products.First(p => p.ProductID == 1);
            Console.WriteLine("Now product 1 has price={0}, UnitsInStock={1}, UnitsOnOrder={2}",
                result.UnitPrice, result.UnitsInStock, result.UnitsOnOrder);
            Console.WriteLine();
            ResetProducts(); // очистка
        }

        [Category("Нежесткая блокировка")]
        [Title("Пользовательское правило разрешения")]
        [Description("Демонстрация использования MemberConflict.Resolve для записи пользовательского правила разрешения.\r\num")]
        public void LinqToSqlOptimistic05()
        {
            Northwind otherUser_db = new Northwind(connString);
            db.Log = null;

            Product product = db.Products.First(p => p.ProductID == 1);
            Console.WriteLine("You retrieve the product 1, it costs {0}", product.UnitPrice);
            Console.WriteLine("There are {0} units in stock, {1} units on order", product.UnitsInStock, product.UnitsOnOrder);
            Console.WriteLine();

            Console.WriteLine("Another user changes the price to 22.22 and UnitsOnOrder to 2");
            Product otherUser_product = otherUser_db.Products.First(p => p.ProductID == 1);
            otherUser_product.UnitPrice = 22.22M;
            otherUser_product.UnitsOnOrder = 2;
            otherUser_db.SubmitChanges();

            Console.WriteLine("You set the price of product 1 to 1.01 and UnitsOnOrder to 11");
            product.UnitPrice = 1.01M;
            product.UnitsOnOrder = 11;
            bool needsSubmit = true;
            while (needsSubmit) {
                try {
                    Console.WriteLine("You submit");
                    Console.WriteLine();
                    needsSubmit = false;
                    db.SubmitChanges();
                } catch (ChangeConflictException) {
                    needsSubmit = true;
                    WriteConflictDetails(db.ChangeConflicts); // записывать измененные объекты и члены на консоль
                    Console.WriteLine();
                    Console.WriteLine("Resolve by higher price / order");
                    foreach (ObjectChangeConflict conflict in db.ChangeConflicts) {
                        conflict.Resolve(RefreshMode.KeepChanges);
                        foreach (MemberChangeConflict memConflict in conflict.MemberConflicts) {
                            if (memConflict.Member.Name == "UnitPrice") {
                                //всегда использовать максимальную цену
                                decimal theirPrice = (decimal)memConflict.DatabaseValue;
                                decimal yourPrice = (decimal)memConflict.CurrentValue;
                                memConflict.Resolve(Math.Max(theirPrice, yourPrice));
                            } else if (memConflict.Member.Name == "UnitsOnOrder") {
                                //всегда использовать более высокий заказ
                                short theirOrder = (short)memConflict.DatabaseValue;
                                short yourOrder = (short)memConflict.CurrentValue;
                                memConflict.Resolve(Math.Max(theirOrder, yourOrder));
                            }
                        }
                    }
                }
            }
            Northwind dbResult = new Northwind(connString);
            Product result = dbResult.Products.First(p => p.ProductID == 1);
            Console.WriteLine("Now product 1 has price={0}, UnitsOnOrder={1}",
                result.UnitPrice, result.UnitsOnOrder);
            Console.WriteLine();
            ResetProducts(); // очистка
        }

        [Category("Нежесткая блокировка")]
        [Title("Submit с параметром FailOnFirstConflict")]
        [Description("Submit(FailOnFirstConflict) создает исключение нежесткой блокировки при обнаружении первого конфликта.\r\num"
           + "Каждый раз обрабатывается только одно исключение; пользователь должен выполнить занесение для каждого конфликта.")]
        public void LinqToSqlOptimistic06()
        {
            db.Log = null;
            Northwind otherUser_db = new Northwind(connString);

            // вы загрузили 3 товара
            Product[] prod = db.Products.OrderBy(p => p.ProductID).Take(3).ToArray();
            for (int i = 0; i < 3; i++) {
                Console.WriteLine("You retrieve the product {0}, it costs {1}", i + 1, prod[i].UnitPrice);
            }
            // другой пользователь изменяет эти продукты
            Product[] otherUserProd = otherUser_db.Products.OrderBy(p => p.ProductID).Take(3).ToArray();
            for (int i = 0; i < 3; i++) {
                decimal otherPrice = (i + 1) * 111.11M;
                Console.WriteLine("Other user changes the price of product {0} to {1}", i + 1, otherPrice);
                otherUserProd[i].UnitPrice = otherPrice;
            }
            otherUser_db.SubmitChanges();
            Console.WriteLine("Other user submitted changes");

            // вы изменяете загруженные продукты
            for (int i = 0; i < 3; i++) {
                decimal yourPrice = (i + 1) * 1.01M;
                Console.WriteLine("You set the price of product {0} to {1}", i + 1, yourPrice);
                prod[i].UnitPrice = yourPrice;
            }

            // отправить
            bool needsSubmit = true;
            while (needsSubmit) {
                try {
                    Console.WriteLine("======= You submit with FailOnFirstConflict =======");
                    needsSubmit = false;
                    db.SubmitChanges(ConflictMode.FailOnFirstConflict);
                } catch (ChangeConflictException) {
                    foreach (ObjectChangeConflict conflict in db.ChangeConflicts) {
                        DescribeConflict(conflict); // записать изменения в консоль
                        Console.WriteLine("Resolve conflict with KeepCurrentValues");
                        conflict.Resolve(RefreshMode.KeepCurrentValues);
                    }
                    needsSubmit = true;
                }
            }
            Northwind dbResult = new Northwind(connString);
            for (int i = 0; i < 3; i++) {
                Product result = dbResult.Products.First(p => p.ProductID == i + 1);
                Console.WriteLine("Now the product {0} has price {1}", i + 1, result.UnitPrice);
            }
            ResetProducts(); // очистка
        }

        [Category("Нежесткая блокировка")]
        [Title("Submit с параметром ContinueOnConflict")]
        [Description("Submit(ContinueOnConflict) собирает все конфликты взаимных блокировок и создает исключение, когда обнаружен последний конфликт.\r\num"
           + "Все конфликты обрабатываются в одном операторе catch.\r\num"
           + "Есть возможность, что другой пользователь обновил те же объекты до выполнения данного обновления. Поэтому возможно, что выдано еще одно исключение нежесткой блокировки, которым необходимо будет обработать.")]
        public void LinqToSqlOptimistic07()
        {
            db.Log = null;
            Northwind otherUser_db = new Northwind(connString);

            // вы загрузили 3 товара
            Product[] prod = db.Products.OrderBy(p => p.ProductID).Take(3).ToArray();
            for (int i = 0; i < 3; i++) {
                Console.WriteLine("You retrieve the product {0}, it costs {1}", i + 1, prod[i].UnitPrice);
            }
            // другой пользователь изменяет эти продукты
            Product[] otherUserProd = otherUser_db.Products.OrderBy(p => p.ProductID).Take(3).ToArray();
            for (int i = 0; i < 3; i++) {
                decimal otherPrice = (i + 1) * 111.11M;
                Console.WriteLine("Other user changes the price of product {0} to {1}", i + 1, otherPrice);
                otherUserProd[i].UnitPrice = otherPrice;
            }
            otherUser_db.SubmitChanges();
            Console.WriteLine("Other user submitted changes");

            // вы изменяете загруженные продукты
            for (int i = 0; i < 3; i++) {
                decimal yourPrice = (i + 1) * 1.01M;
                Console.WriteLine("You set the price of product {0} to {1}", i + 1, yourPrice);
                prod[i].UnitPrice = yourPrice;
            }

            // отправить
            bool needsSubmit = true;
            while (needsSubmit) {
                try {
                    Console.WriteLine("======= You submit with ContinueOnConflict =======");
                    needsSubmit = false;
                    db.SubmitChanges(ConflictMode.ContinueOnConflict);
                } catch (ChangeConflictException) {
                    foreach (ObjectChangeConflict conflict in db.ChangeConflicts) {
                        DescribeConflict(conflict); // записать изменения в консоль
                        Console.WriteLine("Resolve conflict with KeepCurrentValues");
                        conflict.Resolve(RefreshMode.KeepCurrentValues);
                    }
                    needsSubmit = true;
                }
            }
            Northwind dbResult = new Northwind(connString);
            for (int i = 0; i < 3; i++) {
                Product result = dbResult.Products.First(p => p.ProductID == i + 1);
                Console.WriteLine("Now the product {0} has price {1}", i + 1, result.UnitPrice);
            }
            ResetProducts(); // очистка
        }


      
        [Category("Методы частичного расширения")]
        [Title("Update с параметром OnValidate")]
        [Description("Этот пример переопределяет частичный метод OnValidate для классе заказа. Во время обновления заказа проверяется"
        +" Значение \"Способ доставки\" не может превышать 100, иначе выдается исключение, а обновления не заносятся в базу данных.")]
        public void LinqToSqlExtensibility01()
        {

            var order = (from o in db.Orders
                         where o.OrderID == 10355
                         select o).First();
            ObjectDumper.Write(order);
            Console.WriteLine();

            Console.WriteLine("***** Update Order to set ShipVia to 120 and submit changes ******");
            Console.WriteLine();

            order.ShipVia = 120;
            try
            {
                db.SubmitChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("****** Catch exception throw by OnValidate() ******");
                Console.WriteLine(e.Message);
            }

            Console.WriteLine();
            Console.WriteLine("****** verify that order's ShipVia didn't get changed in db. ******");
            Northwind db2 = new Northwind(connString);
            var order2 = (from o in db2.Orders
                          where o.OrderID == 10355
                          select o).First();

            ObjectDumper.Write(order2);

             
        }

       

        public override void InitSample() {
            ClearDBCache();
            SetLogging(true);
        }

        public void ClearDBCache() {
            // Создает новый объект БД \"Борей\" для нового запуска с пустым кэшем объектов
            // Объект БД \"Борей\" будет повторно использовать активное соединение ADO.NET

            TextWriter oldLog;
            if (db == null)
                oldLog = null;
            else
                oldLog = db.Log;

            db = new Northwind(connString) { Log = oldLog };
        }

        public void SetLogging(bool logging) {
            if (logging) {
                db.Log = this.OutputStreamWriter;
            }
            else {
                db.Log = null;
            }
        }

        public override void HandleException(Exception e) {
            Console.WriteLine("Unable to connect to the Northwind database on SQL Server instance.");
            Console.WriteLine("Try restarting SQL Server or your computer.");
            Console.WriteLine();
            Console.WriteLine("If the problem persists, see the Troubleshooting section of the Readme for tips.");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Exception:");
            base.HandleException(e);
        }

        public class Name {
            public string FirstName;
            public string LastName;
        }
    }
}
