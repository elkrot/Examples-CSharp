using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace DBToXML
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument doc = new XmlDocument();
            DataSet dataSet = new DataSet("Books");
            using (SqlConnection conn = new SqlConnection(GetConnectionString()))
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM Books", conn))
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                adapter.Fill(dataSet);
            }

            StringBuilder sb = new StringBuilder();
            using (StringWriter sw = new StringWriter(sb))
            {
                dataSet.WriteXml(sw);
            }

            Console.WriteLine(sb.ToString());
            Console.ReadKey();
        }

        static string GetConnectionString()
        {
            //change data source to point to your local copy of SQL Server
            return @"Data source=BEN-DESKTOP\SQLEXPRESS;Initial Catalog=TestDB;Integrated Security=SSPI";
        }
    }
}
