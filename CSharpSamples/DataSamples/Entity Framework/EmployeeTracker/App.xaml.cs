// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker
{
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using EmployeeTracker.Common;
    using EmployeeTracker.EntityFramework;
    using EmployeeTracker.Fakes;
    using EmployeeTracker.Model.Interfaces;
    using EmployeeTracker.View;
    using EmployeeTracker.ViewModel;

    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "Context is disposed when app exits.")]
    public partial class App : Application
    {
        /// <summary>
        /// Единица работы, координирующая изменения для приложения
        /// </summary>
        private IEmployeeContext context;

        /// <summary>
        /// При значении true будет использоваться фиктивный контекст в памяти
        /// При значении false будет использоваться контекст ADO.Net Entity Framework
        /// </summary>
        private bool useFakes = false;

        /// <summary>
        /// Запуск формы записи при включении
        /// </summary>
        /// <param name="e">Аргументы события запуска</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if (this.useFakes)
            {
                this.context =  Generation.BuildFakeSession();
            }
            else
            {
                //ПРИМЕЧАНИЕ. Если Microsoft SQL Server Express недоступен по имени .\SQLEXPRESS, 
                //      потребуется обновить строку подключения "EmployeeEntities" в App.config
                this.context = new EmployeeEntities();
            }

            IDepartmentRepository departmentRepository = new DepartmentRepository(this.context);
            IEmployeeRepository employeeRepository = new EmployeeRepository(this.context);
            IUnitOfWork unit = new UnitOfWork(this.context);

            MainViewModel main = new MainViewModel(unit, departmentRepository, employeeRepository);
            MainView window = new View.MainView { DataContext = main };
            window.Show();
        }

        /// <summary>
        /// Очищает все ресурсы при выходе
        /// </summary>
        /// <param name="e">Аргументы события выхода</param>
        protected override void OnExit(ExitEventArgs e)
        {
            this.context.Dispose();

            base.OnExit(e);
        }
    }
}
