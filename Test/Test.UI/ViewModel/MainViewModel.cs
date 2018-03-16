using Autofac.Features.Indexed;
using Prism.Commands;
using Prism.Events;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Test.UI.Event;
using Test.UI.View.Services;

namespace Test.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;

        public INavigationViewModel NavigationViewModel { get; }

        private IDetailViewModel _selectedDetailViewModel;
        private IMessageDialogService _messageDialogService;

        private IIndex<string, IDetailViewModel> _detailViewModelCreator;


        public ObservableCollection<IDetailViewModel> DetailViewModels { get; }

        public IDetailViewModel SelectedDetailViewModel
        {
            get { return _selectedDetailViewModel; }
            set
            {
                _selectedDetailViewModel = value;
                OnPropertyChanged();
            }
        }


        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        public MainViewModel(
            INavigationViewModel navigationViewModel
            , IIndex<string, IDetailViewModel> detailViewModelCreator
            , IEventAggregator eventAggregator
            , IMessageDialogService messageDialogService)
        {
            _messageDialogService = messageDialogService;

            _detailViewModelCreator = detailViewModelCreator;
            DetailViewModels = new ObservableCollection<IDetailViewModel>();
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenDetailViewEvent>()
    .Subscribe(OnOpenDetailView);

            _eventAggregator.GetEvent<AfterDeletedEvent>()
    .Subscribe(OnAfterDeleted);

            CreateNewCommand = new DelegateCommand<Type>(OnCreateNewExecute);
            NavigationViewModel = navigationViewModel;
        }

        private void OnAfterDeleted(AfterDeletedEventArgs args)
        {
            var detailViewModel = DetailViewModels
                .SingleOrDefault(vm => vm.Id == args.Id
                && vm.GetType().Name == args.ViewModelName);

            if (detailViewModel != null) {
                DetailViewModels.Remove(detailViewModel);
            }
        }

        private void OnCreateNewExecute(Type viewModelType)
        {
            OnOpenDetailView(new OpenDetailViewEventArgs { ViewModelName = viewModelType.Name });
        }

        public ICommand CreateNewCommand { get; }

        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            var detailViewModel = DetailViewModels
                 .SingleOrDefault(vm => vm.Id == args.Id
                 && vm.GetType().Name == args.ViewModelName);

            if (detailViewModel == null)
            {
                detailViewModel = _detailViewModelCreator[args.ViewModelName];
                await detailViewModel.LoadAsync(args.Id);
                DetailViewModels.Add(detailViewModel);
            }
            SelectedDetailViewModel = detailViewModel;
        }
    }


}
