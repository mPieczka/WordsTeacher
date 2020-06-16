using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsTeacher.Models.Tests
{
    public class TestViewModel
    {
        public int? Id { get; set; }
        public List<SelectListItem> AvailableLanguages { get; set; }

        [Display(Name = "Base")]
        public int PickedBaseLanguage { get; set; }

        [Display(Name = "Translation")]
        public int PickedTranslationLanguage { get; set; }

        public List<SelectListItem> AvailablePhrases { get; set; }

        [Display(Name = "Phrases")]
        public List<int> PickedPharses { get; set; }
    }
}