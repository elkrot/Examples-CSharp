// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.Linq.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace LinqToSqlQueryVisualizer {
   
    public partial class QueryVisualizerForm : Form {

        private SqlQueryText[] queryInfos;
        private string connection;
        private bool uiInitialized;
        private bool twoQueries;
        private string sql1; // Sql-текст для запроса 1 (может быть изменен пользователем)
        private string sql2; // Sql-текст для запроса 2 (может быть изменен пользователем)

        // sql-текст для текущего выбранного запроса
        private string currentSql {
            get { 
                return this.radioQuery1.Checked ? sql1 : sql2; 
            }
            set {
                if (this.radioQuery1.Checked) {
                    sql1 = value;
                } else {
                    sql2 = value;
                }
            }
        }

        // sql-текст для другого запроса
        private string otherSql {
            get { 
                return this.radioQuery1.Checked ? sql2 : sql1; 
            }
            set {
                if (this.radioQuery1.Checked) {
                    sql2 = value;
                } else {
                    sql1 = value;
                }
            }
        }

        public QueryVisualizerForm() {
            InitializeComponent();
        }

        /// <summary>
        /// Настройка формы с использованием данных запроса
        /// </summary>
        /// <param name="expression">Запрос в виде текста выражения</param>
        /// <param name="infos">Sql-текст и описания параметров для запроса</param>
        /// <param name="connectionString">Строка подключения connectionString</param>
        internal void SetTexts(string expression, SqlQueryText[] infos, string connectionString) {
            this.txtExpression.Text = expression;
          
            this.queryInfos = infos;
            this.connection = connectionString;
            this.radioQuery1.Checked = true;
            if (this.queryInfos.Length > 1) {
                this.twoQueries = true;
                this.radioQuery1.Visible = true;
                this.radioQuery2.Visible = true;
            } else {
                this.twoQueries = false;
            }
            this.chkUseOriginal.Checked = false;
            this.btnQuery.Visible = true;
            this.InitSqlTexts();
            this.FillSqlText();
            this.uiInitialized = true;
        }


        /// <summary>
        /// заполнение sql1 и sql2 исходными sql-текстами
        /// </summary>
        private void InitSqlTexts() {
            SqlQueryText qt1 = this.queryInfos[0];
            this.sql1 = Utils.GetQueryTextWithParams(qt1);
            if (twoQueries) {
                SqlQueryText qt2 = this.queryInfos[1];
                this.sql2 = Utils.GetQueryTextWithParams(qt2);
            }
        }

        /// <summary>
        /// Заполнение поля txtSql пользовательского интерфейса
        /// Если "исходный запрос" проверен:
        ///   Заполнение поля txtSql.Text текстом соответствующего запроса и добавление параметров
        /// Иначе:
        ///   Заполнение текстового поля txtSql.Text данными sql
        /// </summary>
        private void FillSqlText() {
            if (this.chkUseOriginal.Checked) {
                SqlQueryText qt = this.queryInfos[0];
                if (this.radioQuery2.Checked) {
                    qt = this.queryInfos[1];
                }
                String s = qt.Text + "\r\n";
                s += "-------------------------------";
                for (int i = 0; i < qt.Params.Length; i++) {
                    ParameterText param = qt.Params[i];
                    if (param.SqlType == "String") {
                        s += "\r\n" + param.Name + " [" + param.SqlType + "]: " + Utils.QuoteString(param.Value);
                    } else {
                        s += "\r\n" + param.Name + " [" + param.SqlType + "]: " + param.Value;
                    }
                }
                this.txtSql.Text = s;
            } else {
                this.txtSql.Text = this.currentSql;
            }
        }

        private void btnQuery_Click(object sender, EventArgs e) {
            this.Cursor = Cursors.WaitCursor;
            
            // сохранение текущего запроса
            if (!this.chkUseOriginal.Checked) {
                this.currentSql = this.txtSql.Text;
            }

            // подготовка наборов данных для результатов запроса
            DataSet ds1 = new DataSet() { Locale = CultureInfo.CurrentUICulture };
            DataSet ds2 = new DataSet() { Locale = CultureInfo.CurrentUICulture };

            if (chkUseOriginal.Checked) {
                QueryExecution.ExecuteOriginalQueries(ds1, ds2, this.queryInfos, this.connection);
            } else if (this.twoQueries) {
                QueryExecution.ExecuteQueries(ds1, ds2, this.sql1, this.sql2, this.connection);
            } else {
                QueryExecution.ExecuteQuery(ds1, this.sql1, this.connection);
            }

            if (this.twoQueries) {
                QueryResult2 form = new QueryResult2();
                form.SetDataSets(ds1, ds2);
                this.Cursor = Cursors.Default;
                form.ShowDialog();
            } else {
                QueryResult form = new QueryResult();
                form.SetDataSet(ds1);
                this.Cursor = Cursors.Default;
                form.ShowDialog();
            }
        }

        private void chkIncludeParams_CheckedChanged(object sender, EventArgs e) {
            if (!uiInitialized) return;
            // сохранение изменений, если новый режим -- "использовать исходные"
            if (this.chkUseOriginal.Checked) {
                this.currentSql = this.txtSql.Text;
            }
            this.txtSql.ReadOnly = this.chkUseOriginal.Checked;
            this.FillSqlText();
        }

        private void radioQuery1_CheckedChanged(object sender, EventArgs e) {
            if (!uiInitialized) return;
            // сохранение текста запроса
            if (!this.chkUseOriginal.Checked) {
                this.otherSql = this.txtSql.Text;
            }
            this.FillSqlText();
        }

    }
}