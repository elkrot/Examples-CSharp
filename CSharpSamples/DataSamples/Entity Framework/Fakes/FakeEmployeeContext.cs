// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.Fakes
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using EmployeeTracker.Common;
    using EmployeeTracker.Model;
    using EmployeeTracker.Model.Interfaces;

    /// <summary>
    /// Реализация IEmployeeContext в памяти
    /// </summary>
    public sealed class FakeEmployeeContext : IEmployeeContext
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса FakeEmployeeContext.
        /// Контекст содержит пустые исходные данные.
        /// </summary>
        public FakeEmployeeContext()
        {
            this.Employees = new FakeObjectSet<Employee>();
            this.Departments = new FakeObjectSet<Department>();
            this.ContactDetails = new FakeObjectSet<ContactDetail>();
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса FakeEmployeeContext.
        /// Контекст содержит предоставленные исходные данные.
        /// </summary>
        /// <param name="employees">Сотрудники, включаемые в контекст</param>
        /// <param name="departments">Отделы, включаемые в контекст</param>
        public FakeEmployeeContext(IEnumerable<Employee> employees, IEnumerable<Department> departments)
        {
            if (employees == null)
            {
                throw new ArgumentNullException("employees");
            }

            if (departments == null)
            {
                throw new ArgumentNullException("departments");
            }

            this.Employees = new FakeObjectSet<Employee>(employees);
            this.Departments = new FakeObjectSet<Department>(departments);

            // Получить контактные данные, производные от предоставленных данных о сотруднике
            this.ContactDetails = new FakeObjectSet<ContactDetail>();
            foreach (var emp in employees)
            {
                foreach (var det in emp.ContactDetails)
                {
                    this.ContactDetails.AddObject(det);
                }
            }
        }

        /// <summary>
        /// Инициируется при вызове Save()
        /// </summary>
        public event EventHandler<EventArgs> SaveCalled;

        /// <summary>
        /// Инициируется при вызове Dispose()
        /// </summary>
        public event EventHandler<EventArgs> DisposeCalled;

        /// <summary>
        /// Возвращает всех сотрудников, отслеживаемых в этом контексте
        /// </summary>
        public IObjectSet<Employee> Employees { get; private set; }

        /// <summary>
        /// Возвращает все отделы, отслеживаемые в этом контексте
        /// </summary>
        public IObjectSet<Department> Departments { get; private set; }

        /// <summary>
        /// Возвращает все контактные данные, отслеживаемые в этом контексте
        /// </summary>
        public IObjectSet<ContactDetail> ContactDetails { get; private set; }

        /// <summary>
        /// Сохранить все отложенные изменения в этом контексте
        /// </summary>
        public void Save()
        {
            this.OnSaveCalled(EventArgs.Empty);
        }

        /// <summary>
        /// Освободить все ресурсы, используемые в этом контексте
        /// </summary>
        public void Dispose()
        {
            this.OnDisposeCalled(EventArgs.Empty);
        }

        /// <summary>
        /// Создает новый экземпляр указанного типа объекта
        /// ПРИМЕЧАНИЕ. Этот шаблон применяется для разрешения использования прокси отслеживания изменений
        ///       при запуске на основе Entity Framework.
        /// </summary>
        /// <typeparam name="T">Создаваемый тип</typeparam>
        /// <returns>Новый объект</returns>
        public T CreateObject<T>() where T : class
        {
            return Activator.CreateInstance<T>();
        }

        /// <summary>
        /// Проверяет, выполняется ли в этом контексте отслеживание указанного объекта
        /// </summary>
        /// <param name="entity">Искомый объект</param>
        /// <returns>Если объект отслеживается - true, в противном случае - false</returns>
        public bool IsObjectTracked(object entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            return this.Employees.Contains(entity) 
                || this.Departments.Contains(entity) 
                || this.ContactDetails.Contains(entity);
        }

        /// <summary>
        /// Вызывает событие SaveCalled
        /// </summary>
        /// <param name="e">Аргументы для события</param>
        private void OnSaveCalled(EventArgs e)
        {
            var handler = this.SaveCalled;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Вызывает событие DisposeCalled
        /// </summary>
        /// <param name="e">Аргументы для события</param>
        private void OnDisposeCalled(EventArgs e)
        {
            var handler = this.DisposeCalled;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
