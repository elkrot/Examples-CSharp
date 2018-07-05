using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace UpdateDatabaseFromDataSet
{
    public partial class Form1 : Form
    {
        IDataAdapter _adapter;
        DataSet _dataSet;
        IDbConnection _conn;

        public Form1()
        {
            InitializeComponent();

            buttonUpdate.Enabled = false;

            CreateDataSet();

            dataGridView1.DataSource = _dataSet.Tables["Books"];
            dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(OnCellEdit);

            buttonUpdate.Click += new EventHandler(buttonUpdate_Click);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            _dataSet.Dispose();
            _conn.Dispose();
        }

        private void CreateDataSet()
        {
            string connectionString = @"Data source=BEN-DESKTOP\SQLEXPRESS;Initial Catalog=TestDB;Integrated Security=SSPI";
            SqlConnection conn = new SqlConnection(connectionString);
                        
            SqlCommand selectCmd = new SqlCommand("SELECT * FROM Books", conn);
            SqlCommand updatecmd = new SqlCommand("UPDATE BOOKS SET Title=@Title, PublishYear=@PublishYear WHERE BookID=@BookID", conn);
            //must add parameters so the DataSet can supply the right values for the columns
            updatecmd.Parameters.Add("@BookID", SqlDbType.Int, 4, "BookID");
            updatecmd.Parameters.Add("@Title", SqlDbType.VarChar, 255,"Title");
            updatecmd.Parameters.Add("@PublishYear", SqlDbType.Int, 4, "PublishYear");
            
            SqlDataAdapter adapter = new SqlDataAdapter();
        
            adapter.TableMappings.Add("Table", "Books");
            adapter.SelectCommand = selectCmd;
            adapter.UpdateCommand = updatecmd;
            _conn = conn;
            _adapter = adapter;
            _dataSet = new DataSet("Books");
            
            //put all the rows into the dataset
            _adapter.Fill(_dataSet);
        }

        private void OnCellEdit(object sender, DataGridViewCellEventArgs e)
        {
            buttonUpdate.Enabled = true;
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            _adapter.Update(_dataSet);
            buttonUpdate.Enabled = false;
        }
    }
}
