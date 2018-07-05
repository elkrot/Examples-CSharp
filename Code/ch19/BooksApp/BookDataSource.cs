using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace BooksApp
{
    public class BookDataSource
    {
        private static DataSet _dataSet;
        public static DataSet DataSet
        {
            get
            {
                return _dataSet;
            }
        }

        static BookDataSource()
        {
            //this would normally come from a database
            //but to keep things simple, let's just simulate one

            _dataSet = new DataSet();
            DataTable table = new DataTable("Books");
            _dataSet.Tables.Add(table);
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Title", typeof(string));
            table.Columns.Add("Author", typeof(string));
            table.Columns.Add("PublishYear", typeof(int));
            table.PrimaryKey = new DataColumn[] { table.Columns["ID"] };

            table.Rows.Add(1, "Les Miserables", "Victor Hugo", 1862);
            table.Rows.Add(2, "Les Chansons des rues et des bois","Victor Hugo", 1865); 
            table.Rows.Add(3, "Les Travailleurs de la Mer", "Victor Hugo", 1866);
            table.Rows.Add(4, "La voix de Guernsey", "Victor Hugo", 1867);            
        }
    }
}
