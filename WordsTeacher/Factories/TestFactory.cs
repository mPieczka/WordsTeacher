﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsTeacher.Data.Entities;
using WordsTeacher.Models.Datatable;
using WordsTeacher.Models.Tests;
using WordsTeacher.Services;

namespace WordsTeacher.Factories
{
    public class TestFactory
    {
        private readonly TestService _testService;
        private readonly IMapper _mapper;
        private readonly LanguageService _languageService;
        private readonly PhraseService _phraseService;

        public TestFactory(TestService testService, IMapper mapper,
            LanguageService languageService, PhraseService phraseService)
        {
            _testService = testService;
            _mapper = mapper;
            _languageService = languageService;
            _phraseService = phraseService;
        }

        public DatatableResponseDTO<TestListModel> PrepareList()
        {
            var testsList = _testService.GetTests();

            return new DatatableResponseDTO<TestListModel>()
            {
                Draw = 1,
                RecordsFiltered = testsList.Count,
                RecordTotal = testsList.Count,
                Data = _mapper.Map<List<TestListModel>>(testsList)
            };
        }

        public TestViewModel PrepareTestViewModel(Test test)
        {
            return PrepareTestViewModelWithDefaults(_mapper.Map<TestViewModel>(test));
        }

        public TestViewModel PrepareTestViewModel()
        {
            return PrepareTestViewModelWithDefaults(new TestViewModel());
        }

        public Test PrepareTest(TestViewModel model, Test test)
        {
            return _mapper.Map(model, test);
        }

        public Test PrepareTest(TestViewModel model)
        {
            var test = new Test();
            test.BaseLanguage = _languageService.GetLanguageById(model.PickedBaseLanguage);
            test.TranslationLanguage = _languageService.GetLanguageById(model.PickedTranslationLanguage);
            test.Phrases = new List<PhraseTestMapping>();
            foreach (var item in model.PickedPharses)
            {
                test.Phrases.Add(new PhraseTestMapping
                {
                    PhraseId = item
                });
            }
            return test;
        }

        private TestViewModel PrepareTestViewModelWithDefaults(TestViewModel model)
        {
            var languages = _languageService.GetLanguages();
            model.PickedBaseLanguage = languages.First(a => a.FullName == "English").Id;
            model.PickedTranslationLanguage = languages.First(a => a.FullName == "Polish").Id;
            PreapareAvailableProperties(model, languages);
            return model;
        }

        public TestViewModel PreapareAvailableProperties(TestViewModel model, List<Language> languages = null)
        {
            PrapareLanguagesForViewModel(model, languages);
            int groupIndex = 0;
            model.AvailablePhrases = _phraseService.GetPhrases().GroupBy(a => a.RemaiderTimeUtc).SelectMany(a =>
            {
                var groupName = $"Group {++groupIndex}"; return a.Select(b => new SelectListItem
                {
                    Value = b.Id.ToString(),
                    Text = b.BasePhrase,
                    Group = new SelectListGroup
                    {
                        Name = groupName,
                    }
                });
            }).ToList();
            return model;
        }

        private void PrapareLanguagesForViewModel(TestViewModel model, List<Language> languages = null)
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