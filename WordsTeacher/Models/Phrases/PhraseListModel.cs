using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsTeacher.Models.Phrases
{
    public class PhraseListModel
    {
        public int Id { get; set; }
        [Display(Name = "Phrase")]
        public string BasePhrase { get; set; }
        [Display(Name = "Translated phrase")]
        public string TranslatedPhrase { get; set; }
        [Display(Name = "Language")]
        public string BaseLanguageName { get; set; }
        [Display(Name = "Translation language")]
        public string TranslationLanguageName { get; set; }
    }
}