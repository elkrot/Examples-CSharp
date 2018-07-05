using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace UseTransactions
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
                    
                    Console.WriteLine("Before attempted inserts: ");
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Books", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0}\t{1}\t{2}", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                        }
                    }
                    
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand cmd = new SqlCommand("INSERT INTO Books (Title, PublishYear) VALUES ('Test', 2010)", conn, transaction))
                            {

                                cmd.ExecuteNonQuery();//this should work
                            }

                            using (SqlCommand cmd = new SqlCommand("INSERT INTO Books (Title, PublishYear) VALUES ('Test', 'Oops')", conn, transaction))
                            {
                                cmd.ExecuteNonQuery();//this should NOT work
                            }
                            
                            transaction.Commit();
                        }
                        catch (SqlException )
                        {
                            Console.WriteLine("Exception occured, rolling back");
                            transaction.Rollback();
                        }

                    }
                    Console.WriteLine("After attempted inserts");
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Books", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
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
