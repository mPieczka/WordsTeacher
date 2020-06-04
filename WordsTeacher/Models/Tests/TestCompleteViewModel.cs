using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WordsTeacher.Models.Phrases;

namespace WordsTeacher.Models.Tests
{
    public class TestCompleteViewModel
    {
        public int Id { get; set; }
        public List<PhraseViewModel> Phrases { get; set; }
    }
}