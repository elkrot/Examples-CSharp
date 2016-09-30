// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.ViewModel
{
    using System;
    using EmployeeTracker.Model;

    /// <summary>
    /// ViewModel отдельного адреса
    /// </summary>
    public class AddressViewModel : ContactDetailViewModel
    {
        /// <summary>
        /// Объект адреса для резервирования этой модели ViewModel
        /// </summary>
        private Address address;
        
        /// <summary>
        /// Инициализирует новый экземпляр класса AddressViewModel.
        /// </summary>
        /// <param name="detail">Базовый адрес, на котором должна быть основана эта модель ViewModel</param>
        public AddressViewModel(Address detail)
        {
            if (detail == null)
            {
                throw new ArgumentNullException("detail");
            }

            this.address = detail;
        }

        /// <summary>
        /// Базовый адрес, на котором основана эта модель ViewModel
        /// </summary>
        public override ContactDetail Model
        {
            get { return this.address; }
        }

        /// <summary>
        /// Возвращает или задает первую строку адреса
        /// </summary>
        public string LineOne
        {
            get
            {
                return this.address.LineOne;
            }

            set
            {
                this.address.LineOne = value;
                this.OnPropertyChanged("LineOne");
            }
        }

        /// <summary>
        /// Возвращает или задает вторую строку адреса
        /// </summary>
        public string LineTwo
        {
            get
            {
                return this.address.LineTwo;
            }

            set
            {
                this.address.LineTwo = value;
                this.OnPropertyChanged("LineTwo");
            }
        }

        /// <summary>
        /// Возвращает или задает город данного адреса
        /// </summary>
        public string City
        {
            get
            {
                return this.address.City;
            }

            set
            {
                this.address.City = value;
                this.OnPropertyChanged("City");
            }
        }

        /// <summary>
        /// Возвращает или задает республику данного адреса
        /// </summary>
        public string State
        {
            get
            {
                return this.address.State;
            }

            set
            {
                this.address.State = value;
                this.OnPropertyChanged("State");
            }
        }

        /// <summary>
        /// Возвращает или задает почтовый индекс данного адреса
        /// </summary>
        public string ZipCode
        {
            get
            {
                return this.address.ZipCode;
            }

            set
            {
                this.address.ZipCode = value;
                this.OnPropertyChanged("ZipCode");
            }
        }

        /// <summary>
        /// Возвращает или задает страну данного адреса
        /// </summary>
        public string Country
        {
            get
            {
                return this.address.Country;
            }

            set
            {
                this.address.Country = value;
                this.OnPropertyChanged("Country");
            }
        }
    }
}
