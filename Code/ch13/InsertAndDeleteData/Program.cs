using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace InsertData
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
                    //insert
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Books (Title, PublishYear) VALUES (@Title, @PublishYear)", conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@Title", "Test Book"));
                        cmd.Parameters.Add(new SqlParameter("@PublishYear", "2010"));

                        int rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine("{0} rows affected by insert", rowsAffected);
                    }
                    //display
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Books", conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0}\t{1}\t{2}", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                        }
                    }
                    //delete
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Books WHERE Title LIKE '%Test%'", conn))
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        Console.WriteLine("{0} rows affect by delete", rowsAffected);
                    }
                    //display
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
        private static string GetConnectionString()
        {
            return @"Data source=BEN-DESKTOP\SQLEXPRESS;Initial Catalog=TestDB;Integrated Security=SSPI";
        }
    }
}
