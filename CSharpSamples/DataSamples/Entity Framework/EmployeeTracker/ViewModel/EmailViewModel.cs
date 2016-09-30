// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.ViewModel
{
    using System;
    using EmployeeTracker.Model;

    /// <summary>
    /// Модель ViewModel отдельной электронной почты
    /// </summary>
    public class EmailViewModel : ContactDetailViewModel
    {
        /// <summary>
        /// Объект электронной почты для резервирования этой модели ViewModel
        /// </summary>
        private Email email;

        /// <summary>
        /// Инициализирует новый экземпляр класса EmailViewModel.
        /// </summary>
        /// <param name="detail">Базовая электронная почта, на которой должна быть основана эта модель ViewModel</param>
        public EmailViewModel(Email detail)
        {
            if (detail == null)
            {
                throw new ArgumentNullException("detail");
            }

            this.email = detail;
        }

        /// <summary>
        /// Возвращает базовую электронную почту, на которой основана эта модель ViewModel
        /// </summary>
        public override ContactDetail Model
        {
            get { return this.email; }
        }

        /// <summary>
        /// Получает или задает реальный электронный адрес
        /// </summary>
        public string Address
        {
            get
            {
                return this.email.Address;
            }

            set
            {
                this.email.Address = value;
                this.OnPropertyChanged("Address");
            }
        }
    }
}
