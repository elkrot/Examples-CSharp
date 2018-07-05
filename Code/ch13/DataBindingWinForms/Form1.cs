using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DataBindingWinForms
{
    public partial class Form1 : Form
    {
        DataSet _dataSet;

        public Form1()
        {
            InitializeComponent();

            _dataSet = CreateDataSet();
            dataGridView.DataSource = _dataSet.Tables["Books"];
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _dataSet.Dispose();
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
