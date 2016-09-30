// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects;
    using System.Linq;
    using EmployeeTracker.Model;
    using EmployeeTracker.Model.Interfaces;

    /// <summary>
    /// Репозиторий для получения данных отдела из IObjectSet
    /// </summary>
    public class DepartmentRepository : IDepartmentRepository
    {
        /// <summary>
        /// Базовый ObjectSet, из которого извлекаются данные
        /// </summary>
        private IObjectSet<Department> objectSet;

        /// <summary>
        /// Инициализирует новый экземпляр класса DepartmentRepository.
        /// </summary>
        /// <param name="context">Контекст, из которого извлекаются данные</param>
        public DepartmentRepository(IEmployeeContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            this.objectSet = context.Departments;
        }

        /// <summary>
        /// Все отделы для организации
        /// </summary>
        /// <returns>Перечисляемый тип всех отделов</returns>
        public IEnumerable<Department> GetAllDepartments()
        {
            // ПРИМЕЧАНИЕ. Некоторые моменты, рассмотренные в ходе реализации методов доступа к данным:
            //    -  ToList используется для обеспечения инициации любых исключений, связанных с доступом к данным
            //       во время выполнения этого метода, а не во время перечисления данных.
            //    - Возвращение IEnumerable вместо IQueryable обеспечивает полный контроль репозитория
            //       над получением данных из хранилища; возвращение IQueryable позволит пользователям
            //       добавлять дополнительные операторы и изменять запрос, отправляемый в хранилище.
            return this.objectSet.ToList();
        }
    }
}
