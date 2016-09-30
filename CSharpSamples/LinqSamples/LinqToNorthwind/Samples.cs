// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using nwind;

namespace LinqToNorthwind {
    public static class Samples {

        public static void Sample1(Northwind db) 
        { 
            // использовать Where() для нахождения только соответственных элементов
            IEnumerable<Customer> q = db.Customers.Where(c => c.City == "London");
            ObjectDumper.Write(q, 0);
        }

        public static void Sample2(Northwind db)
        {
            // использовать First() вместо Where() для нахождения первого или единственного элемента 
            Customer cust = db.Customers.First(c => c.CustomerID == "ALFKI");
            ObjectDumper.Write(cust, 0);
        }

        public static void Sample3(Northwind db) 
        {
            // Использовать Select() для сопоставления результатов
            var q = db.Customers.Select(c => c.ContactName);
            ObjectDumper.Write(q, 0);
        }

        public static void Sample4(Northwind db)
        {
            // использовать конструкторы анонимных типов только для извлечения соответствующих данных
            var q = db.Customers.Select(c => new {c.ContactName, c.Phone});
            ObjectDumper.Write(q, 0);
        }

        public static void Sample5(Northwind db) 
        {
            // Объединить Where() и Select() для общих запросов
            var q = db.Customers.Where(c => c.City == "London").Select(c => c.ContactName);
            ObjectDumper.Write(q, 0);
        }

        public static void Sample6(Northwind db) 
        { 
            // использовать SelectMany() для выравнивания коллекций
            IEnumerable<Order> q = db.Customers.SelectMany(c => c.Orders);
            ObjectDumper.Write(q, 0);
        }

        public static void Sample7(Northwind db) 
        {
            // использовать выражения запроса для упрощения общих условий select/where
            var q = from c in db.Customers
                    from o in c.Orders
                    where c.City == "London"
                    select new {c.ContactName, o.OrderDate};
            ObjectDumper.Write(q, 0);
        }

        public static void Sample8(Northwind db) 
        {
            // использовать orderby для упорядочения результатов
            var q = from c in db.Customers orderby c.City, c.ContactName select c;
            ObjectDumper.Write(q, 0);
        }

        public static void Sample9(Northwind db) 
        {
            // использовать группировку x по y, чтобы создать набор разделов групп
            var q = from p in db.Products group p by p.CategoryID into Group select new {CategoryID=Group.Key, Group};
            ObjectDumper.Write(q, 1);
        }

        public static void Sample10(Northwind db) 
        {
            // использовать группировку по и агрегирование, например Min()/Max(), для вычисления значений по разделам групп
            var q = from p in db.Products
                    group p by p.CategoryID into g
                    select new {
                        Category = g.Key,
                        MinPrice = g.Min(p => p.UnitPrice),
                        MaxPrice = g.Max(p => p.UnitPrice)
                        };
            ObjectDumper.Write(q, 1);
        }

        public static void Sample11(Northwind db) 
        {
            // использовать Any(), чтобы определить, содержится ли в коллекции, по крайней мере, один элемент или, по крайней мере, один элемент соответствует условию
            var q = from c in db.Customers
                    where c.Orders.Any()
                    select c;
            ObjectDumper.Write(q, 0);
        }

        public static void Sample12(Northwind db) 
        {
            // использовать All() для определения, все ли элементы в коллекции соответствуют условию (или коллекция является пустой)
            var q = from c in db.Customers
                    where c.Orders.All(o => o.ShipCity == c.City)
                    select c;
            ObjectDumper.Write(q, 0);
        }

        public static void Sample13(Northwind db) 
        {
            // использовать Take(n), чтобы ограничить последовательность только первыми n элементами
            var q = db.Customers.OrderBy(c => c.ContactName).Take(5);
            ObjectDumper.Write(q, 0);
        }

        public static void Sample14(Northwind db) 
        {
            // использовать SubmitChanges() для отправки всех изменений обратно в базу данных
            Customer cust = db.Customers.First(c => c.CustomerID == "ALFKI");
            cust.ContactTitle = "Sith Lord";
            // другие изменения ...
            db.SubmitChanges();
        }
        
        public static void Sample15(Northwind db) 
        {
            using(System.Transactions.TransactionScope ts = new System.Transactions.TransactionScope()) {            
                // использовать SubmitChanges() для отправки всех изменений обратно в базу данных
                Customer cust = db.Customers.First(c => c.CustomerID == "ALFKI");
                cust.ContactTitle = "Sith Lord";
                // другие изменения ...
                db.SubmitChanges();
                ts.Complete();
            }
        }        
    }
}
