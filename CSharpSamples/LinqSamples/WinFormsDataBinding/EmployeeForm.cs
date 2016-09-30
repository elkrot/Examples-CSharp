// © Корпорация Майкрософт (Microsoft Corp.).  Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//(C) Корпорация Майкрософт (Microsoft Corporation). Все права защищены.

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using System.Data.Linq;
using NorthwindMapping;

namespace WinFormsDataBinding {
    public partial class EmployeeForm : Form {
        private Northwind db;

        public EmployeeForm() {
            InitializeComponent();
            db = new Northwind(Program.connString);
            var employeeQuery = from employee in db.Employees 
                                orderby employee.LastName
                                select employee;
            var managerQuery =  from manager in db.Employees 
                                orderby manager.LastName
                                select manager;
            //Метод ToBindingList преобразует запрос в структуру, поддерживающую IBindingList.
            //Для преобразования списка привязок требуется Table<T>, чтобы добавление и
            //удаление сущностей было отслежено правильно.
            employeeBindingSource.DataSource = employeeQuery;
            managerBindingSource.DataSource = managerQuery.ToList();
       }

        private void submitChanges_Click(object sender, EventArgs e) {
            //Приводит к тому, что контейнер элемента управления заканчивает редактирование и начинает выполнять проверку.
            this.Validate();
            db.SubmitChanges();
        }

        private void employeeDataGridView_CellParsing(object sender, DataGridViewCellParsingEventArgs e) {
            string s = e.Value as string;

             //Требуется OfType, так как employeeBindingSource возвращает экземпляры объекта типа.
             Employee emp = (from employee in this.managerBindingSource.OfType<Employee>()
                            where employee.ToString()==s
                            select employee).FirstOrDefault();
            
            e.Value = emp;
            e.ParsingApplied = true;
        }

        private void launchEmployeeManager_Click(object sender, EventArgs e) {
            WinFormsDataBinding.EmployeeManagerGrids form = new EmployeeManagerGrids();
            form.Visible = true;
        }
    }
}