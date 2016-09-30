// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.Model.Interfaces
{
    using System.Collections.Generic;
    using EmployeeTracker.Model;

    /// <summary>
    /// Репозиторий для получения данных об отделе
    /// </summary>
    public interface IDepartmentRepository
    {
        /// <summary>
        /// Все отделы для организации
        /// </summary>
        /// <returns>Перечисляемый тип всех отделов</returns>
        IEnumerable<Department> GetAllDepartments();
    }
}
