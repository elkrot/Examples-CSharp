// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Обозначает телефонный номер сотрудника
    /// </summary>
    public class Phone : ContactDetail
    {
        /// <summary>
        /// Значения использования, допустимые для телефонных номеров
        /// </summary>
        private static string[] validUsageValues = new string[] { "Business", "Home", "Cell" };

        /// <summary>
        /// Получает список значений использования, допустимых для телефонных номеров
        /// </summary>
        public override IEnumerable<string> ValidUsageValues
        {
            get { return validUsageValues; }
        }

        /// <summary>
        /// Получает или задает реальный телефонный номер
        /// </summary>
        public virtual string Number { get; set; }

        /// <summary>
        /// Получает или задает расширение, связанное с этим телефонным номером
        /// </summary>
        public virtual string Extension { get; set; }
    }
}
