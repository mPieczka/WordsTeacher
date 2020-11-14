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
using WordsTeacher.HtmlHelpers;
using WordsTeacher.Models.Datatable;
using WordsTeacher.Models.Tests;
using WordsTeacher.Services;

namespace WordsTeacher.Controllers
{
    [Authorize]
    public class TestController : BaseController
    {
        private readonly TestFactory _testFactory;
        private readonly TestService _testService;
        private readonly PhraseService _phraseService;
        private readonly PhraseFactory _phraseFactory;
        private readonly AjaxFactory _ajaxFactory;

        public TestController(TestFactory testFactory, TestService testService,
            PhraseService phraseService, PhraseFactory phraseFactory, AjaxFactory ajaxFactory)
        {
            _testFactory = testFactory;
            _testService = testService;
            _phraseService = phraseService;
            _phraseFactory = phraseFactory;
            _ajaxFactory = ajaxFactory;
        }

        public IActionResult Index()
        {
            return View(new TestListModel());
        }

        public IActionResult GetTests(DataTableReqest request)
        {
            return Ok(_testFactory.PrepareList(request));
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

                AddSuccessMessage(DefaultMessages.CreatedMessage);
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
                AddSuccessMessage(DefaultMessages.UpdatedMessage);

                return RedirectToAction(nameof(Index));
            }
            return View("CreateEdit", model);
        }

        public IActionResult Delete(int id)
        {
            var test = _testService.GetById(id);

            if (test == null)
            {
                return Ok(_ajaxFactory.NotFound());
            }
            _testService.Delete(test);
            return Ok(_ajaxFactory.SuccesfullyDeleted());
        }

        public IActionResult Test(int id)
        {
            var test = _testService.GetById(id);
            if (test == null)
                return RedirectToAction(nameof(Index));

            return View(new TestCompleteViewModel
            {
                Id = id,
                Phrases = test.Phrases.Where(a => !a.Correct)
                .Select(a => _phraseFactory.PreparePhraseViewModel(a.Phrase, test)).ToList()
            });
        }

        public IActionResult CompleteTest(int id, int[] correctAnswersPhrasesId)
        {
            var test = _testService.GetById(id);
            if (test == null)
                return Ok(_ajaxFactory.NotFound());
            
            foreach (var phraseId in correctAnswersPhrasesId)
            {
                test.Phrases.First(a => a.PhraseId == phraseId).Correct = true;
            }

            test.CorrectAnswers = correctAnswersPhrasesId.Length;

            _testService.Update(test);

            return Ok(_ajaxFactory.Successful());
        }
    }
}