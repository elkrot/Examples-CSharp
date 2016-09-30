// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Базовый класс, обозначающий контактные данные сотрудника
    /// </summary>
    public abstract class ContactDetail
    {
        /// <summary>
        /// Получает значения, допустимые для свойства использования
        /// </summary>
        public abstract IEnumerable<string> ValidUsageValues { get; }

        /// <summary>
        /// Получает или задает идентификатор данного ContactDetail
        /// </summary>
        public virtual int ContactDetailId { get; set; }

        /// <summary>
        /// Получает или задает идентификатор сотрудника, к которому относится данный ContactDetail
        /// </summary>
        public virtual int EmployeeId { get; set; }

        /// <summary>
        /// Получает или задает, как используются эти контактные данные, т.е. домашний/рабочий адрес и т.д.
        /// </summary>
        public virtual string Usage { get; set; }
    }
}
