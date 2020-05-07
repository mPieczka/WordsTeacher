using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsTeacher.Models.Phrases
{
    public class PhraseListModel
    {
        public int Id { get; set; }
        public string BasePhrase { get; set; }
        public string TranslatedPhrase { get; set; }
        public string BaseLanguageName { get; set; }
        public string TranslationLanguageName { get; set; }
    }
}