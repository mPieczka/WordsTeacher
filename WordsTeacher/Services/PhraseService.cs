﻿using System;
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

        public Phrase GetPhraseById(int id)
        {
            return _phraseRepository.Table.FirstOrDefault(a => a.Id == id);
        }

        public List<Phrase> GetPhrases(int pageSize = int.MaxValue, int pageIndex = 0)
        {
            var query = _phraseRepository.Table;

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