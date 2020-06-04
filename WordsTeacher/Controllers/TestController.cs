using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WordsTeacher.Data;
using WordsTeacher.Data.Entities;
using WordsTeacher.Factories;
using WordsTeacher.Models.Tests;
using WordsTeacher.Services;

namespace WordsTeacher.Controllers
{
    [Authorize]
    public class TestController : Controller
    {
        private readonly TestFactory _testFactory;
        private readonly TestService _testService;
        private readonly PhraseService _phraseService;
        private readonly PhraseFactory _phraseFactory;

        public TestController(TestFactory testFactory, TestService testService,
            PhraseService phraseService, PhraseFactory phraseFactory)
        {
            _testFactory = testFactory;
            _testService = testService;
            _phraseService = phraseService;
            _phraseFactory = phraseFactory;
        }

        public IActionResult Index()
        {
            return View(new TestListModel());
        }

        public IActionResult GetTests()
        {
            return Ok(_testFactory.PrepareList());
        }

        public IActionResult Create()
        {
            return View("CreateEdit", _testFactory.PrepareTestViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var test = _testFactory.PrepareTest(model);
                _testService.Insert(test);

                return RedirectToAction(nameof(Index));
            }
            return View("CreateEdit", _testFactory.PreapareAvailableProperties(model));
        }

        public IActionResult Edit(int id)
        {
            var test = _testService.GetById(id);
            if (test == null)
                return RedirectToAction(nameof(Index));

            return View("CreateEdit", _testFactory.PrepareTestViewModel(test));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(TestViewModel model)
        {
            if (ModelState.IsValid)
            {
                var test = _testService.GetById(model.Id.GetValueOrDefault());
                if (test == null)
                    return RedirectToAction(nameof(Index));

                _testFactory.PrepareTest(model, test);
                _testService.Update(test);

                return RedirectToAction(nameof(Index));
            }
            return View("CreateEdit", model);
        }

        public IActionResult Delete(int id)
        {
            return Ok();
        }

        public IActionResult Test(int id)
        {
            var test = _testService.GetById(id);
            if (test == null)
                return RedirectToAction(nameof(Index));

            return View(new TestCompleteViewModel
            {
                Id = id,
                Phrases = test.Phrases.Select(a => _phraseFactory.PreparePhraseViewModel(a.Phrase)).ToList()
            });
        }

        public IActionResult CompleteTest(TestCompleteViewModel model)
        {
            var test = _testService.GetById(model.Id);
            if (test == null)
                return RedirectToAction(nameof(Test), new { model.Id });

            test.CorrectAnswers = test.Phrases.Count(a =>
                model.Phrases.First(b => b.Id == a.PhraseId).TranslatedPhrase.Equals(
                (test.TranslationLanguage.Id == a.Phrase.PhraseLanguage.Id ?
                a.Phrase.BasePhrase : a.Phrase.TranslatedPhrase), StringComparison.InvariantCultureIgnoreCase));

            _testService.Update(test);

            return RedirectToAction(nameof(Index));
        }
    }
}