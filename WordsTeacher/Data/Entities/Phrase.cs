using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace WordsTeacher.Data.Entities
{
    public class Phrase : BaseEntity
    {
        public virtual ICollection<PhraseTestMapping> Tests { get; set; }
        public virtual Language PhareLanguage { get; set; }
        public virtual Language TranslationLanguage { get; set; }
        public string BasePharse { get; set; }
        public string TranslatedPharse { get; set; }
        public string TranslationPronunciation { get; set; }
        public string AudioPath { get; set; }
        public string ImagePath { get; set; }
        public DateTime RemaiderTime { get; set; }
        public virtual IdentityUser User { get; set; }
    }
}