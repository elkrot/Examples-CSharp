using Prism.Commands;
using Prism.Events;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Test.UI.Event;
using Test.UI.View.Services;

namespace Test.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;

        public INavigationViewModel NavigationViewModel { get;}
        private Func<ITestDetailViewModel> _testDetailViewModelCreator { get;  }
        private ITestDetailViewModel _testDetailViewModel;
        private IMessageDialogService _messageDialogService;

        public ITestDetailViewModel TestDetailViewModel
        {
            get { return _testDetailViewModel; }
            private set { _testDetailViewModel = value;
                OnPropertyChanged();
            }
        }


        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        public MainViewModel(
            INavigationViewModel navigationViewModel
            , Func<ITestDetailViewModel> testDetailViewModelCreator
            , IEventAggregator eventAggregator
            , IMessageDialogService messageDialogService)
        {
             _messageDialogService = messageDialogService;
            _testDetailViewModelCreator = testDetailViewModelCreator;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenTestDetailViewEvent>()
    .Subscribe(OnOpenTestDetailView);

            _eventAggregator.GetEvent<AfterTestDeletedEvent>()
    .Subscribe(OnAfterTestDeleted);

            CreateNewTestCommand = new DelegateCommand(OnCreateNewTestExecute);
             NavigationViewModel = navigationViewModel;
        }

        private void OnAfterTestDeleted(int obj)
        {
            TestDetailViewModel = null;
        }

        private void OnCreateNewTestExecute()
        {
            OnOpenTestDetailView(null);
        }

        public ICommand CreateNewTestCommand {get;}

        private async void OnOpenTestDetailView(int? testId)
        {
            if(TestDetailViewModel!=null && TestDetailViewModel.HasChanges){
                var result = _messageDialogService.ShowOKCancelDialog("?", "Q");
                if (result == MessageDialogResult.Cancel) {
                    return;
                }
            }
            TestDetailViewModel = _testDetailViewModelCreator();
            await TestDetailViewModel.LoadAsync(testId);
        }
    }


}
