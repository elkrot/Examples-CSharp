using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace ConnectToSqlServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string connectionString = GetConnectionString();
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    //don't forget to pass the connection object to the command
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Books", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        //SqlDataReader reads the rows one at a time from the database
                        //as you request them
                        while (reader.Read())
                        {
                            Console.WriteLine("{0}\t{1}\t{2}", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.Write(ex);
            }
            Console.ReadKey();            
        }

        //you shouldn't store connection strings in source code, but this is
        //to illustrate SQL access, so I'm keeping it simple
        static string GetConnectionString()
        {
            //change data source to point to your local copy of SQL Server
            return @"Data source=BEN-DESKTOP\SQLEXPRESS;Initial Catalog=TestDB;Integrated Security=SSPI";
        }
    }
}
