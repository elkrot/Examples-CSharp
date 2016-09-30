// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.Common
{
    using System;
    using System.Globalization;
    using System.Linq;
    using EmployeeTracker.Model;

    /// <summary>
    /// Инкапсулирует изменения в базовых данных, хранящихся в EmployeeContext
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Изменения в отслеживании базового контекста
        /// </summary>
        private IEmployeeContext underlyingContext;

        /// <summary>
        /// Инициализирует новый экземпляр класса UnitOfWork.
        /// Изменения, зарегистрированные в UnitOfWork, записываются в предоставленном контексте
        /// </summary>
        /// <param name="context">Базовый контекст для данного UnitOfWork</param>
        public UnitOfWork(IEmployeeContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.underlyingContext = context;
        }

        /// <summary>
        /// Сохранить все ожидающие изменения в этот UnitOfWork
        /// </summary>
        public void Save()
        {
            this.underlyingContext.Save();
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
            return this.underlyingContext.CreateObject<T>();
        }

        /// <summary>
        /// Регистрирует добавление нового отдела
        /// </summary>
        /// <param name="department">Добавляемый отдел</param>
        /// <exception cref="InvalidOperationException">Инициируется, если отдел уже добавлен в UnitOfWork</exception>
        public void AddDepartment(Department department)
        {
            if (department == null)
            {
                throw new ArgumentNullException("department");
            }
            
            this.CheckEntityDoesNotBelongToUnitOfWork(department);
            this.underlyingContext.Departments.AddObject(department);
        }

        /// <summary>
        /// Регистрирует добавление нового сотрудника
        /// </summary>
        /// <param name="employee">Добавляемый сотрудник</param>
        /// <exception cref="InvalidOperationException">Инициируется, если сотрудник уже добавлен в UnitOfWork</exception>
        public void AddEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException("employee");
            }

            this.CheckEntityDoesNotBelongToUnitOfWork(employee);
            this.underlyingContext.Employees.AddObject(employee);
        }

        /// <summary>
        /// Регистрирует добавление новых контактных данных
        /// </summary>
        /// <param name="employee">Сотрудник, для которого добавляются контактные данные</param>
        /// <param name="detail">Добавляемые контактные данные</param>
        /// <exception cref="InvalidOperationException">Инициируется, если сотрудник не отслеживается этим UnitOfWork</exception>
        /// <exception cref="InvalidOperationException">Инициируется, если контактные данные уже добавлены в UnitOfWork</exception>
        public void AddContactDetail(Employee employee, ContactDetail detail)
        {
            if (employee == null)
            {
                throw new ArgumentNullException("employee");
            }

            if (detail == null)
            {
                throw new ArgumentNullException("detail");
            }

            this.CheckEntityDoesNotBelongToUnitOfWork(detail);
            this.CheckEntityBelongsToUnitOfWork(employee);

            this.underlyingContext.ContactDetails.AddObject(detail);
            employee.ContactDetails.Add(detail);
        }

        /// <summary>
        /// Регистрирует удаление существующего отдела
        /// </summary>
        /// <param name="department">Удаляемый отдел</param>
        /// <exception cref="InvalidOperationException">Инициируется, если отдел не отслеживается этим UnitOfWork</exception>
        public void RemoveDepartment(Department department)
        {
            if (department == null)
            {
                throw new ArgumentNullException("department");
            }

            this.CheckEntityBelongsToUnitOfWork(department);
            foreach (var emp in department.Employees.ToList())
            {
                emp.Department = null;
            }

            this.underlyingContext.Departments.DeleteObject(department);
        }

        /// <summary>
        /// Регистрирует удаление существующего сотрудника
        /// </summary>
        /// <param name="employee">Удаляемый сотрудник</param>
        /// <exception cref="InvalidOperationException">Инициируется, если сотрудник не отслеживается этим UnitOfWork</exception>
        public void RemoveEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException("employee");
            }

            this.CheckEntityBelongsToUnitOfWork(employee);
            employee.Manager = null;
            foreach (var e in employee.Reports.ToList())
            {
                e.Manager = null;
            }

            this.underlyingContext.Employees.DeleteObject(employee);
        }

        /// <summary>
        /// Регистрирует удаление существующих контактных данных
        /// </summary>
        /// <param name="employee">Сотрудник, чьи контактные данные удаляются</param>
        /// <param name="detail">Удаляемые контактные данные</param>
        /// <exception cref="InvalidOperationException">Инициируется, если сотрудник не отслеживается этим UnitOfWork</exception>
        /// <exception cref="InvalidOperationException">Инициируется, если контактные данные не отслеживаются этим UnitOfWork</exception>
        public void RemoveContactDetail(Employee employee, ContactDetail detail)
        {
            if (employee == null)
            {
                throw new ArgumentNullException("employee");
            }

            if (detail == null)
            {
                throw new ArgumentNullException("detail");
            }

            this.CheckEntityBelongsToUnitOfWork(detail);
            this.CheckEntityBelongsToUnitOfWork(employee);
            if (!employee.ContactDetails.Contains(detail))
            {
                throw new InvalidOperationException("The supplied ContactDetail does not belong to the supplied Employee");
            }

            employee.ContactDetails.Remove(detail);
            this.underlyingContext.ContactDetails.DeleteObject(detail);
        }

        /// <summary>
        /// Подтверждает, что указанная сущность отслеживается в этом UnitOfWork
        /// </summary>
        /// <param name="entity">Искомый объект</param>
        /// <exception cref="InvalidOperationException">Инициируется, если объект не отслеживается этим UnitOfWork</exception>
        private void CheckEntityBelongsToUnitOfWork(object entity)
        {
            if (!this.underlyingContext.IsObjectTracked(entity))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The supplied {0} is not part of this Unit of Work.", entity.GetType().Name));
            }
        }

        /// <summary>
        /// Подтверждает, что указанная сущность не отслеживается в этом UnitOfWork
        /// </summary>
        /// <param name="entity">Искомый объект</param>
        /// <exception cref="InvalidOperationException">Инициируется, если объект отслеживается этим UnitOfWork</exception>
        private void CheckEntityDoesNotBelongToUnitOfWork(object entity)
        {
            if (this.underlyingContext.IsObjectTracked(entity))
            {
                throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "The supplied {0} is already part of this Unit of Work.", entity.GetType().Name));
            }
        }
    }
}
