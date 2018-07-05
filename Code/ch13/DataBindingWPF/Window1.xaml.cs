using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace DataBindingWPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        DataSet _dataSet;
        public Window1()
        {
            InitializeComponent();

            _dataSet = CreateDataSet();
            listView1.DataContext = _dataSet.Tables["Books"];
            Binding binding = new Binding();
            listView1.SetBinding(ListView.ItemsSourceProperty, binding);
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
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
