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
    /// Обозначает лицо, нанятое компанией
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Контактные данные, относящиеся к этому сотруднику
        /// </summary>
        private ICollection<ContactDetail> details;

        /// <summary>
        /// Сотрудники, подчиненные данному сотруднику
        /// </summary>
        private ICollection<Employee> reports;

        /// <summary>
        /// Отдел, в котором работает данный сотрудник
        /// </summary>
        private Department department;

        /// <summary>
        /// Руководитель данного сотрудника
        /// </summary>
        private Employee manager;

        /// <summary>
        /// Инициализирует новый экземпляр класса Employee.
        /// </summary>
        public Employee()
        {
            // ПРИМЕЧАНИЕ. Привязка не требуется, так как это однонаправленный переход
            this.details = new ObservableCollection<ContactDetail>();

            // Привяжите коллекцию отчетов к ссылкам синхронизации
            // ПРИМЕЧАНИЕ. При выполнении на основе Entity Framework с прокси отслеживания изменений эта логика будет пропущена,
            //       так как свойство Reports будет переопределено и заменено на EntityCollection<Employee>.
            //       EntityCollection вместо этого выполнит эту привязку.
            ObservableCollection<Employee> reps = new ObservableCollection<Employee>();
            this.reports = reps;
            reps.CollectionChanged += (sender, e) =>
            {
                // Задайте ссылку на всех сотрудников, добавляемых для этого руководителя
                if (e.NewItems != null)
                {
                    foreach (Employee item in e.NewItems)
                    {
                        if (item.Manager != this)
                        {
                            item.Manager = this;
                        }
                    }
                }

                // Удалите ссылку на всех удаляемых сотрудниках, все еще указывающую на этого руководителя
                if (e.OldItems != null)
                {
                    foreach (Employee item in e.OldItems)
                    {
                        if (item.Manager == this)
                        {
                            item.Manager = null;
                        }
                    }
                }
            };
        }

        /// <summary>
        /// Получает или задает идентификатор данного сотрудника
        /// </summary>
        public virtual int EmployeeId { get; set; }

        /// <summary>
        /// Получает или задает обращение данного сотрудника
        /// </summary>
        public virtual string Title { get; set; }

        /// <summary>
        /// Получает или задает имя данного сотрудника
        /// </summary>
        public virtual string FirstName { get; set; }

        /// <summary>
        /// Получает или задает фамилию данного сотрудника
        /// </summary>
        public virtual string LastName { get; set; }

        /// <summary>
        /// Получает или задает должность данного сотрудника
        /// </summary>
        public virtual string Position { get; set; }

        /// <summary>
        /// Получает или задает дату приема на работу данного сотрудника
        /// </summary>
        public virtual DateTime HireDate { get; set; }

        /// <summary>
        /// Получает или задает дату увольнения данного сотрудника
        /// Возвращает неопределенное значение, если сотрудник еще работает
        /// </summary>
        public virtual DateTime? TerminationDate { get; set; }

        /// <summary>
        /// Получает или задает дату рождения данного сотрудника
        /// </summary>
        public virtual DateTime BirthDate { get; set; }

        /// <summary>
        /// Получает или задает идентификатор отдела, в котором работает данный сотрудник
        /// </summary>
        public virtual int? DepartmentId { get; set; }

        /// <summary>
        /// Получает или задает идентификатор руководителя данного сотрудника
        /// </summary>
        public virtual int? ManagerId { get; set; }

        /// <summary>
        /// Получает или задает контактные данные этого сотрудника
        /// Привязка не выполняется, так как это однонаправленный переход
        /// </summary>
        public virtual ICollection<ContactDetail> ContactDetails
        {
            get { return this.details; }
            set { this.details = value; }
        }

        /// <summary>
        /// Получает или задает сотрудников, подчиненных данному сотруднику
        /// Добавление или удаление выполнит привязку свойства manager к затронутому сотруднику
        /// </summary>
        public virtual ICollection<Employee> Reports
        {
            get { return this.reports; }
            set { this.reports = value; }
        }

        /// <summary>
        /// Получает или задает отдел, в котором работает данный сотрудник
        /// Задание данного свойства выполнит привязку коллекции к исходному и новому отделу
        /// </summary>
        public virtual Department Department
        {
            get
            {
                return this.department;
            }

            set
            {
                if (value != this.department)
                {
                    Department original = this.department;
                    this.department = value;

                    // Удалить из старой коллекции
                    if (original != null && original.Employees.Contains(this))
                    {
                        original.Employees.Remove(this);
                    }

                    // Добавить в новое подключение
                    if (value != null && !value.Employees.Contains(this))
                    {
                        value.Employees.Add(this);
                    }
                }
            }
        }

        /// <summary>
        /// Получает или задает руководителя данного сотрудника
        /// Задание данного свойства выполнит привязку коллекции к исходному и новому отделу
        /// </summary>
        public virtual Employee Manager
        {
            get
            {
                return this.manager;
            }

            set
            {
                if (value != this.manager)
                {
                    Employee original = this.manager;
                    this.manager = value;

                    // Удалить из старой коллекции
                    if (original != null && original.Reports.Contains(this))
                    {
                        original.Reports.Remove(this);
                    }

                    // Добавить в новое подключение
                    if (value != null && !value.Reports.Contains(this))
                    {
                        value.Reports.Add(this);
                    }
                }
            }
        }
    }
}
