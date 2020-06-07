using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WordsTeacher.Models.Phrases
{
    public class PhraseViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Phrase")]
        public string BasePhrase { get; set; }

        [Display(Name = "Phrase pronunciation")]
        public string BasePhrasePronunciation { get; set; }

        [Display(Name = "Translated phrase")]
        public string TranslatedPhrase { get; set; }

        [Display(Name = "Translated phrase pronunciation")]
        public string TranslationPronunciation { get; set; }

        public List<SelectListItem> AvailableLanguages { get; set; }

        [Display(Name = "Language")]
        public int PickedBaseLanguage { get; set; }

        [Display(Name = "Translation language")]
        public int PickedTranslationLanguage { get; set; }
    }
}