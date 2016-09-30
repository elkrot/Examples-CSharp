// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.Model
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Обозначает отдел внутри компании
    /// </summary>
    public class Department
    {
        /// <summary>
        /// Сотрудники, работающие в этом отделе
        /// </summary>
        private ICollection<Employee> employees;

        /// <summary>
        /// Инициализирует новый экземпляр класса Department.
        /// </summary>
        public Department()
        {
            // Перешлите коллекцию сотрудников в ссылки синхронизации
            // ПРИМЕЧАНИЕ. При выполнении на основе Entity Framework с прокси отслеживания изменений эта логика будет пропущена,
            //       так как свойство Employees будет переопределено и заменено на EntityCollection<Employee>.
            //       EntityCollection вместо этого выполнит эту привязку.
            ObservableCollection<Employee> emps = new ObservableCollection<Employee>();
            this.employees = emps;
            emps.CollectionChanged += (sender, e) =>
            {
                // Задайте ссылку на всех сотрудников, добавляемых к этому отделу
                if (e.NewItems != null)
                {
                    foreach (Employee item in e.NewItems)
                    {
                        if (item.Department != this)
                        {
                            item.Department = this;
                        }
                    }
                }

                // Удалите ссылку на всех удаляемых сотрудниках, все еще указывающую на этот отдел
                if (e.OldItems != null)
                {
                    foreach (Employee item in e.OldItems)
                    {
                        if (item.Department == this)
                        {
                            item.Department = null;
                        }
                    }
                }
            };
        }

        /// <summary>
        /// Получает или задает идентификатор данного отдела
        /// </summary>
        public virtual int DepartmentId { get; set; }

        /// <summary>
        /// Получает или задает имя данного отдела
        /// </summary>
        public virtual string DepartmentName { get; set; }

        /// <summary>
        /// Получает или задает код данного отдела
        /// </summary>
        public virtual string DepartmentCode { get; set; }

        /// <summary>
        /// Получает или задает дату последнего аудита данного отдела
        /// </summary>
        public virtual DateTime? LastAudited { get; set; }

        /// <summary>
        /// Получает или задает сотрудников, работающих в этом отделе
        /// Добавление или удаление выполнит привязку свойства department к затронутому сотруднику
        /// </summary>
        public virtual ICollection<Employee> Employees
        {
            get { return this.employees; }
            set { this.employees = value; }
        }
    }
}
