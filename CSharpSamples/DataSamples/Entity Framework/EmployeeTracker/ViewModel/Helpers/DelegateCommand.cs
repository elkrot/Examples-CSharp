// © Корпорация Майкрософт (Microsoft Corp.). Все права защищены.
// Этот код выпущен на условиях 
// открытой лицензии Майкрософт (MS-PL, http://opensource.org/licenses/ms-pl.html.)


namespace EmployeeTracker.ViewModel.Helpers
{
    using System;
    using System.Diagnostics;
    using System.Windows.Input;

    /// <summary>
    /// Реализация ICommand, основанная на делегатах
    /// </summary>
    public class DelegateCommand : ICommand
    {
        /// <summary>
        /// Действие, которое должно быть выполнено при выполнении этой команды
        /// </summary>
        private Action<object> executionAction;

        /// <summary>
        /// Предикат, который должен быть определен, если команда допустима для выполнения
        /// </summary>
        private Predicate<object> canExecutePredicate;

        /// <summary>
        /// Инициализирует новый экземпляр класса DelegateCommand.
        /// Команда будет всегда допустима для выполнения.
        /// </summary>
        /// <param name="execute">Делегат, вызываемый при выполнении</param>
        public DelegateCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса DelegateCommand.
        /// </summary>
        /// <param name="execute">Делегат, вызываемый при выполнении</param>
        /// <param name="canExecute">Предикат, который должен быть определен, если команда допустима для выполнения</param>
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this.executionAction = execute;
            this.canExecutePredicate = canExecute;
        }

        /// <summary>
        /// Инициируется, если изменяется CanExecute
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Выполняет делегата, резервирующего эту команду DelegateCommand
        /// </summary>
        /// <param name="parameter">параметр, передаваемый в предикат</param>
        /// <returns>Значение true, если команда допустима для выполнения</returns>
        public bool CanExecute(object parameter)
        {
            return this.canExecutePredicate == null ? true : this.canExecutePredicate(parameter);
        }

        /// <summary>
        /// Выполняет делегата, резервирующего эту команду DelegateCommand
        /// </summary>
        /// <param name="parameter">параметр, передаваемый для делегата</param>
        /// <exception cref="InvalidOperationException">Инициируется, если CanExecute возвращает значение false</exception>
        public void Execute(object parameter)
        {
            if (!this.CanExecute(parameter))
            {
                throw new InvalidOperationException("The command is not valid for execution, check the CanExecute method before attempting to execute.");
            }

            this.executionAction(parameter);
        }
    }
}
