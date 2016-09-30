// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using EmployeeTracker.Common;
    using EmployeeTracker.Model;
    using EmployeeTracker.ViewModel.Helpers;

    /// <summary>
    /// Модель ViewModel для управления сотрудниками в компании
    /// </summary>
    public class EmployeeWorkspaceViewModel : ViewModelBase
    {
        /// <summary>
        /// Сотрудник, выделенный в настоящее время в рабочей области
        /// </summary>
        private EmployeeViewModel currentEmployee;

        /// <summary>
        /// UnitOfWork для управления изменениями
        /// </summary>
        private IUnitOfWork unitOfWork;

        /// <summary>
        /// Отделы, используемые для просмотра
        /// </summary>
        private ObservableCollection<DepartmentViewModel> departmentLookup;

        /// <summary>
        /// Инициализирует новый экземпляр класса EmployeeWorkspaceViewModel.
        /// </summary>
        /// <param name="employees">Управляемые сотрудники</param>
        /// <param name="departmentLookup">Отделы, используемые для просмотра</param>
        /// <param name="unitOfWork">UnitOfWork для управления изменениями</param>
        public EmployeeWorkspaceViewModel(ObservableCollection<EmployeeViewModel> employees, ObservableCollection<DepartmentViewModel> departmentLookup, IUnitOfWork unitOfWork)
        {
            if (employees == null)
            {
                throw new ArgumentNullException("employees");
            }

            if (departmentLookup == null)
            {
                throw new ArgumentNullException("departmentLookup");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            this.unitOfWork = unitOfWork;
            this.AllEmployees = employees;
            this.departmentLookup = departmentLookup;
            this.CurrentEmployee = employees.Count > 0 ? employees[0] : null;

            // Реагирование на изменения вне этой модели ViewModel
            this.AllEmployees.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.CurrentEmployee))
                {
                    this.CurrentEmployee = null;
                }
            };

            this.AddEmployeeCommand = new DelegateCommand((o) => this.AddEmployee());
            this.DeleteEmployeeCommand = new DelegateCommand((o) => this.DeleteCurrentEmployee(), (o) => this.CurrentEmployee != null);
        }

        /// <summary>
        /// Возвращает команду для добавления нового сотрудника
        /// </summary>
        public ICommand AddEmployeeCommand { get; private set; }

        /// <summary>
        /// Возвращает команду для удаления текущего сотрудника
        /// </summary>
        public ICommand DeleteEmployeeCommand { get; private set; }

        /// <summary>
        /// Возвращает всех сотрудников компании
        /// </summary>
        public ObservableCollection<EmployeeViewModel> AllEmployees { get; private set; }

        /// <summary>
        /// Возвращает или задает сотрудника, выделенного в настоящее время в рабочей области
        /// </summary>
        public EmployeeViewModel CurrentEmployee
        {
            get
            {
                return this.currentEmployee;
            }

            set
            {
                this.currentEmployee = value;
                this.OnPropertyChanged("CurrentEmployee");
            }
        }

        /// <summary>
        /// Обрабатывает добавление нового сотрудника в рабочую область и модель
        /// </summary>
        private void AddEmployee()
        {
            Employee emp = this.unitOfWork.CreateObject<Employee>();
            this.unitOfWork.AddEmployee(emp);

            EmployeeViewModel vm = new EmployeeViewModel(emp, this.AllEmployees, this.departmentLookup, this.unitOfWork);
            this.AllEmployees.Add(vm);
            this.CurrentEmployee = vm;
        }

        /// <summary>
        /// Обрабатывает удаление текущего сотрудника
        /// </summary>
        private void DeleteCurrentEmployee()
        {
            this.unitOfWork.RemoveEmployee(this.CurrentEmployee.Model);
            this.AllEmployees.Remove(this.CurrentEmployee);
            this.CurrentEmployee = null;
        }
    }
}
