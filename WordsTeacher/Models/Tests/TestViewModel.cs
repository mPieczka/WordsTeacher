using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsTeacher.Models.Tests
{
    public class TestViewModel
    {
        public int? Id { get; set; }
        public List<SelectListItem> AvailableLanguages { get; set; }
        public int PickedBaseLanguage { get; set; }
        public int PickedTranslationLanguage { get; set; }
        public List<SelectListItem> AvailablePhrases { get; set; }
        public List<int> PickedPharses { get; set; }
    }
}