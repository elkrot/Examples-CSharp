// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.Common
{
    using System;
    using System.Data.Objects;
    using EmployeeTracker.Model;

    /// <summary>
    /// Контекст данных, содержащий данные для модели EmployeeTracker
    /// </summary>
    public interface IEmployeeContext : IDisposable
    {
        /// <summary>
        /// Получает сотрудников в контексте данных
        /// </summary>
        IObjectSet<Employee> Employees { get; }

        /// <summary>
        /// Получает отделы в контексте данных
        /// </summary>
        IObjectSet<Department> Departments { get; }

        /// <summary>
        /// Получает ContactDetails в контексте данных
        /// </summary>
        IObjectSet<ContactDetail> ContactDetails { get; }

        /// <summary>
        /// Сохранить все ожидающие изменения в контекст данных
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
        /// Проверяет, выполняется ли в этом контексте данных отслеживание предоставленного объекта
        /// </summary>
        /// <param name="obj">Проверяемый объект</param>
        /// <returns>Если объект отслеживается - true, в противном случае - false</returns>
        bool IsObjectTracked(object obj);
    }
}
