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
    /// Модель ViewModel для управления отделами в компании
    /// </summary>
    public class DepartmentWorkspaceViewModel : ViewModelBase
    {
        /// <summary>
        /// Отдел, выделенный в настоящее время в рабочей области
        /// </summary>
        private DepartmentViewModel currentDepartment;

        /// <summary>
        /// UnitOfWork для управления изменениями
        /// </summary>
        private IUnitOfWork unitOfWork;

        /// <summary>
        /// Инициализирует новый экземпляр класса DepartmentWorkspaceViewModel.
        /// </summary>
        /// <param name="departments">Отделы, которыми необходимо управлять</param>
        /// <param name="unitOfWork">UnitOfWork для управления изменениями</param>
        public DepartmentWorkspaceViewModel(ObservableCollection<DepartmentViewModel> departments, IUnitOfWork unitOfWork)
        {
            if (departments == null)
            {
                throw new ArgumentNullException("departments");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            this.unitOfWork = unitOfWork;
            this.AllDepartments = departments;
            this.CurrentDepartment = this.AllDepartments.Count > 0 ? this.AllDepartments[0] : null;

            // Реагирование на изменения вне этой модели ViewModel
            this.AllDepartments.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.CurrentDepartment))
                {
                    this.CurrentDepartment = null;
                }
            };

            this.AddDepartmentCommand = new DelegateCommand((o) => this.AddDepartment());
            this.DeleteDepartmentCommand = new DelegateCommand((o) => this.DeleteCurrentDepartment(), (o) => this.CurrentDepartment != null);
        }

        /// <summary>
        /// Возвращает команду для добавления нового отдела
        /// </summary>
        public ICommand AddDepartmentCommand { get; private set; }

        /// <summary>
        /// Возвращает команду для удаления текущего отдела
        /// </summary>
        public ICommand DeleteDepartmentCommand { get; private set; }

        /// <summary>
        /// Возвращает все отделы в компании
        /// </summary>
        public ObservableCollection<DepartmentViewModel> AllDepartments { get; private set; }

        /// <summary>
        /// Возвращает или задает отдел, выделенный в настоящее время в рабочей области
        /// </summary>
        public DepartmentViewModel CurrentDepartment
        {
            get
            {
                return this.currentDepartment;
            }

            set
            {
                this.currentDepartment = value;
                this.OnPropertyChanged("CurrentDepartment");
            }
        }

        /// <summary>
        /// Обрабатывает добавление нового отдела в рабочую область и модель
        /// </summary>
        private void AddDepartment()
        {
            Department dep = this.unitOfWork.CreateObject<Department>();
            this.unitOfWork.AddDepartment(dep);

            DepartmentViewModel vm = new DepartmentViewModel(dep);
            this.AllDepartments.Add(vm);
            this.CurrentDepartment = vm;
        }

        /// <summary>
        /// Обрабатывает удаление текущего отдела
        /// </summary>
        private void DeleteCurrentDepartment()
        {
            this.unitOfWork.RemoveDepartment(this.CurrentDepartment.Model);
            this.AllDepartments.Remove(this.CurrentDepartment);
            this.CurrentDepartment = null;
        }
    }
}
