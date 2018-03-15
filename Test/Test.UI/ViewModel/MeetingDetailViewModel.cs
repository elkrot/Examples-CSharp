using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using Test.UI.View.Services;
using Test.UI.Services.Repositories;
using Test.UI.Wrapper;
using Test.Model;
using Prism.Commands;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Test.UI.ViewModel
{
    public class MeetingDetailViewModel : DetailViewModelBase, IMeetingDetailViewModel
    {
        private IMeetingRepository _meetingRepository;
        private IMessageDialogService _messageDialogService;

        private TestEntity _selectedAvailableTest;
        private TestEntity _selectedAddedTest;

        public MeetingDetailViewModel(IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IMeetingRepository meetingRepository ) : base(eventAggregator)
        {
            _messageDialogService = messageDialogService;
            _meetingRepository = meetingRepository;

            AvailableTests = new ObservableCollection<TestEntity>();
            AddedTests = new ObservableCollection<TestEntity>();

            AddTestCommand = new DelegateCommand(OnAddTestExecute,OnAddTestCanExecute); 
            RemoveTestCommand = new DelegateCommand(OnRemoveTestExecute, OnRemoveTestCanExecute); 

        }

        private bool OnRemoveTestCanExecute()
        {
            return SelectedAddedTest != null;
        }

        private void OnRemoveTestExecute()
        {
            var testToRemove = SelectedAddedTest;
            Meeting.Model.Tests.Remove(testToRemove);
            AddedTests.Remove(testToRemove);
            AvailableTests.Add(testToRemove);
            HasChanges = _meetingRepository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private bool OnAddTestCanExecute()
        {
            return SelectedAvailableTest != null;
        }

        private void OnAddTestExecute()
        {
            var testToAdd = SelectedAvailableTest;
            Meeting.Model.Tests.Add(testToAdd);
            AddedTests.Add(testToAdd);
            AvailableTests.Remove(testToAdd);
            HasChanges = _meetingRepository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private MeetingWrapper  _meeting;
        private List<TestEntity> _allTests;

        public MeetingWrapper Meeting
        {
            get { return _meeting; }
            set { _meeting = value;
                OnPropertyChanged();
            }
        }



        public ObservableCollection<TestEntity> AvailableTests { get; private set; }
        public ICommand AddTestCommand { get; }
        public ICommand RemoveTestCommand { get; }


        

        public TestEntity SelectedAvailableTest
        {
            get { return _selectedAvailableTest; }
            set { _selectedAvailableTest = value;
                OnPropertyChanged();
                ((DelegateCommand)AddTestCommand).RaiseCanExecuteChanged();
            }
        }

        public TestEntity SelectedAddedTest
        {
            get { return _selectedAddedTest; }
            set
            {
                _selectedAddedTest = value;
                OnPropertyChanged();
                ((DelegateCommand)RemoveTestCommand).RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<TestEntity> AddedTests { get; private set; }

        public override async Task LoadAsync(int? meetingId)
        {
            var meeting = meetingId.HasValue
                ? await _meetingRepository.GetByIdAsync(meetingId.Value)
                : CreateNewMeeting();
            InitializeMeeting(meeting);

            _allTests = await _meetingRepository.GetAllTestAsync();
            SetupPicklist();

        }

        private void SetupPicklist()
        {
            var meetingTestIds = Meeting.Model.Tests.Select(f => f.TestKey).ToList();
            var addedTests = _allTests.Where(f => meetingTestIds.Contains(f.TestKey)).OrderBy(f => f.TestTitle);
            var availableTests = _allTests.Except(addedTests).OrderBy(f => f.TestTitle);

            AddedTests.Clear();
            AvailableTests.Clear();

            foreach (var availableTest in availableTests)
            {
                AvailableTests.Add(availableTest);
            }
            foreach (var addedTest in addedTests)
            {
                AddedTests.Add(addedTest);
            }

        }

        private void InitializeMeeting(Meeting meeting)
        {
            Meeting = new MeetingWrapper(meeting);
            Meeting.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _meetingRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Meeting.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
                    
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            if (Meeting.Id == 0) {
                Meeting.Title = "";
            }
        }

        private Meeting CreateNewMeeting()
        {
            var meeting = new Meeting
            {
                DateFrom = DateTime.Now.Date,
                DateTo = DateTime.Now.Date
            };
            _meetingRepository.Add(meeting);
            return meeting;
        }

        protected override void OnDeleteExecute()
        {
            var result = _messageDialogService.ShowOKCancelDialog($"{Meeting.Title}?", "");
            if (result == MessageDialogResult.OK)
            {
                _meetingRepository.Remove(Meeting.Model);
                _meetingRepository.SaveAsync();
                RaiseDetailDelitedEvent(Meeting.Id);
            }
        }

        protected override bool OnSaveCanExecute()
        {
            return Meeting != null && !Meeting.HasErrors && HasChanges;
        }

        protected override async void OnSaveExecute()
        {
            await _meetingRepository.SaveAsync();
            HasChanges = _meetingRepository.HasChanges();
            RaiseDetailSavedEvent(Meeting.Id, Meeting.Title);
        }
    }
}
