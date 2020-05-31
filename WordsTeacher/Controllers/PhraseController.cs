﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WordsTeacher.Factories;
using WordsTeacher.Models.Phrases;
using WordsTeacher.Services;

namespace WordsTeacher.Controllers
{
    [Authorize]
    public class PhraseController : Controller
    {
        private readonly PhraseService _phraseService;
        private readonly PhraseFactory _phraseFactory;

        public PhraseController(PhraseService phraseService, PhraseFactory phraseFactory)
        {
            _phraseService = phraseService;
            _phraseFactory = phraseFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetPhrases()
        {
            return Ok(_phraseFactory.PrepareList());
        }

        public IActionResult Create()
        {
            return View("CreateEdit", _phraseFactory.PreparePhraseViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhraseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var phrase = _phraseFactory.PreparePhrase(model);
                _phraseService.AddPhrase(phrase);
                return RedirectToAction(nameof(Index));
            }
            return View("CreateEdit", model);
        }

        public IActionResult Edit(int id)
        {
            var phrase = _phraseService.GetPhraseById(id);

            if (phrase == null)
            {
                return NotFound();
            }
            var model = _phraseFactory.PreparePhraseViewModel(phrase);
            return View("CreateEdit", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PhraseViewModel model)
        {
            if (ModelState.IsValid)
            {
                var phrase = _phraseService.GetPhraseById(model.Id);
                _phraseFactory.PreparePhrase(model, phrase);
                _phraseService.UpdatePhrase(phrase);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var phrase = _phraseService.GetPhraseById(id);

            if (phrase == null)
            {
                return NotFound();
            }
            _phraseService.DeletePhrase(phrase);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var phrase = _phraseService.GetPhraseById(id);

            if (phrase == null)
            {
                return NotFound();
            }

            return Ok(_phraseFactory.PreparePhraseViewModel(phrase));
        }

        public IActionResult SetPhraseLearningState(int id, bool correct)
        {
            var phrase = _phraseService.GetPhraseById(id);

            if (phrase == null)
            {
                return NotFound();
            }
            _phraseFactory.SetLearningState(phrase, correct);
            _phraseService.UpdatePhrase(phrase);

            return Ok(new { Success = true });
        }
    }
}