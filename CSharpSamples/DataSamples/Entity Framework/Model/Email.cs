// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Обозначает электронный адрес сотрудника
    /// </summary>
    public class Email : ContactDetail
    {
        /// <summary>
        /// Значения использования, допустимые для электронных адресов
        /// </summary>
        private static string[] validUsageValues = new string[] { "Business", "Personal" };
        
        /// <summary>
        /// Получает список значений использования, допустимых для электронных адресов
        /// </summary>
        public override IEnumerable<string> ValidUsageValues
        {
            get { return validUsageValues; }
        }

        /// <summary>
        /// Получает или задает реальный электронный адрес
        /// </summary>
        public virtual string Address { get; set; }
    }
}
