using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsTeacher.Models.Tests
{
    public class TestListModel
    {
        public int Id { get; set; }

        [Display(Name = "Base Language")]
        public string BaseLanguageName { get; set; }

        [Display(Name = "Translation Language")]
        public string TranslationLanguageName { get; set; }

        [Display(Name = "Correct Answers")]
        public string CorrectAnswers { get; set; }

        public bool CanTryAgain { get; set; }
    }
}