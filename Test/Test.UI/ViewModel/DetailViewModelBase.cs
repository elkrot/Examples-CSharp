using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Test.UI.Event;

namespace Test.UI.ViewModel
{
    public abstract class DetailViewModelBase : ViewModelBase, IDetailViewModel
    {
        private bool _hasChanges;
        protected readonly IEventAggregator EventAggregator;

        public ICommand SaveCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }

        public DetailViewModelBase(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            SaveCommand = new DelegateCommand(OnSaveExecute, OnSaveCanExecute);
            DeleteCommand = new DelegateCommand(OnDeleteExecute);
        }

        public abstract Task LoadAsync(int? id);

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
