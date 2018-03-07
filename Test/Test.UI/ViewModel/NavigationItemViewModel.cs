using Prism.Commands;
using Prism.Events;
using System.Windows.Input;
using Test.UI.Event;

namespace Test.UI.ViewModel
{
    public class NavigationItemViewModel:ViewModelBase
    {
        
        public NavigationItemViewModel(int id,string displayMember, IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            OpenTestDetailViewCommand = new DelegateCommand(OnOpenTestDetailView);
            _eventAggregator = eventAggregator;
        }

        public int Id { get; }
        private string _displayMember;
        private IEventAggregator _eventAggregator;

        public string DisplayMember
        {
            get { return _displayMember; }
            set { _displayMember = value;
                OnPropertyChanged();
            }
        }
        public ICommand OpenTestDetailViewCommand { get; }

        private void OnOpenTestDetailView()
        {
            _eventAggregator.GetEvent<OpenTestDetailViewEvent>().Publish(Id);
        }


    }
}
