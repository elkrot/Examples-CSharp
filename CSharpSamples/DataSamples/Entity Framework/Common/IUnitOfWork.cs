// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.Common
{
    using System;
    using EmployeeTracker.Model;

    /// <summary>
    /// Инкапсулирует изменения в базовых данных
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Сохранить все ожидающие изменения в этот UnitOfWork
        /// </summary>
        void Save();

        /// <summary>
        /// Создает новый экземпляр указанного типа объекта
        /// ПРИМЕЧАНИЕ. Этот шаблон применяется для разрешения использования прокси отслеживания изменений
        ///       при запуске на основе Entity Framework.
        /// </summary>
        /// <typeparam name="T">Создаваемый тип</typeparam>
        /// <returns>Новый объект</returns>
        T CreateObject<T>() where T : class;

        /// <summary>
        /// Регистрирует добавление нового отдела
        /// </summary>
        /// <param name="department">Добавляемый отдел</param>
        /// <exception cref="InvalidOperationException">Инициируется, если отдел уже добавлен в UnitOfWork</exception>
        void AddDepartment(Department department);

        /// <summary>
        /// Регистрирует добавление нового сотрудника
        /// </summary>
        /// <param name="employee">Добавляемый сотрудник</param>
        /// <exception cref="InvalidOperationException">Инициируется, если сотрудник уже добавлен в UnitOfWork</exception>
        void AddEmployee(Employee employee);

        /// <summary>
        /// Регистрирует добавление новых контактных данных
        /// </summary>
        /// <param name="employee">Сотрудник, для которого добавляются контактные данные</param>
        /// <param name="detail">Добавляемые контактные данные</param>
        /// <exception cref="InvalidOperationException">Инициируется, если сотрудник не отслеживается этим UnitOfWork</exception>
        /// <exception cref="InvalidOperationException">Инициируется, если контактные данные уже добавлены в UnitOfWork</exception>
        void AddContactDetail(Employee employee, ContactDetail detail);

        /// <summary>
        /// Регистрирует удаление существующего отдела
        /// </summary>
        /// <param name="department">Удаляемый отдел</param>
        /// <exception cref="InvalidOperationException">Инициируется, если отдел не отслеживается этим UnitOfWork</exception>
        void RemoveDepartment(Department department);

        /// <summary>
        /// Регистрирует удаление существующего сотрудника
        /// </summary>
        /// <param name="employee">Удаляемый сотрудник</param>
        /// <exception cref="InvalidOperationException">Инициируется, если сотрудник не отслеживается этим UnitOfWork</exception>
        void RemoveEmployee(Employee employee);

        /// <summary>
        /// Регистрирует удаление существующих контактных данных
        /// </summary>
        /// <param name="employee">Сотрудник, чьи контактные данные удаляются</param>
        /// <param name="detail">Удаляемые контактные данные</param>
        /// <exception cref="InvalidOperationException">Инициируется, если сотрудник не отслеживается этим UnitOfWork</exception>
        /// <exception cref="InvalidOperationException">Инициируется, если контактные данные не отслеживаются этим UnitOfWork</exception>
        void RemoveContactDetail(Employee employee, ContactDetail detail);
    }
}
