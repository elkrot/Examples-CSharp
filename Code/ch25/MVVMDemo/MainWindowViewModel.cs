using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace MVVMDemo
{
    class MainWindowViewModel : BaseViewModel
    {
        private WidgetRepository _widgets = new WidgetRepository();
        private int _nextId = 5;

        //this is better than RoutedUICommand when using MVVM
        public DelegateCommand ExitCommand {get;private set;}
        public DelegateCommand OpenAllWidgetsListCommand { get; private set; }
        public DelegateCommand ViewWidgetCommand { get; private set; }
        public DelegateCommand AddWidgetCommand { get; private set; }

        public ObservableCollection<BaseViewModel> OpenViews { get; private set; }
        
        public MainWindowViewModel()
            :base("MVVM Demo")
        {
            ExitCommand = new DelegateCommand(executeDelegate => OnClose());
            OpenAllWidgetsListCommand = new DelegateCommand(executeDelegate => OpenAllWidgetsList());
            ViewWidgetCommand = new DelegateCommand(executeDelegate => ViewWidget());
            AddWidgetCommand = new DelegateCommand(executeDelegate => AddNewWidget());

            OpenViews = new ObservableCollection<BaseViewModel>();
        }

        public event EventHandler<EventArgs> Close;

        protected void OnClose()
        {
            if (Close != null)
            {
                Close(this, EventArgs.Empty);
            }
        }

        private void OpenAllWidgetsList()
        {
            OpenViews.Add(new AllWidgetsViewModel(_widgets));
        }

        private void ViewWidget()
        {
            OpenViews.Add(new WidgetViewModel(_widgets[0]));
        }

        private void AddNewWidget()
        {
            _widgets.AddWidget(new Widget(_nextId++, "New Widget", WidgetType.TypeA));
        }
    }
}
