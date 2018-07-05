using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace DataBindingWeb
{
    public partial class _Default : System.Web.UI.Page
    {
        private DataSet _dataSet;

        protected void Page_Load(object sender, EventArgs e)
        {
            _dataSet = CreateDataSet();
            GridView1.DataSource = _dataSet.Tables["Books"];
            GridView1.DataBind();
        }

        private DataSet CreateDataSet()
        {
            string connectionString = @"Data source=BEN-DESKTOP\SQLEXPRESS;Initial Catalog=TestDB;Integrated Security=SSPI";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Books", conn))
                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    adapter.TableMappings.Add("Table", "Books");
                    adapter.SelectCommand = cmd;
                    DataSet dataSet = new DataSet("Books");
                    //put all the rows into the dataset
                    adapter.Fill(dataSet);
                    return dataSet;
                }
            }
        }
    }
}
