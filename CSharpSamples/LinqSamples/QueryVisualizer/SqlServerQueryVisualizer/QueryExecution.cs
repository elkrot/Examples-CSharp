// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Data.Linq.Provider;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;


namespace LinqToSqlQueryVisualizer {


    /// <summary>
    /// Прокси-сервер визуализатора для общих запросов
    /// Он обеспечивает сериализацию запросов и передачу их в поток
    /// Делегирует эту задачу в особую вспомогательную функцию визуализатора запросов соответствующего поставщика
    /// </summary>
    public class SourceChooser : VisualizerObjectSource {
        public override object CreateReplacementObject(object target, Stream incomingData) {
            return base.CreateReplacementObject(target, incomingData);
        }
        public override void TransferData(object target, Stream incomingData, Stream outgoingData) {
            base.TransferData(target, incomingData, outgoingData);
        }

        /// <summary>
        /// Этот метод должен записывать в поток сведения об объекте для визуализации, которые 
        /// затем передаются в визуализатор.
        /// Эта реализация извлекает из запроса сведения о поставщике и выполняет поиск атрибута QueryVisualizer.
        /// Атрибут содержит имя класса и сборки вспомогательной функции визуализатора специальных запросов для поставщика.
        /// Эти данные записываются в поток, а затем загружается и вызывается класс для выполнения текущей 
        /// сериализации запроса.
        /// </summary>
        /// <param name="target"> Объект для сериализации, должен быть запросом</param>
        /// <param name="outgoingData">Поток, получающий сведения о запросе</param>
        public override void GetData(object target, Stream outgoingData) {
            SerializeTheQuery(target, outgoingData);
        }

        private static void Error(Stream str, string message) {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(str, "None");
            formatter.Serialize(str, message);
            return;
        }

        /// <summary>
        /// Модульные тесты будут применять эту статическую версию
        /// </summary>
        /// <param name="target"></param>
        /// <param name="outgoingData"></param>
        public static void SerializeTheQuery(object target, Stream stream) {
            // получение запроса
            IQueryable query = target as IQueryable;
            if (query == null) {
                Error(stream, "Query visualizer invoked on non-IQueryable target.");
                return;
            }

            //получение сведений о поставщике
            Type tQueryImpl = query.GetType();
            FieldInfo fiContext = tQueryImpl.GetField("context", BindingFlags.NonPublic | BindingFlags.Instance);
            if (fiContext == null) {
                Error(stream, "Query field 'context' not found in type " + tQueryImpl.ToString() + ".");
                return;
            }

            Object objProvider = fiContext.GetValue(query);
            if (objProvider == null) {
                Error(stream, "Query field 'context' returned null.");
                return;
            }

            System.Data.Linq.DataContext dataContext = objProvider as System.Data.Linq.DataContext;
            if (dataContext == null) {
                Error(stream, "Query is not against a DataContext.");
                return;
            }

            //вызов визуализатора для сериализации сведений о запросе
            Visualizer.StreamQueryInfo(dataContext, query, stream);
        }
    }


    /// <summary>
    /// Класс, который определяет пользовательский интерфейс и поведение визуализатора
    /// Его реализация делегирует задачу визуализатору специальных запросов для поставщика
    /// </summary>
    public class DialogChooser : DialogDebuggerVisualizer {

        /// <summary>
        /// Формат сведений запроса и требуемый интерфейс пользователя обычно зависят от поставщика Linq to SQLq,
        /// определенного в запросе. 
        /// Поэтому в этом визуализаторе общих запросов выполняется только считывание данных сборки и класса для визуализатора
        /// специальных запросов из потока Stream и вызов метода "Display" этого класса, после чего
        /// считываются сведения о запросе, и отображается интерфейс пользователя.         
        /// </summary>
        /// <param name="windowService"> используется для отображения интерфейса пользователя </param>
        /// <param name="objectProvider"> используется для извлечения данных (в виде потока Stream) из прокси-сервера визуализатора</param>
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider) {
            Stream rawStream = objectProvider.GetData();

            Visualizer.Display(windowService, rawStream);
        }
        public static void TestShow(object elementToVisualize) {
            VisualizerDevelopmentHost visualizerHost = new VisualizerDevelopmentHost(elementToVisualize, typeof(DialogChooser), typeof(SourceChooser));
            visualizerHost.ShowVisualizer();
        }

    }

    internal static class Utils {

        internal static string QuoteString(string raw) {
            return "'" + raw.Replace("'", "''") + "'";
        }

        /// <summary>
        /// Преобразование текста SQL-запроса (в котором содержатся имена параметров)
        /// и сведений о параметрах (SqlType и значения, возвращенные методом .ToString())
        /// в одну строку SQL (в которой содержатся текстовые представления значений).
        /// Выполнение этой строки не должно отличаться от выполнения исходного запроса, за исключением 
        /// предельных вариантов (например, строка содержит имя параметра или для десятичного числа указана слишком большая точность)
        /// </summary>
        /// <param name="qt">текст запроса и сведения о параметрах</param>
        /// <returns>строка SQL для выполнения</returns>
        internal static string GetQueryTextWithParams(SqlQueryText qt) {
            string s = qt.Text;
            for (int i=qt.Params.Length-1; i >= 0; i--){
                ParameterText param = qt.Params[i];
                string val;
                switch (param.SqlType.ToString()) {
                    case "String":
                    case "Guid":
                    case "DateTime":
                        val = QuoteString(param.Value);
                        break;
                    case "Boolean":
                        if (param.Value == "True") {
                            val = "1";
                        } else if (param.Value == "False") {
                            val = "0";
                        } else {
                            throw new ArgumentException("Boolean value other than True or False");
                        }
                        break;
                    case "Time":
                        TimeSpan ts = TimeSpan.Parse(param.Value);
                        val = ts.Ticks.ToString(CultureInfo.CurrentUICulture);
                        break;
                    default:
                        val = param.Value;
                        break;
                }
                s = s.Replace(param.Name, val);
            }
            return s;
        }
    }

    internal static class QueryExecution {

        // реконструкция объекта из его Clr-типа и строки значения
        // (которое получается в результате вызова метода ToString)
        private static object GetObject(string val, string sqlType) {

            DbType nnType = (DbType)Enum.Parse(typeof(DbType), sqlType.Trim());
            if (nnType == DbType.String) {
                return val;
            } else if (nnType == DbType.Int16) {
                return System.Int16.Parse(val, CultureInfo.CurrentUICulture);
            } else if (nnType == DbType.Int32) {
                return System.Int32.Parse(val, CultureInfo.CurrentUICulture);
            } else if (nnType == DbType.Int64) {
                return System.Int64.Parse(val, CultureInfo.CurrentUICulture);
            } else if (nnType == DbType.Byte) {
                return System.Byte.Parse(val, CultureInfo.CurrentUICulture);
            } else if (nnType == DbType.Double) {
                return System.Double.Parse(val, CultureInfo.CurrentUICulture);
            } else if (nnType == DbType.Single) {
                return System.Single.Parse(val, CultureInfo.CurrentUICulture);
            } else if (nnType == DbType.Decimal) {
                return System.Decimal.Parse(val, CultureInfo.CurrentUICulture);
            } else if (nnType == DbType.Boolean) {
                return System.Boolean.Parse(val);
            } else if (nnType == DbType.DateTime) {
                return System.DateTime.Parse(val, CultureInfo.CurrentUICulture);
            } else if (nnType == DbType.Time) {
                return System.TimeSpan.Parse(val);
            } else if (nnType == DbType.Guid) {
                return new Guid(val);
            } else {
                throw new NotSupportedException("Type " + sqlType + " is not supported for parameters in Linq to Sql Query Visualizer");
            }
        }



        /// <summary>
        /// Формирование SqlCommand путем создания параметров из строк в qt.Params
        /// и текста в qt.Text
        /// </summary>
        /// <param name="qt">Входные данные SqlQueryText</param>
        /// <param name="conn">Подключение SqlConnection, связываемое с SqlCommand</param>
        /// <returns>SqlCommand</returns>
        private static SqlCommand GetSqlCommand(SqlQueryText qt, SqlConnection conn) {
            SqlCommand cmd = new SqlCommand(qt.Text, conn);
            foreach (ParameterText param in qt.Params) {
                System.Data.SqlClient.SqlParameter sqlParam = cmd.CreateParameter();
                sqlParam.ParameterName = param.Name;
                object val = GetObject(param.Value, param.SqlType);
                sqlParam.Value = val; 
                cmd.Parameters.Add(sqlParam);
            }
            return cmd;
        }

        // Выполнение запросов с использованием исходных сведений о запросе
        // Этот метод формирует запрос и параметры как в Linq to SQL
        // (не используя значения параметров как часть текста Sql)
        internal static void ExecuteOriginalQueries(DataSet ds1, DataSet ds2, SqlQueryText[] infos, string connectionString) {
            SqlConnection conn = new SqlConnection(connectionString);

            // извлечение данных
            using (conn) {
                conn.Open();

                SqlCommand cmd1 = GetSqlCommand(infos[0], conn);
                SqlDataAdapter adapter1 = new SqlDataAdapter(cmd1);
                adapter1.Fill(ds1);

                if (infos.Length > 1) {
                    SqlCommand cmd2 = GetSqlCommand(infos[1], conn);
                    SqlDataAdapter adapter2 = new SqlDataAdapter(cmd2);
                    adapter2.Fill(ds2);
                }
            }
        }

        // Выполнение Sql-команды как текста
        internal static void ExecuteQuery(DataSet ds1, string cmd1, string connectionString) {
            SqlConnection conn = new SqlConnection(connectionString);
            using (conn) {
                conn.Open();
                SqlCommand sqlCmd1 = new SqlCommand(cmd1, conn);
                SqlDataAdapter adapter1 = new SqlDataAdapter(sqlCmd1);
                adapter1.Fill(ds1);
            }
        }

        // Выполнение Sql-команд как текста
        internal static void ExecuteQueries(DataSet ds1, DataSet ds2, string cmd1, string cmd2, string connectionString) {
            SqlConnection conn = new SqlConnection(connectionString);
            using (conn) {
                conn.Open();
                SqlCommand sqlCmd1 = new SqlCommand(cmd1, conn);
                SqlDataAdapter adapter1 = new SqlDataAdapter(sqlCmd1);
                adapter1.Fill(ds1);

                SqlCommand sqlCmd2 = new SqlCommand(cmd2, conn);
                SqlDataAdapter adapter2 = new SqlDataAdapter(sqlCmd2);
                adapter2.Fill(ds2);
            }
        }
    }
}
