// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Office = Microsoft.Office.Core;

namespace DataContentControls
{
    partial class ActionsPaneControl1 : UserControl
    {
        public ActionsPaneControl1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Globals.ThisDocument.employeesBindingSource.MovePrevious();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Globals.ThisDocument.employeesBindingSource.MoveNext();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            int currentIndex = Globals.ThisDocument.employeesBindingSource.Position;
            int foundIndex = Globals.ThisDocument.employeesBindingSource.Find("EmployeeID", TextBox1.Text);
            if (foundIndex < 0)
            {
                // Предложите пользователю запрос допустимого идентификатора, поскольку введенное значение не удалось найти                
                MessageBox.Show("Please enter a valid ID!");
                Globals.ThisDocument.employeesBindingSource.Position = currentIndex;
            }
            else
                //Перейдите к найденной записи
                Globals.ThisDocument.employeesBindingSource.Position = foundIndex;
    
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Globals.ThisDocument.plainTextContentControl4.DataBindings["Text"].WriteValue();
            Globals.ThisDocument.employeesBindingSource.EndEdit();
            Globals.ThisDocument.employeesTableAdapter.Update(Globals.ThisDocument.northwindDataSet.Employees);
            MessageBox.Show("Title Updated");
        }
    }
}
