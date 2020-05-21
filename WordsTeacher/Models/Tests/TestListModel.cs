using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsTeacher.Models.Tests
{
    public class TestListModel
    {
        public int Id { get; set; }
        public string BaseLanguageName { get; set; }
        public string TranslationLanguageName { get; set; }
        public int CorrectAnswers { get; set; }
    }
}