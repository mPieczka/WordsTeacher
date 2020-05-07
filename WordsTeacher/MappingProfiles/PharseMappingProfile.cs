using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsTeacher.Data.Entities;
using WordsTeacher.Models;
using WordsTeacher.Models.Phrases;

namespace WordsTeacher.MappingProfiles
{
    public class PharseMappingProfile : Profile
    {
        public PharseMappingProfile()
        {
            CreateMap<Phrase, PhraseViewModel>();
            CreateMap<Phrase, PhraseListModel>()
                .ForMember(dest => dest.TranslationLanguageName,
                cfg => cfg.MapFrom(src => src.TranslationLanguage == null ? "" : src.TranslationLanguage.FullName))
                .ForMember(dest => dest.BaseLanguageName,
                cfg => cfg.MapFrom(src => src.PhraseLanguage == null ? "" : src.PhraseLanguage.FullName));
            CreateMap<PhraseViewModel, Phrase>()
                .ForMember(dest => dest.Id, cfg => cfg.Ignore());
            CreateMap<PhraseListModel, Phrase>();
        }
    }
}