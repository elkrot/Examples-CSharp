// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Обозначает адрес сотрудника
    /// </summary>
    public class Address : ContactDetail
    {
        /// <summary>
        /// Значения использования, допустимые для адресов
        /// </summary>
        private static string[] validUsageValues = new string[] { "Business", "Home", "Mailing" };

        /// <summary>
        /// Получает список значений использования, допустимых для адресов
        /// </summary>
        public override IEnumerable<string> ValidUsageValues
        {
            get { return validUsageValues; }
        }

        /// <summary>
        /// Получает или задает первую строку данного адреса
        /// </summary>
        public virtual string LineOne { get; set; }

        /// <summary>
        /// Получает или задает вторую строку данного адреса
        /// </summary>
        public virtual string LineTwo { get; set; }

        /// <summary>
        /// Получает или задает город данного адреса
        /// </summary>
        public virtual string City { get; set; }

        /// <summary>
        /// Получает или задает штат данного адреса
        /// </summary>
        public virtual string State { get; set; }

        /// <summary>
        /// Получает или задает почтовый индекс данного адреса
        /// </summary>
        public virtual string ZipCode { get; set; }

        /// <summary>
        /// Получает или задает страну данного адреса
        /// </summary>
        public virtual string Country { get; set; }
    }
}
