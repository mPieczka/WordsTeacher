using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsTeacher.Data.Entities;
using WordsTeacher.Models;
using WordsTeacher.Models.Datatable;
using WordsTeacher.Models.Phrases;
using WordsTeacher.Services;

namespace WordsTeacher.Factories
{
    public class PhraseFactory
    {
        private const int _firstCatalogDaysRemainder = 1;
        private const int _secondCatalogDaysRemainder = 3;
        private const int _thirdCatalogDaysRemainder = 6;
        private const int _fourthCatalogDaysRemainder = 12;
        private const int _fiftCatalogDaysRemainder = 24;

        private readonly IMapper _mapper;
        private readonly LanguageService _languageService;
        private readonly PhraseService _phraseService;

        public PhraseFactory(IMapper mapper, LanguageService languageService, PhraseService phraseService)
        {
            _mapper = mapper;
            _languageService = languageService;
            _phraseService = phraseService;
        }

        public Phrase SetLearningState(Phrase phrase, bool correct)
        {
            if (correct)
                phrase.RemaiderTimeUtc = GetPhraseRemainderTimeAfterCorrectGuess(phrase);
            else
                phrase.RemaiderTimeUtc = GetPhraseRemainderTimeAfterIncorrectGuess(phrase);

            return phrase;
        }

        private DateTime? GetPhraseRemainderTimeAfterCorrectGuess(Phrase phrase)
        {
            var daysdiff = (phrase.RemaiderTimeUtc.GetValueOrDefault() - phrase.UpdateTimeUtc).TotalDays;

            if (daysdiff > _fourthCatalogDaysRemainder + 1)
                return null;

            if (daysdiff > _thirdCatalogDaysRemainder + 1)
                return DateTime.UtcNow.AddDays(_fiftCatalogDaysRemainder);

            if (daysdiff > _secondCatalogDaysRemainder + 1)
                return DateTime.UtcNow.AddDays(_fourthCatalogDaysRemainder);

            if (daysdiff > _firstCatalogDaysRemainder + 1)
                return DateTime.UtcNow.AddDays(_thirdCatalogDaysRemainder);
            else
                return DateTime.UtcNow.AddDays(_secondCatalogDaysRemainder);
        }

        private DateTime? GetPhraseRemainderTimeAfterIncorrectGuess(Phrase phrase)
        {
            var daysdiff = (phrase.RemaiderTimeUtc.GetValueOrDefault() - phrase.UpdateTimeUtc).TotalDays;

            if (daysdiff > _fourthCatalogDaysRemainder + 1)
                return DateTime.UtcNow.AddDays(_fourthCatalogDaysRemainder);

            if (daysdiff > _thirdCatalogDaysRemainder + 1)
                return DateTime.UtcNow.AddDays(_thirdCatalogDaysRemainder);

            if (daysdiff > _secondCatalogDaysRemainder + 1)
                return DateTime.UtcNow.AddDays(_secondCatalogDaysRemainder);
            else
                return DateTime.UtcNow.AddDays(_firstCatalogDaysRemainder);
        }

        public PhraseViewModel PreparePhraseViewModel(Phrase phrase)
        {
            if (phrase == null)
                return PreparePhraseViewModel();

            var model = _mapper.Map<PhraseViewModel>(phrase);
            PrapareLanguagesForViewModel(model);
            model.PickedTranslationLanguage = phrase.TranslationLanguage.Id;
            model.PickedBaseLanguage = phrase.PhraseLanguage.Id;
            return model;
        }

        public PhraseViewModel PreparePhraseViewModel()
        {
            var model = new PhraseViewModel();
            var languages = _languageService.GetLanguages();
            PrapareLanguagesForViewModel(model, languages);
            model.PickedBaseLanguage = languages.First(a => a.FullName == "English").Id;
            model.PickedTranslationLanguage = languages.First(a => a.FullName == "Polish").Id;

            return model;
        }

        public Phrase PreparePhrase(PhraseViewModel model)
        {
            return PreparePhrase(model, new Phrase());
        }

        public Phrase PreparePhrase(PhraseViewModel model, Phrase phrase)
        {
            _mapper.Map(model, phrase);
            phrase.PhraseLanguage = _languageService.GetLanguageById(model.PickedBaseLanguage);
            phrase.TranslationLanguage = _languageService.GetLanguageById(model.PickedTranslationLanguage);
            phrase.RemaiderTimeUtc = DateTime.UtcNow.AddDays(_firstCatalogDaysRemainder);
            return phrase;
        }

        public DatatableResponseDTO<PhraseListModel> PrepareList(DataTableReqest reqest)
        {
            var phraseList = _phraseService.GetPhrases();

            return new DatatableResponseDTO<PhraseListModel>()
            {
                Draw = reqest.Draw,
                RecordsFiltered = phraseList.Count,
                RecordsTotal = phraseList.Count,
                Data = _mapper.Map<List<PhraseListModel>>(phraseList)
            };
        }

        private void PrapareLanguagesForViewModel(PhraseViewModel model, List<Language> languages = null)
        {
            if (languages == null)
                languages = _languageService.GetLanguages();
            model.AvailableLanguages = languages.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.FullName
            }).ToList();
        }
    }
}