using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordsTeacher.Data.Entities;
using WordsTeacher.Models.Tests;

namespace WordsTeacher.MappingProfiles
{
    public class TestMappingProfile : Profile
    {
        public TestMappingProfile()
        {
            CreateMap<Test, TestViewModel>()
                .ForMember(dest => dest.PickedTranslationLanguage, opt => opt.MapFrom(src => src.TranslationLanguage != null ? src.TranslationLanguage.Id : 0))
                .ForMember(dest => dest.PickedBaseLanguage, opt => opt.MapFrom(src => src.BaseLanguage != null ? src.BaseLanguage.Id : 0));
            CreateMap<TestViewModel, Test>().ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Test, TestListModel>()
                .ForMember(dest => dest.TranslationLanguageName, opt => opt.MapFrom(src => src.TranslationLanguage != null ? src.TranslationLanguage.FullName : ""))
                .ForMember(dest => dest.CorrectAnswers, opt => opt.MapFrom(src => $"{src.CorrectAnswers} / {src.Phrases.Count}"))
                .ForMember(dest => dest.BaseLanguageName, opt => opt.MapFrom(src => src.BaseLanguage != null ? src.BaseLanguage.FullName : ""));
        }
    }
}