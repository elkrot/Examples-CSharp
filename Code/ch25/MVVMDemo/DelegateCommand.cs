using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace MVVMDemo
{
    //this is a common type of class, also known as RelayCommand
    class DelegateCommand : ICommand
    {
        //delegates to control command
        private Action<object> _execute;
        private Predicate<object> _canExecute;

        public DelegateCommand(Action<object> executeDelegate)
            :this(executeDelegate, null)
        {
        }
        public DelegateCommand(Action<object> executeDelegate, Predicate<object> canExecuteDelegate)
        {
            _execute = executeDelegate;
            _canExecute = canExecuteDelegate;
        }
        #region ICommand Members

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }
            return _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion
    }
}
