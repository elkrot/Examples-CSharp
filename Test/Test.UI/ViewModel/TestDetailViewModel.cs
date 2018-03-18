using Prism.Commands;
using Prism.Events;
using System.Threading.Tasks;
using System.Windows.Input;
using Test.UI.Event;
using Test.UI.Services.Repositories;
using Test.UI.Wrapper;
using Test.Model;
using Test.UI.View.Services;
using Test.UI.Services.Lookups;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Test.UI.ViewModel
{
    public class TestDetailViewModel : DetailViewModelBase, ITestDetailViewModel
    {
        private ITestRepository _repository;
      
        private TestWrapper _test;

        public TestDetailViewModel(ITestRepository repository, IEventAggregator eventAggregator
            , IMessageDialogService messageService
            , IProgrammingLanguageLookupDataService programmingLanguageLookupDataService)
            :base(eventAggregator,messageService)

        {
            _repository = repository;
            
           

            _programmingLanguageLookupDataService = programmingLanguageLookupDataService;

            AddQuestionCommand = new DelegateCommand(OnAddQuestionExecute);
            RemoveQuestionCommand = new DelegateCommand(OnRemoveQuestionExecute,
                OnRemoveQuestionCanExecute);

            ProgrammingLanguages = new ObservableCollection<LookupItem>();
            Questions = new ObservableCollection<QuestionWrapper>();

        }

        private bool OnRemoveQuestionCanExecute()
        {
            return SelectedQuestion != null;
        }

        private void OnRemoveQuestionExecute()
        {
            SelectedQuestion.PropertyChanged -= Wrapper_PropertyChanged;
            _repository.RemoveQuestion(SelectedQuestion.Model);
            Test.Model.Questions.Remove(SelectedQuestion.Model);
            Questions.Remove(SelectedQuestion);
            SelectedQuestion = null;
            HasChanges = _repository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private void OnAddQuestionExecute()
        {
            var newQuestion = new QuestionWrapper(new Question());
            newQuestion.PropertyChanged += Wrapper_PropertyChanged;
            Questions.Add(newQuestion);
            Test.Model.Questions.Add(newQuestion.Model);
            newQuestion.QuestionTitle = "";

        }

        protected override async void OnDeleteExecute()
        {
            if (await _repository.HasMeetingAsync(Test.TestKey)) {
                MessageDialogService.ShowInfoDialog("!!!");
                return;
            }

            var result = MessageDialogService.ShowOKCancelDialog("?", "title");

            if (result == MessageDialogResult.OK)
            {
                _repository.Remove(Test.Model);
                await _repository.SaveAsync();
                RaiseDetailDelitedEvent(Test.TestKey);
               
            }

        }

        private bool OnDeleteCanExecute()
        {
            return true;
        }

        public TestWrapper Test
        {
            get { return _test; }
            private set
            {
                _test = value;
                OnPropertyChanged();
            }
        }



        public ICommand AddQuestionCommand { get; }
        public ICommand RemoveQuestionCommand { get; }

        protected override bool OnSaveCanExecute()
        {
            return Test != null && !Test.HasErrors
                && Questions.All(q=>!q.HasErrors)
                && HasChanges;
        }

        private bool _hasChanges;

        private IProgrammingLanguageLookupDataService _programmingLanguageLookupDataService;
        public ObservableCollection<LookupItem> ProgrammingLanguages { get; }
        public ObservableCollection<QuestionWrapper> Questions { get; }

        protected override async void OnSaveExecute()
        {
            await _repository.SaveAsync();
            HasChanges = _repository.HasChanges();
            Id = Test.TestKey;
            RaiseDetailSavedEvent(Test.TestKey, $"{Test.TestTitle}");

        }

        public QuestionWrapper SelectedQuestion {
            get { return _selectedQuestion;
            } set { _selectedQuestion = value;
                OnPropertyChanged();
                ((DelegateCommand)RemoveQuestionCommand).RaiseCanExecuteChanged();
            } }

        public QuestionWrapper _selectedQuestion { get; set; }

        public override async Task LoadAsync(int testId)
        {

            var test = testId>0 ?
                await _repository.GetByIdAsync(testId) :
                CreateNewTest();

            Id = testId;
            InitializedTest(test);
            InitializeQuestions(test.Questions);
            await LoadProgrammingLanguagesLookup();
        }

        private void InitializeQuestions(ICollection<Question> questions)
        {
            foreach (var wrapper in Questions)
            {
                wrapper.PropertyChanged -= Wrapper_PropertyChanged;
            }
            Questions.Clear();
            foreach (var question in questions)
            {
                var wrapper = new QuestionWrapper(question);
                Questions.Add(wrapper);
                wrapper.PropertyChanged += Wrapper_PropertyChanged;
            }
        }

        private void Wrapper_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!HasChanges) {
                HasChanges = _repository.HasChanges();
            }
            if (e.PropertyName == nameof(QuestionWrapper.HasErrors)) {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        private void InitializedTest(TestEntity test)
        {
            Test = new TestWrapper(test);
            Test.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _repository.HasChanges();
                }

                if (e.PropertyName == nameof(Test.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }

                if (e.PropertyName == nameof(Test.TestTitle)) {
                    SetTitle();
                }
            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();

            if (Test.TestKey == 0)
            {
                Test.TestTitle = "";
            }
        }

        private void SetTitle()
        {
            Title = $"{Test.TestTitle}";
        }

        private async Task LoadProgrammingLanguagesLookup()
        {
            ProgrammingLanguages.Clear();
            ProgrammingLanguages.Add(new NullLookupItem());
            var lookup = await _programmingLanguageLookupDataService.GetProgrammingLanguageLookupAsync();
            foreach (var lookupItem in lookup)
            {
                ProgrammingLanguages.Add(lookupItem);
            }
        }

        private TestEntity CreateNewTest()
        {
            var test = new TestEntity();
            _repository.Add(test);
            return test;
        }
    }
}
