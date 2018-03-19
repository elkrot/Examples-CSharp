﻿using System;
using System.Threading.Tasks;
using Prism.Events;
using Test.UI.View.Services;
using Test.UI.Services.Repositories;
using System.Collections.ObjectModel;
using Test.UI.Wrapper;
using System.ComponentModel;
using Prism.Commands;
using System.Linq;

namespace Test.UI.ViewModel
{
    public class ProgrammingLanguageDetailViewModel : DetailViewModelBase
    {
        private IProgrammingLanguageRepository _programmingLanguageRepository;

        public ProgrammingLanguageDetailViewModel(
            IEventAggregator eventAggregator
            , IMessageDialogService _messageDialogService
            , IProgrammingLanguageRepository programmingLanguageRepository) 
            : base(eventAggregator, _messageDialogService)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
            Title = "Prog Lang";
            ProgrammingLanguages = new ObservableCollection<ProgrammingLanguageWrapper>();
        }

        public ObservableCollection<ProgrammingLanguageWrapper> ProgrammingLanguages { get; }

        public async override Task LoadAsync(int id)
        {
            
            Id = id;
            foreach (var wrapper in ProgrammingLanguages)
            {
                wrapper.PropertyChanged -= Wrapper_PropertyChanged;
            }
            ProgrammingLanguages.Clear();

            var languages = await _programmingLanguageRepository.GetAllAsync();

            foreach (var model in languages)
            {
                var wrapper = new ProgrammingLanguageWrapper(model);
                wrapper.PropertyChanged += Wrapper_PropertyChanged;
                ProgrammingLanguages.Add(wrapper);
            }
        }

        private void Wrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges) {
                HasChanges = _programmingLanguageRepository.HasChanges();
            }
            if (e.PropertyName == nameof(ProgrammingLanguageWrapper.HasErrors)) {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        protected override void OnDeleteExecute()
        {
            throw new NotImplementedException();
        }

        protected override bool OnSaveCanExecute()
        {
            return HasChanges && ProgrammingLanguages.All(p => !p.HasErrors);
        }

        protected async override void OnSaveExecute()
        {
            await _programmingLanguageRepository.SaveAsync();
            HasChanges = _programmingLanguageRepository.HasChanges();
        }
    }
}
