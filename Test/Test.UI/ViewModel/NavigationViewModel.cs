
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Test.UI.Event;
using System.Linq;
using Test.UI.Services.Lookups;
using System;

namespace Test.UI.ViewModel
{
    public class NavigationViewModel :ViewModelBase, INavigationViewModel
    {
        private IEventAggregator _eventAggregator;
        private ILookupDataService _lookupDataService;
 public ObservableCollection<NavigationItemViewModel> Tests { get; }

        public NavigationViewModel(ILookupDataService lookupDataService,IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _lookupDataService = lookupDataService;
            Tests = new ObservableCollection<NavigationItemViewModel>();
            _eventAggregator.GetEvent<AfterTestSaveEvent>().Subscribe(AfterTestSaved);
            _eventAggregator.GetEvent<AfterTestDeletedEvent>().Subscribe(AfterTestDeleted);

        }

        private void AfterTestDeleted(int testKey)
        {
            var test = Tests.SingleOrDefault(t => t.Id == testKey);
            if(test!=null){
                Tests.Remove(test);
            }
        }

        private void AfterTestSaved(AfterTestSavedEventArgs obj)
        {
            var lookupItem = Tests.SingleOrDefault(l => l.Id == obj.Id);
            if (lookupItem == null)
            {
                Tests.Add(new NavigationItemViewModel(obj.Id, obj.DisplayMember, _eventAggregator));
            }
            else {
                lookupItem.DisplayMember = obj.DisplayMember;
            }
        }

       

        public async Task LoadAsync() {
            var lookup = await _lookupDataService.GetTestLookupAsync();
            Tests.Clear();
            foreach (var item in lookup)
            {
                Tests.Add(new NavigationItemViewModel(item.Id,item.DisplayMember,_eventAggregator));
            }
        }

        

    }
}
