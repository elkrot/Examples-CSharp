// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.ViewModel
{
    using System;
    using EmployeeTracker.Model;
    using EmployeeTracker.ViewModel.Helpers;

    /// <summary>
    /// ViewModel конкретного отдела
    /// </summary>
    public class DepartmentViewModel : ViewModelBase
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса DepartmentViewModel.
        /// </summary>
        /// <param name="department">Базовый отдел, на котором должна быть основана эта модель ViewModel</param>
        public DepartmentViewModel(Department department)
        {
            if (department == null)
            {
                throw new ArgumentNullException("department");
            }

            this.Model = department;
        }

        /// <summary>
        /// Возвращает базовый отдел, на котором основана эта модель ViewModel
        /// </summary>
        public Department Model { get; private set; }

        /// <summary>
        /// Возвращает или задает имя данного отдела
        /// </summary>
        public string DepartmentName
        {
            get
            {
                return this.Model.DepartmentName;
            }

            set
            {
                this.Model.DepartmentName = value;
                this.OnPropertyChanged("DepartmentName");
            }
        }

        /// <summary>
        /// Возвращает или задает код данного отдела
        /// </summary>
        public string DepartmentCode
        {
            get
            {
                return this.Model.DepartmentCode;
            }

            set
            {
                this.Model.DepartmentCode = value;
                this.OnPropertyChanged("DepartmentCode");
            }
        }

        /// <summary>
        /// Возвращает или задает дату последнего аудита данного отдела
        /// </summary>
        public DateTime? LastAudited
        {
            get
            {
                return this.Model.LastAudited;
            }

            set
            {
                this.Model.LastAudited = value;
                this.OnPropertyChanged("LastAudited");
            }
        }
    }
}
