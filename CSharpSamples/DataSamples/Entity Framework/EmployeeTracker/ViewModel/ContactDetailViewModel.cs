// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.ViewModel
{
    using System;
    using System.Collections.Generic;
    using EmployeeTracker.Model;
    using EmployeeTracker.ViewModel.Helpers;

    /// <summary>
    /// Общие функциональные возможности для моделей ViewModel отдельных данных ContactDetail
    /// </summary>
    public abstract class ContactDetailViewModel : ViewModelBase
    {
        /// <summary>
        /// Возвращает значения, которые могут быть назначены свойству Usage этой модели ViewModel
        /// </summary>
        public IEnumerable<string> ValidUsageValues
        {
            get { return this.Model.ValidUsageValues; }
        }

        /// <summary>
        /// Возвращает базовые данные ContactDetail, на которых основана эта модель ViewModel
        /// </summary>
        public abstract ContactDetail Model { get; }

        /// <summary>
        /// Возвращает или задает, как должны использоваться эти данные, т.е. домашний/рабочий адрес и т.д.
        /// Возможные значения доступны из свойства ValidUsageValues
        /// </summary>
        public string Usage
        {
            get
            {
                return this.Model.Usage;
            }

            set
            {
                this.Model.Usage = value;
                this.OnPropertyChanged("Usage");
            }
        }

        /// <summary>
        /// Конструирует модель просмотра для представления предоставленных данных ContactDetail
        /// </summary>
        /// <param name="detail">Данные, для которых должны быть сконструирована модель ViewModel</param>
        /// <returns>Сконструированная модель ViewModel или null, если она не может быть построена</returns>
        public static ContactDetailViewModel BuildViewModel(ContactDetail detail)
        {
            if (detail == null)
            {
                throw new ArgumentNullException("detail");
            }

            Email e = detail as Email;
            if (e != null)
            {
                return new EmailViewModel(e);
            }

            Phone p = detail as Phone;
            if (p != null)
            {
                return new PhoneViewModel(p);
            }

            Address a = detail as Address;
            if (a != null)
            {
                return new AddressViewModel(a);
            }

            return null;
        }
    }
}
