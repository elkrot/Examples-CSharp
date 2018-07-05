using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace ConnectToMySQL
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string connectionString = GetConnectionString();
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM Books", conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0}\t{1}\t{2}", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.Write(ex);
            }
            Console.ReadKey();            
        }
        //you shouldn't store connection strings in source code, but this is
        //to illustrate SQL access, so I'm keeping it simple
        static string GetConnectionString()
        {
            return @"Data source=localhost;Initial Catalog=TestDB;user=ben;password=password;";
        }
    }
}
