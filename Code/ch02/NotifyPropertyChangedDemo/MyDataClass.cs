using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NotifyPropertyChangedDemo
{
    class MyDataClass : System.ComponentModel.INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        #endregion

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }

        private int _tag = 0;
        public int Tag
        {
            get
            {
                return _tag;
            }
            set
            {
                _tag = value;
                OnPropertyChanged("Tag");
            }
        }
    }
}
