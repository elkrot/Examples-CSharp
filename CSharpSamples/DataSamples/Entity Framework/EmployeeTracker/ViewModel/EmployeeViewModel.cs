// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using EmployeeTracker.Common;
    using EmployeeTracker.Model;
    using EmployeeTracker.ViewModel.Helpers;

    /// <summary>
    /// Модель ViewModel отдельного <см. cref="Employee"/>
    /// </summary>
    public class EmployeeViewModel : BasicEmployeeViewModel
    {
        /// <summary>
        /// Отдел, в который назначен в настоящее время этот сотрудник
        /// </summary>
        private DepartmentViewModel department;

        /// <summary>
        /// Руководитель, который назначен в настоящее время этому сотруднику
        /// </summary>
        private EmployeeViewModel manager;

        /// <summary>
        /// Выбранные в настоящее время контактные данные
        /// </summary>
        private ContactDetailViewModel currentContactDetail;

        /// <summary>
        /// UnitOfWork для управления изменениями
        /// </summary>
        private IUnitOfWork unitOfWork;

        /// <summary>
        /// Инициализирует новый экземпляр класса EmployeeViewModel.
        /// </summary>
        /// <param name="employee">Базовый сотрудник, на котором должна быть основана эта модель ViewModel</param>
        /// <param name="managerLookup">Существующая коллекция работников, используемая для подстановки руководителя</param>
        /// <param name="departmentLookup">Существующая коллекция отделов, используемая для подстановки отдела</param>
        /// <param name="unitOfWork">UnitOfWork для управления изменениями</param>
        public EmployeeViewModel(Employee employee, ObservableCollection<EmployeeViewModel> managerLookup, ObservableCollection<DepartmentViewModel> departmentLookup, IUnitOfWork unitOfWork)
            : base(employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException("employee");
            }

            this.unitOfWork = unitOfWork;
            this.ManagerLookup = managerLookup;
            this.DepartmentLookup = departmentLookup;

            // Построение структур данных для контактных данных
            this.ContactDetails = new ObservableCollection<ContactDetailViewModel>();
            foreach (var detail in employee.ContactDetails)
            {
                ContactDetailViewModel vm = ContactDetailViewModel.BuildViewModel(detail);
                if (vm != null)
                {
                    this.ContactDetails.Add(vm);
                }
            }

            // Реагирование на изменения вне этой модели ViewModel
            this.DepartmentLookup.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.Department))
                {
                    this.Department = null;
                }
            };
            this.ManagerLookup.CollectionChanged += (sender, e) =>
            {
                if (e.OldItems != null && e.OldItems.Contains(this.Manager))
                {
                    this.Manager = null;
                }
            };

            this.AddEmailAddressCommand = new DelegateCommand((o) => this.AddContactDetail<Email>());
            this.AddPhoneNumberCommand = new DelegateCommand((o) => this.AddContactDetail<Phone>());
            this.AddAddressCommand = new DelegateCommand((o) => this.AddContactDetail<Address>());
            this.DeleteContactDetailCommand = new DelegateCommand((o) => this.DeleteCurrentContactDetail(), (o) => this.CurrentContactDetail != null);
        }

        /// <summary>
        /// Возвращает команду для добавления нового адреса электронной почты
        /// </summary>
        public ICommand AddEmailAddressCommand { get; private set; }

        /// <summary>
        /// Возвращает команду для добавления нового телефонного номера
        /// </summary>
        public ICommand AddPhoneNumberCommand { get; private set; }

        /// <summary>
        /// Возвращает команду для добавления нового адреса
        /// </summary>
        public ICommand AddAddressCommand { get; private set; }

        /// <summary>
        /// Возвращает команду для удаления текущего работника
        /// </summary>
        public ICommand DeleteContactDetailCommand { get; private set; }

        /// <summary>
        /// Возвращает или задает выбранные в настоящее время контактные данные
        /// </summary>
        public ContactDetailViewModel CurrentContactDetail
        {
            get
            {
                return this.currentContactDetail;
            }

            set
            {
                this.currentContactDetail = value;
                this.OnPropertyChanged("CurrentContactDetail");
            }
        }
        
        /// <summary>
        /// Возвращает или задает отдел, в который в настоящее время назначен этот работник
        /// </summary>
        public DepartmentViewModel Department
        {
            get
            {
                // Нужно отразить выполненные в модели изменения, чтобы текущее значение проверялось перед возвращением
                if (this.Model.Department == null)
                {
                    return null;
                }
                else if (this.department == null || this.department.Model != this.Model.Department)
                {
                    this.department = this.DepartmentLookup.Where(d => d.Model == this.Model.Department).SingleOrDefault();
                }

                return this.department;
            }

            set
            {
                this.department = value;
                this.Model.Department = (value == null) ? null : value.Model;
                this.OnPropertyChanged("Department");
            }
        }

        /// <summary>
        /// Возвращает или задает руководителя, которому в настоящее время назначен этот работник
        /// </summary>
        public EmployeeViewModel Manager
        {
            get
            {
                // Нужно отразить выполненные в модели изменения, чтобы текущее значение проверялось перед возвращением
                if (this.Model.Manager == null)
                {
                    return null;
                }
                else if (this.manager == null || this.manager.Model != this.Model.Manager)
                {
                    this.manager = this.ManagerLookup.Where(e => e.Model == this.Model.Manager).SingleOrDefault();
                }

                return this.manager;
            }

            set
            {
                this.manager = value;
                this.Model.Manager = (value == null) ? null : value.Model;
                this.OnPropertyChanged("Manager");
            }
        }

        /// <summary>
        /// Возвращает коллекцию отделов, в которые может быть назначен этот работник
        /// </summary>
        public ObservableCollection<DepartmentViewModel> DepartmentLookup { get; private set; }

        /// <summary>
        /// Возвращает коллекцию сотрудников, которые могли бы быть руководителями этого работника
        /// </summary>
        public ObservableCollection<EmployeeViewModel> ManagerLookup { get; private set; }

        /// <summary>
        /// Возвращает контактные данные в файле для этого сотрудника
        /// </summary>
        public ObservableCollection<ContactDetailViewModel> ContactDetails { get; private set; }

        /// <summary>
        /// Обрабатывает добавление новых контактных данных для этого сотрудника
        /// </summary>
        /// <typeparam name="T">Тип добавляемых контактных данных</typeparam>
        private void AddContactDetail<T>() where T : ContactDetail
        {
            ContactDetail detail = this.unitOfWork.CreateObject<T>();
            this.unitOfWork.AddContactDetail(this.Model, detail);

            ContactDetailViewModel vm = ContactDetailViewModel.BuildViewModel(detail);
            this.ContactDetails.Add(vm);
            this.CurrentContactDetail = vm;
        }

        /// <summary>
        /// Обрабатывает удаление текущего сотрудника
        /// </summary>
        private void DeleteCurrentContactDetail()
        {
            this.unitOfWork.RemoveContactDetail(this.Model, this.CurrentContactDetail.Model);
            this.ContactDetails.Remove(this.CurrentContactDetail);
            this.CurrentContactDetail = null;
        }
    }
}
