using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordsTeacher.Data.Entities;

namespace WordsTeacher.Services
{
    public class LanguageService
    {
        private readonly Repository<Language> _languageRepository;

        public LanguageService(Repository<Language> languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public List<Language> GetLanguages()
        {
            return _languageRepository.Table.ToList();
        }

        public Language GetLanguageById(int id)
        {
            return _languageRepository.Table.FirstOrDefault(a => a.Id == id);
        }
    }
}