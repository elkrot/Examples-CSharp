// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using EmployeeTracker.Model;
    using EmployeeTracker.Model.Interfaces;

    /// <summary>
    /// Репозиторий для получения данных о сотруднике из ObjectSet
    /// </summary>
    public class EmployeeRepository : IEmployeeRepository
    {
        /// <summary>
        /// Базовый ObjectSet, из которого извлекаются данные
        /// </summary>
        private IObjectSet<Employee> objectSet;

        /// <summary>
        /// Инициализирует новый экземпляр класса EmployeeRepository.
        /// </summary>
        /// <param name="context">Контекст, из которого извлекаются данные</param>
        public EmployeeRepository(IEmployeeContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.objectSet = context.Employees;
        }

        /// <summary>
        /// Все сотрудники для организации
        /// </summary>
        /// <returns>Перечисляемый тип всех сотрудников</returns>  
        public IEnumerable<Employee> GetAllEmployees()
        {
            // ПРИМЕЧАНИЕ. Некоторые моменты, рассмотренные в ходе реализации методов доступа к данным:
            //    -  ToList используется для обеспечения инициации любых исключений, связанных с доступом к данным
            //       во время выполнения этого метода, а не во время перечисления данных.
            //    - Возвращение IEnumerable вместо IQueryable обеспечивает полный контроль репозитория
            //       над получением данных из хранилища; возвращение IQueryable позволит пользователям
            //       добавлять дополнительные операторы и изменять запрос, отправляемый в хранилище.
            return this.objectSet.ToList();
        }

        /// <summary>
        /// Получает сотрудников с наибольшим сроком работы
        /// </summary>
        /// <param name="quantity">Количество возвращаемых сотрудников</param>
        /// <returns>Перечисляемый тип сотрудников с наибольшим сроком работы</returns>
        public IEnumerable<Employee> GetLongestServingEmployees(int quantity)
        {
            // ПРИМЕЧАНИЕ. При запуске на основе набора имитаций объектов произойдет сортировка по сроку владения в памяти
            //       При запуске на основе EF будет использоваться функция, определенная моделью, объявленная в EmployeeModel.edmx,
            //       и сортировка будет обработана в хранилище
            return this.objectSet
                .Where(e => e.TerminationDate == null)
                .OrderByDescending(e => GetTenure(e))
                .Take(quantity)
                .ToList();
        }

        /// <summary>
        /// Вычисляет длительность работы сотрудника в компании
        /// </summary>
        /// <param name="employee">Сотрудник, для которого рассчитывается срок пребывания в должности</param>
        /// <returns>Срок пребывания должности, выраженный в годах</returns>
        [EdmFunction("EmployeeTracker.EntityFramework", "GetTenure")]
        private static int GetTenure(Employee employee)
        {
            // ПРИМЕЧАНИЕ. Тело для этого метода добавлено для облегчения выполнения на основе имитаций в памяти
            //       EF не требует реализации, см. замечания в GetLongestServingEmployees()
            DateTime endDate = employee.TerminationDate ?? DateTime.Today;
            return endDate.Subtract(employee.HireDate).Days / 365;
        }
    }
}
