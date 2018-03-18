using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Test.UI.Event;
using Test.UI.View.Services;

namespace Test.UI.ViewModel
{
    public abstract class DetailViewModelBase : ViewModelBase, IDetailViewModel
    {
        private bool _hasChanges;
        protected readonly IEventAggregator EventAggregator;

        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public IMessageDialogService MessageDialogService { get;  }

        public DetailViewModelBase(IEventAggregator eventAggregator
            ,IMessageDialogService _messageDialogService)
        {
            EventAggregator = eventAggregator;
            MessageDialogService = _messageDialogService;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
            CloseDetailViewModelCommand = new DelegateCommand(OnCloseDetailViewExecute);
        }

        protected virtual void OnCloseDetailViewExecute()
        {
            if (HasChanges) {
                var result = MessageDialogService.ShowOKCancelDialog(
                    "?","title"
                    );
                if (result == MessageDialogResult.Cancel) {
                    return;
                }
            }
            EventAggregator.GetEvent<AfterDetailClosedEvent>()
                .Publish(new AfterDtailClosedEventArgs {
                    Id = this.Id
                    , ViewModelName = this.GetType().Name
                });
            
        }

        public abstract Task LoadAsync(int id);

        protected abstract void OnDeleteExecute();

        protected abstract bool OnSaveCanExecute();


        public bool HasChanges
        {
            get { return _hasChanges; }
            set
            {
                if (_hasChanges != value)
                {
                    _hasChanges = value;
                    OnPropertyChanged();
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
            }
        }

        private int _id;

        public int Id
        {
            get
            {
                return _id;
            }

            protected set
            {
                _id = value;

            }
        }


        private string _title;

        public string Title
        {
            get
            {
                return _title;
            }

            protected set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public ICommand CloseDetailViewModelCommand { get; private set; }

        protected virtual void RaiseDetailDelitedEvent(int modelId)
        {
            EventAggregator.GetEvent<AfterDeletedEvent>().Publish(
                new AfterDeletedEventArgs
                {
                    Id = modelId,
                    ViewModelName = this.GetType().Name
                }
                );
        }

        protected abstract void OnSaveExecute();

        protected virtual void RaiseDetailSavedEvent(int modelId, string displayMember)
        {
            EventAggregator.GetEvent<AfterSaveEvent>().Publish(new AfterTestSavedEventArgs
            {
                Id = modelId
                ,
                DisplayMember = displayMember
                ,
                ViewModelName = this.GetType().Name
            });
        }
    }
}
