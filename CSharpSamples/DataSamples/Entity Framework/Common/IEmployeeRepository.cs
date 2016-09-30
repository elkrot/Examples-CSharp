// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.Model.Interfaces
{
    using System.Collections.Generic;
    using EmployeeTracker.Model;

    /// <summary>
    /// Репозиторий для получения данных о сотруднике
    /// </summary>
    public interface IEmployeeRepository
    {
        /// <summary>
        /// Все сотрудники для организации
        /// </summary>
        /// <returns>Перечисляемый тип всех сотрудников</returns>  
        IEnumerable<Employee> GetAllEmployees();

        /// <summary>
        /// Получает сотрудников с наибольшим сроком работы
        /// </summary>
        /// <param name="quantity">Количество возвращаемых сотрудников</param>
        /// <returns>Перечисляемый тип сотрудников с наибольшим сроком работы</returns>
        IEnumerable<Employee> GetLongestServingEmployees(int quantity);
    }
}
