using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DetectIfDatabaseDown
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DB is {0}", TestConnection() ? "OK" : "Down");
            Console.WriteLine("Try shutting down the database server and running this test again");
            
            Console.ReadKey();
        }

        private static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GetConnectionString()))
                {
                    conn.Open();
                    return (conn.State == ConnectionState.Open);
                }
            }
            catch (SqlException)
            {
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
        }

        private static string GetConnectionString()
        {
            return @"Data source=BEN-DESKTOP\SQLEXPRESS;Initial Catalog=TestDB;Integrated Security=SSPI";
        }
    }
}
