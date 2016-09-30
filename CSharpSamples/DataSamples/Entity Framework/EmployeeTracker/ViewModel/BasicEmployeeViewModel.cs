// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.ViewModel
{
    using System;
    using System.Globalization;
    using EmployeeTracker.Model;
    using EmployeeTracker.ViewModel.Helpers;

    /// <summary>
    /// ViewModel отдельного сотрудника без связей
    /// Объект EmployeeViewModel должен использоваться, если должны отображаться или изменяться связи
    /// </summary>
    public class BasicEmployeeViewModel : ViewModelBase
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса BasicEmployeeViewModel.
        /// </summary>
        /// <param name="employee">Базовый сотрудник, на котором должна быть основана эта модель ViewModel</param>
        public BasicEmployeeViewModel(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException("employee");
            }

            this.Model = employee;
        }

        /// <summary>
        /// Возвращает базового сотрудника, на котором основана эта модель ViewModel
        /// </summary>
        public Employee Model { get; private set; }

        /// <summary>
        /// Возвращает или задает имя этого сотрудника
        /// </summary>
        public string FirstName
        {
            get
            {
                return this.Model.FirstName;
            }

            set
            {
                this.Model.FirstName = value;
                this.OnPropertyChanged("FirstName");
            }
        }

        /// <summary>
        /// Возвращает или задает должность этого сотрудника
        /// </summary>
        public string Title
        {
            get
            {
                return this.Model.Title;
            }

            set
            {
                this.Model.Title = value;
                this.OnPropertyChanged("Title");
            }
        }

        /// <summary>
        /// Возвращает или задает фамилию этого сотрудника
        /// </summary>
        public string LastName
        {
            get
            {
                return this.Model.LastName;
            }

            set
            {
                this.Model.LastName = value;
                this.OnPropertyChanged("LastName");
            }
        }

        /// <summary>
        /// Возвращает или задает штатную единицу, которую занимает сотрудник в компании
        /// </summary>
        public string Position
        {
            get
            {
                return this.Model.Position;
            }

            set
            {
                this.Model.Position = value;
                this.OnPropertyChanged("Position");
            }
        }

        /// <summary>
        /// Возвращает или задает дату рождения этого сотрудника
        /// </summary>
        public DateTime BirthDate
        {
            get
            {
                return this.Model.BirthDate;
            }

            set
            {
                this.Model.BirthDate = value;
                this.OnPropertyChanged("BirthDate");
            }
        }

        /// <summary>
        /// Возвращает или задает дату найма компанией этого сотрудника
        /// </summary>
        public DateTime HireDate
        {
            get
            {
                return this.Model.HireDate;
            }

            set
            {
                this.Model.HireDate = value;
                this.OnPropertyChanged("HireDate");
            }
        }

        /// <summary>
        /// Возвращает или задает дату увольнения данного сотрудника
        /// </summary>
        public DateTime? TerminationDate
        {
            get
            {
                return this.Model.TerminationDate;
            }

            set
            {
                this.Model.TerminationDate = value;
                this.OnPropertyChanged("TerminationDate");
            }
        }

        /// <summary>
        /// При ссылке на этого сотрудника возвращает отображаемый текст
        /// </summary>
        public string DisplayName
        {
            get { return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", this.Model.LastName, this.Model.FirstName); }
        }

        /// <summary>
        /// Возвращает отображаемый текст для доступной только для чтения версии даты найма этого сотрудника
        /// </summary>
        public string DisplayHireDate
        {
            get { return this.Model.HireDate.ToShortDateString(); }
        }
    }
}
