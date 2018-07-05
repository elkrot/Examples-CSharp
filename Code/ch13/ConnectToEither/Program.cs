using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace ConnectToEither
{
    class Program
    {
        enum DatabaseServerType { SqlServer, MySQL };

        static void PrintUsage()
        {
            Console.WriteLine("ConnectToEither.exe [SqlServer | MySql]");
        }

        static void Main(string[] args)
        {
            DatabaseServerType dbType;

            if (args.Length < 1)
            {
                PrintUsage();
                return;
            }
            if (string.Compare("SqlServer", args[0]) == 0)
            {
                dbType = DatabaseServerType.SqlServer;
            }
            else if (string.Compare("MySQL", args[0]) == 0)
            {
                dbType = DatabaseServerType.MySQL;
            }
            else
            {
                PrintUsage();
                return;
            }
            try
            {
                string connectionString = GetConnectionString(dbType);
                using (DbConnection conn = CreateConnection(dbType, connectionString))
                {
                    conn.Open();
                    using (DbCommand cmd = CreateCommand(dbType, "SELECT * FROM Books", conn))
                    using (IDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine("{0}\t{1}\t{2}", reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                        }
                    }
                }
            }
            catch (DbException ex)
            {
                Console.Write(ex);
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
        
        static string GetConnectionString(DatabaseServerType dbType)
        {
            switch (dbType)
            {
                case DatabaseServerType.SqlServer:
                    return @"Data source=BEN-DESKTOP\SQLEXPRESS;Initial Catalog=TestDB;Integrated Security=SSPI";
                case DatabaseServerType.MySQL:
                    return @"Data source=localhost;Initial Catalog=TestDB;user=ben;password=password;";
            }
            throw new InvalidOperationException();
        }

        private static DbConnection CreateConnection(DatabaseServerType dbType, string connectionString)
        {
            switch (dbType)
            {
                case DatabaseServerType.SqlServer:
                    return new System.Data.SqlClient.SqlConnection(connectionString);
                case DatabaseServerType.MySQL:
                    return new MySql.Data.MySqlClient.MySqlConnection(connectionString);
            }
            throw new InvalidOperationException();
        }

        private static string CreateSelectString(DatabaseServerType dbType)
        {
            //be aware that different servers have slightly different SQL syntax for many things
            //for a simple select like we're doing, the syntax is the same
            return "SELECT * FROM Books";
        }

        private static DbCommand CreateCommand(DatabaseServerType dbType, string query, DbConnection conn)
        {
            switch (dbType)
            {
                case DatabaseServerType.SqlServer:
                    return new System.Data.SqlClient.SqlCommand(query, conn as System.Data.SqlClient.SqlConnection);
                case DatabaseServerType.MySQL:
                    return new MySql.Data.MySqlClient.MySqlCommand(query, conn as MySql.Data.MySqlClient.MySqlConnection);
            }
            throw new InvalidOperationException();
        }
    }
}
