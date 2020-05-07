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
        private readonly IMapper _mapper;
        private readonly LanguageService _languageService;
        private readonly PhraseService _phraseService;

        public PhraseFactory(IMapper mapper, LanguageService languageService, PhraseService phraseService)
        {
            _mapper = mapper;
            _languageService = languageService;
            _phraseService = phraseService;
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
            return _mapper.Map<Phrase>(model);
        }

        public Phrase PreparePhrase(PhraseViewModel model, Phrase phrase)
        {
            _mapper.Map(model, phrase);
            phrase.PhraseLanguage = _languageService.GetLanguageById(model.PickedBaseLanguage);
            phrase.TranslationLanguage = _languageService.GetLanguageById(model.PickedTranslationLanguage);
            return phrase;
        }

        public DatatableResponseDTO<PhraseListModel> PrepareList()
        {
            var phraseList = _phraseService.GetPhrases();

            return new DatatableResponseDTO<PhraseListModel>()
            {
                Draw = 1,
                RecordsFiltered = phraseList.Count,
                RecordTotal = phraseList.Count,
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