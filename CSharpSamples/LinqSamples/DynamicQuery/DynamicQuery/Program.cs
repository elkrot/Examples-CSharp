// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Linq.Dynamic;
using System.Windows.Forms;
using NorthwindMapping;

namespace Dynamic
{
    class Program
    {
        static void Main(string[] args)
        {
            // Чтобы этот пример заработал, необходим активный сервер базы данных или экспресс-выпуск SQL Server.
            // Это подключение к проекту образцов данных, который поставляется вместе с Microsoft Visual Studio 2008.
            string dbPath = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\..\..\Data\NORTHWND.MDF"));
            string sqlServerInstance = @".\SQLEXPRESS";
            string connString = "AttachDBFileName='" + dbPath + "';Server='" + sqlServerInstance + "';user instance=true;Integrated Security=SSPI;Connection Timeout=60";
            
            // Это дополнительная строка для подключения, которую при необходимости можно изменить.
            // string connString = "server=test;database=northwind;user id=test;password=test";

            Northwind db = new Northwind(connString); 
            db.Log = Console.Out;

            var query =
                db.Customers.Where("City == @0 and Orders.Count >= @1", "London", 10).
                OrderBy("CompanyName").
                Select("New(CompanyName as Name, Phone)");

            Console.WriteLine(query);
            Console.ReadLine();
        }
    }
}
