using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordsTeacher.Data.Entities
{
    public class PhraseTestMapping
    {
        public virtual Test Test { get; set; }
        public int TestId { get; set; }
        public virtual Phrase Phrase { get; set; }
        public int PhraseId { get; set; }
        public bool Correct { get; set; }
    }
}