using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace WordsTeacher.Data.Entities
{
    public class Phrase : BaseEntity
    {
        public virtual ICollection<PhraseTestMapping> Tests { get; set; }
        public virtual Language PhraseLanguage { get; set; }
        public virtual Language TranslationLanguage { get; set; }
        public string BasePhrase { get; set; }
        public string BasePhrasePronunciation { get; set; }
        public string TranslatedPhrase { get; set; }
        public string TranslationPronunciation { get; set; }
        public string AudioPath { get; set; }
        public string ImagePath { get; set; }
        public DateTime RemaiderTimeUtc { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}