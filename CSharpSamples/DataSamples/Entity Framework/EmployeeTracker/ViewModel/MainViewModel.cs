// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using EmployeeTracker.Common;
    using EmployeeTracker.Model.Interfaces;
    using EmployeeTracker.ViewModel.Helpers;

    /// <summary>
    /// Модель ViewModel для доступа ко всем данным для компании
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// UnitOfWork для координации изменений
        /// </summary>
        private IUnitOfWork unitOfWork;

        /// <summary>
        /// Инициализирует новый экземпляр класса MainViewModel.
        /// </summary>
        /// <param name="unitOfWork">UnitOfWork для координации изменений</param>
        /// <param name="departmentRepository">Репозиторий для запросов данных об отделе</param>
        /// <param name="employeeRepository">Репозиторий для запросов данных о сотруднике</param>
        public MainViewModel(IUnitOfWork unitOfWork, IDepartmentRepository departmentRepository, IEmployeeRepository employeeRepository)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            if (departmentRepository == null)
            {
                throw new ArgumentNullException("departmentRepository");
            }

            if (employeeRepository == null)
            {
                throw new ArgumentNullException("employeeRepository");
            }

            this.unitOfWork = unitOfWork;

            // Построение структур данных для заполнения зон поверхности приложений
            ObservableCollection<EmployeeViewModel> allEmployees = new ObservableCollection<EmployeeViewModel>();
            ObservableCollection<DepartmentViewModel> allDepartments = new ObservableCollection<DepartmentViewModel>();

            foreach (var dep in departmentRepository.GetAllDepartments())
            {
                allDepartments.Add(new DepartmentViewModel(dep));
            }

            foreach (var emp in employeeRepository.GetAllEmployees())
            {
                allEmployees.Add(new EmployeeViewModel(emp, allEmployees, allDepartments, this.unitOfWork));
            }

            this.DepartmentWorkspace = new DepartmentWorkspaceViewModel(allDepartments, unitOfWork);
            this.EmployeeWorkspace = new EmployeeWorkspaceViewModel(allEmployees, allDepartments, unitOfWork);

            // Построение не интерактивного списка сотрудников с большим стажем
            List<BasicEmployeeViewModel> longServingEmployees = new List<BasicEmployeeViewModel>();
            foreach (var emp in employeeRepository.GetLongestServingEmployees(5))
            {
                longServingEmployees.Add(new BasicEmployeeViewModel(emp));
            }

            this.LongServingEmployees = longServingEmployees;

            this.SaveCommand = new DelegateCommand((o) => this.Save());
        }

        /// <summary>
        /// Возвращает команду сохранения всех изменений, выполненных в UnitOfWork текущих сеансов
        /// </summary>
        public ICommand SaveCommand { get; private set; }

        /// <summary>
        /// Возвращает рабочую область для управления сотрудниками компании
        /// </summary>
        public EmployeeWorkspaceViewModel EmployeeWorkspace { get; private set; }

        /// <summary>
        /// Возвращает рабочую область для управления отделами компании
        /// </summary>
        public DepartmentWorkspaceViewModel DepartmentWorkspace { get; private set; }

        /// <summary>
        /// Возвращает список сотрудников для комитета лояльности
        /// </summary>
        public IEnumerable<BasicEmployeeViewModel> LongServingEmployees { get; private set; }

        /// <summary>
        /// Сохраняет все изменения, выполненные в UnitOfWork текущих сеансов
        /// </summary>
        private void Save()
        {
            this.unitOfWork.Save();
        }
    }
}
