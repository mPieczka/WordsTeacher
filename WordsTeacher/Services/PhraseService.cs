using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordsTeacher.Data.Entities;

namespace WordsTeacher.Services
{
    public class PhraseService
    {
        private readonly Repository<Phrase> _phraseRepository;

        public PhraseService(Repository<Phrase> phraseRepository)
        {
            _phraseRepository = phraseRepository;
        }

        public Phrase GetPhrase(int id)
        {
            return _phraseRepository.Table.FirstOrDefault(a => a.Id == id);
        }

        public Phrase GetPhrase(string basePhrase)
        {
            return _phraseRepository.Table.FirstOrDefault(a => a.BasePhrase == basePhrase);
        }

        public List<Phrase> GetPhrases(int pageSize = int.MaxValue, int pageIndex = 0, bool? toRemaind = null,
            List<int> searchedIds = null, string searchedText = null)
        {
            var query = _phraseRepository.Table;

            if (toRemaind == true)
                query = query.Where(a => a.RemaiderTimeUtc != null && a.RemaiderTimeUtc.Value.Date <= DateTime.UtcNow);

            if (searchedIds != null)
                query = query.Where(a => searchedIds.Contains(a.Id));

            if (!string.IsNullOrEmpty(searchedText))
            {
                query = query.Where(a => a.BasePhrase.Contains(searchedText) || a.TranslatedPhrase.Contains(searchedText) ||
                a.BasePhrasePronunciation.Contains(searchedText) || a.TranslationPronunciation.Contains(searchedText));
            }

            query = query.OrderByDescending(a => a.Id);

            return query.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        public Phrase AddPhrase(Phrase phrase)
        {
            if (phrase == null)
                return null;

            _phraseRepository.Insert(phrase);
            return phrase;
        }

        public Phrase UpdatePhrase(Phrase phrase)
        {
            if (phrase == null)
                return null;

            _phraseRepository.Update(phrase);
            return phrase;
        }

        public void DeletePhrase(Phrase phrase)
        {
            if (phrase == null)
                return;

            _phraseRepository.Delete(phrase);
        }
    }
}