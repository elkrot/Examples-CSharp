﻿using Autofac.Features.Indexed;
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
        
        private IDetailViewModel _detailViewModel;
        private IMessageDialogService _messageDialogService;
        
        private IIndex<string, IDetailViewModel> _detailViewModelCreator;

        public IDetailViewModel DetailViewModel
        {
            get { return _detailViewModel; }
            private set { _detailViewModel = value;
                OnPropertyChanged();
            }
        }


        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        public MainViewModel(
            INavigationViewModel navigationViewModel
            , IIndex<string,IDetailViewModel> detailViewModelCreator
            , IEventAggregator eventAggregator
            , IMessageDialogService messageDialogService)
        {
             _messageDialogService = messageDialogService;

            _detailViewModelCreator = detailViewModelCreator;

            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OpenDetailViewEvent>()
    .Subscribe(OnOpenDetailView);

            _eventAggregator.GetEvent<AfterDeletedEvent>()
    .Subscribe(OnAfterDeleted);

            CreateNewCommand = new DelegateCommand<Type>(OnCreateNewExecute);
             NavigationViewModel = navigationViewModel;
        }

        private void OnAfterDeleted(AfterDeletedEventArgs obj)
        {
            DetailViewModel = null;
        }

        private void OnCreateNewExecute(Type viewModelType)
        {
            OnOpenDetailView(new OpenDetailViewEventArgs { ViewModelName=viewModelType.Name});
        }

        public ICommand CreateNewCommand {get;}

        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            if(DetailViewModel!=null && DetailViewModel.HasChanges){
                var result = _messageDialogService.ShowOKCancelDialog("?", "Q");
                if (result == MessageDialogResult.Cancel) {
                    return;
                }
            }

            DetailViewModel = _detailViewModelCreator[args.ViewModelName];
            
            await DetailViewModel.LoadAsync(args.Id);
        }
    }


}
