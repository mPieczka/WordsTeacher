using AzureSpeechSyntezator.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WordsTeacher.Factories;
using WordsTeacher.HtmlHelpers;
using WordsTeacher.Models.Ajax;
using WordsTeacher.Models.Datatable;
using WordsTeacher.Models.Phrases;
using WordsTeacher.Services;

namespace WordsTeacher.Controllers
{
    [Authorize]
    public class PhraseController : BaseController
    {
        private readonly PhraseService _phraseService;
        private readonly PhraseFactory _phraseFactory;
        private readonly AjaxFactory _ajaxFactory;
        private readonly SpeechSyntezator _speechSyntezator;

        public PhraseController(PhraseService phraseService, PhraseFactory phraseFactory,
            AjaxFactory ajaxFactory, SpeechSyntezator speechSyntezator)
        {
            _phraseService = phraseService;
            _phraseFactory = phraseFactory;
            _ajaxFactory = ajaxFactory;
            _speechSyntezator = speechSyntezator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetPhrases(DataTableReqest request)
        {
            return Ok(_phraseFactory.PrepareList(request));
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
                AddSuccessMessage(DefaultMessages.CreatedMessage);
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
                AddSuccessMessage(DefaultMessages.UpdatedMessage);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var phrase = _phraseService.GetPhraseById(id);

            if (phrase == null)
            {
                return Ok(_ajaxFactory.NotFound());
            }
            _phraseService.DeletePhrase(phrase);
            return Ok(_ajaxFactory.SuccesfullyDeleted());
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

        public async Task<IActionResult> GetPhraseAudio(int phraseId)
        {
            var phrase = _phraseService.GetPhraseById(phraseId);
            if (phrase == null)
                return null;
            string phraseText;
            if (string.Equals(phrase.PhraseLanguage.Code, "pl", System.StringComparison.OrdinalIgnoreCase))
                phraseText = phrase.TranslatedPhrase;
            else
                phraseText = phrase.BasePhrase;

            return File(System.IO.File.ReadAllBytes(await _speechSyntezator.Speak(phraseText)), "audio/wav");
        }
    }
}