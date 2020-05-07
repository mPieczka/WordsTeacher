using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace WordsTeacher.Models.Phrases
{
    public class PhraseViewModel
    {
        public int Id { get; set; }
        public string BasePhrase { get; set; }
        public string BasePhrasePronunciation { get; set; }
        public string TranslatedPhrase { get; set; }
        public string TranslationPronunciation { get; set; }
        public List<SelectListItem> AvailableLanguages { get; set; }
        public int PickedBaseLanguage { get; set; }
        public int PickedTranslationLanguage { get; set; }
    }
}