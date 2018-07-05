using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace BooksApp
{
    public partial class BookDetailForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string idStr = Request.QueryString["id"];
            int id = 1;
            //parse the id to make sure it's a valid int
            if (int.TryParse(idStr, out id))
            {
                DataSet set = BookDataSource.DataSet;
                DataRow[] rows = set.Tables["Books"].Select("ID=" + id.ToString());
                if (rows.Length == 1)
                {
                    bookEntry.BookID = id;
                    bookEntry.Title = rows[0]["Title"] as string;
                    bookEntry.Author = rows[0]["Author"] as string;
                    bookEntry.PublishYear = (int)rows[0]["PublishYear"];
                }
            }
            
        }
    }
}
