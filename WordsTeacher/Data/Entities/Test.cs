using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordsTeacher.Data.Entities
{
    public class Test : BaseEntity
    {
        public virtual Language BaseLanguage { get; set; }
        public virtual Language TranslationLanguage { get; set; }
        public int CorrectAnswers { get; set; }
        public virtual ICollection<PhraseTestMapping> Phrases { get; set; }
    }
}