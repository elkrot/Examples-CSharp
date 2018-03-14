
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Test.UI.Event;
using System.Linq;
using Test.UI.Services.Lookups;
using System;

namespace Test.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IEventAggregator _eventAggregator;
        private ILookupDataService _lookupDataService;
        private IMeetingLookupDataService _meetingLookupDataService;

        public ObservableCollection<NavigationItemViewModel> Tests { get; }

        public ObservableCollection<NavigationItemViewModel> Meetings { get; }

        public NavigationViewModel(ILookupDataService lookupDataService
            ,IMeetingLookupDataService meetingLookupDataService
            , IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _lookupDataService = lookupDataService;

            _meetingLookupDataService = meetingLookupDataService;
            Meetings = new ObservableCollection<NavigationItemViewModel>();

            Tests = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterSaveEvent>().Subscribe(AfterSaved);
            _eventAggregator.GetEvent<AfterDeletedEvent>().Subscribe(AfterDeleted);

        }

        private void AfterDeleted(AfterDeletedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(TestDetailViewModel):
                    AfterDetailDelited(Tests, args);
                    break;
                case nameof(MeetingDetailViewModel):
                    AfterDetailDelited(Meetings, args);
                    break;
            }
        }

        private void AfterDetailDelited(ObservableCollection<NavigationItemViewModel> items
            , AfterDeletedEventArgs args)
        {
            var item = items.SingleOrDefault(t => t.Id == args.Id);
            if (item != null)
            {
                items.Remove(item);
            }
        }

        private void AfterSaved(AfterTestSavedEventArgs args)
        {
            switch (args.ViewModelName)
            {
                case nameof(TestDetailViewModel):
                    AfterDetailSaved(Tests, args);
                    break;
                case nameof(MeetingDetailViewModel):
                    AfterDetailSaved(Meetings, args);
                    break;
            }

        }

        private void AfterDetailSaved(ObservableCollection<NavigationItemViewModel> items
            , AfterTestSavedEventArgs args)
        {
            var lookupItem = items.SingleOrDefault(l => l.Id == args.Id);
            if (lookupItem == null)
            {
                items.Add(new NavigationItemViewModel(args.Id, args.DisplayMember,
                    args.ViewModelName
                    , _eventAggregator));
            }
            else
            {
                lookupItem.DisplayMember = args.DisplayMember;
            }
        }

        public async Task LoadAsync()
        {
            var lookup = await _lookupDataService.GetTestLookupAsync();
            Tests.Clear();
            foreach (var item in lookup)
            {
                Tests.Add(new NavigationItemViewModel(item.Id, item.DisplayMember, nameof(TestDetailViewModel), _eventAggregator));
            }


            lookup = await _meetingLookupDataService.GetMeetingLookupAsync();
            Meetings.Clear();
            foreach (var item in lookup)
            {
                Meetings.Add(new NavigationItemViewModel(item.Id, 
                    item.DisplayMember, nameof(MeetingDetailViewModel), _eventAggregator));
            }


        }



    }
}
