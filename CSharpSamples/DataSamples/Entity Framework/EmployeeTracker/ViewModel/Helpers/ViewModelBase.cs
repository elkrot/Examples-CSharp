// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.ViewModel.Helpers
{
    using System.ComponentModel;

    /// <summary>
    /// Абстрактный базовый класс консолидации общих функциональных возможностей всех моделей ViewModel
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Инициируется, если свойство этого объекта имеет новое значение
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Инициирует это событие PropertyChanged моделей ViewModel
        /// </summary>
        /// <param name="propertyName">Имя свойства, имеющего новое значение</param>
        protected void OnPropertyChanged(string propertyName)
        {
            this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Инициирует это событие PropertyChanged моделей ViewModel
        /// </summary>
        /// <param name="e">Аргументы, детализирующие изменение</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
