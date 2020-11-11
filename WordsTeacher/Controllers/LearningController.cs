using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WordsTeacher.Extensions;
using WordsTeacher.Services;

namespace WordsTeacher.Controllers
{
    public class LearningController : Controller
    {
        private readonly PhraseService _phraseService;

        public LearningController(PhraseService phraseService)
        {
            _phraseService = phraseService;
        }

        public IActionResult Index()
        {
            return View(_phraseService.GetPhrases(toRemaind: true).ConvertAll(a => a.Id).Shuffle());
        }

        public IActionResult CheckIfAble()
        {
            return Ok(_phraseService.GetPhrases(toRemaind: true).Count > 0);
        }
    }
}