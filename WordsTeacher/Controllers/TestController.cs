using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class TestController : Controller
    {
        private readonly TestFactory _testFactory;
        private readonly TestService _testService;

        public TestController(TestFactory testFactory, TestService testService)
        {
            _testFactory = testFactory;
            _testService = testService;
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
    }
}