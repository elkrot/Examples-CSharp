// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LinqToSqlQueryVisualizer {
    public partial class QueryResult2 : Form {
        public QueryResult2() {
            InitializeComponent();
        }

        public void SetDataSets(DataSet ds1, DataSet ds2) {
            this.dataGridView1.DataSource = ds1.Tables[0];
            this.dataGridView2.DataSource = ds2.Tables[0];
        }
    }
}