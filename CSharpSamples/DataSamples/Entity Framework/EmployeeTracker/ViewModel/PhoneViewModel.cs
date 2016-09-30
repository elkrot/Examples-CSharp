// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.ViewModel
{
    using System;
    using EmployeeTracker.Model;

    /// <summary>
    /// Модель ViewModel отдельного телефона
    /// </summary>
    public class PhoneViewModel : ContactDetailViewModel
    {
        /// <summary>
        /// Объект телефона для резервирования этой модели ViewModel
        /// </summary>
        private Phone phone;

        /// <summary>
        /// Инициализирует новый экземпляр класса PhoneViewModel.
        /// </summary>
        /// <param name="detail">Базовый телефон, на котором должна быть основана эта модель ViewModel</param>
        public PhoneViewModel(Phone detail)
        {
            if (detail == null)
            {
                throw new ArgumentNullException("detail");
            }

            this.phone = detail;
        }

        /// <summary>
        /// Базовый телефон, на котором основана эта модель ViewModel
        /// </summary>
        public override ContactDetail Model
        {
            get { return this.phone; }
        }

        /// <summary>
        /// Получает или задает действительный номер
        /// </summary>
        public string Number
        {
            get
            {
                return this.phone.Number;
            }

            set
            {
                this.phone.Number = value;
                this.OnPropertyChanged("Number");
            }
        }

        /// <summary>
        /// Получает или задает расширение, которое должно быть использовано с этим телефонным номером
        /// </summary>
        public string Extension
        {
            get
            {
                return this.phone.Extension;
            }

            set
            {
                this.phone.Extension = value;
                this.OnPropertyChanged("Extension");
            }
        }
    }
}
