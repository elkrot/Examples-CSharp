// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

// OleDbSample.cs
// Чтобы выполнить построение этого примера из командной строки, используйте команду:
// csc oledbsample.cs

using System;
using System.Data;
using System.Data.OleDb;
using System.Xml.Serialization;

public class MainClass 
{
	public static void Main ()
	{
		// Установка подключения Access и выбора строк.
		// Путь к BugTypes.MDB необходимо изменить, если построение примера выполняется
		// из командной строки:
#if USINGPROJECTSYSTEM
		string strAccessConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\\..\\BugTypes.MDB";
#else
		string strAccessConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=BugTypes.MDB";
#endif
		string strAccessSelect = "SELECT * FROM Categories";

		// Создание набора данных и добавление в него таблицы Categories:
		DataSet myDataSet = new DataSet();
		OleDbConnection myAccessConn = null;
		try
		{
			myAccessConn = new OleDbConnection(strAccessConn);
		}
		catch(Exception ex)
		{
			Console.WriteLine("Error: Failed to create a database connection. \n{0}", ex.Message);
			return;
		}

		try
		{
		
			OleDbCommand myAccessCommand = new OleDbCommand(strAccessSelect,myAccessConn);
			OleDbDataAdapter myDataAdapter = new OleDbDataAdapter(myAccessCommand);

			myAccessConn.Open();
			myDataAdapter.Fill(myDataSet,"Categories");

		}
		catch (Exception ex)
		{
			Console.WriteLine("Error: Failed to retrieve the required data from the DataBase.\n{0}", ex.Message);
			return;
		}
		finally
		{
			myAccessConn.Close();
		}

		// Набор данных может содержать множество таблиц, поэтому рекомендуется поместить их
		// в массив:
		DataTableCollection dta = myDataSet.Tables;
		foreach (DataTable dt in dta)
		{
			Console.WriteLine("Found data table {0}", dt.TableName);
		}
	    
		// Следующие две строки иллюстрируют два разных способа получения
		// сведений о числе таблиц в наборе данных:
		Console.WriteLine("{0} tables in data set", myDataSet.Tables.Count);
		Console.WriteLine("{0} tables in data set", dta.Count);
		// Из следующих строк  видно, как получить сведения
		// о конкретной таблице набора данных, указав ее имя:
		Console.WriteLine("{0} rows in Categories table", myDataSet.Tables["Categories"].Rows.Count);
		// Сведения о столбце автоматически извлекаются из базы данных, поэтому
		// здесь они доступны для чтения:
		Console.WriteLine("{0} columns in Categories table", myDataSet.Tables["Categories"].Columns.Count);
		DataColumnCollection drc = myDataSet.Tables["Categories"].Columns;
		int i = 0;
		foreach (DataColumn dc in drc)
		{
			// Печать индекса столбца, его имени и
			// типа данных:
			Console.WriteLine("Column name[{0}] is {1}, of type {2}",i++ , dc.ColumnName, dc.DataType);
		}
		DataRowCollection dra = myDataSet.Tables["Categories"].Rows;
		foreach (DataRow dr in dra)
		{
			// Печать CategoryID как индекса, затем -- CategoryName:
			Console.WriteLine("CategoryName[{0}] is {1}", dr[0], dr[1]);
		}
      
	}
}

