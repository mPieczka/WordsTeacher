using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WordsTeacher.Data;
using WordsTeacher.Data.Entities;

namespace WordsTeacher.Controllers
{
    public class PhraseController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhraseController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Phrases
        public async Task<IActionResult> Index()
        {
            return View(await _context.Phrases.ToListAsync());
        }

        // GET: Phrases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phrase = await _context.Phrases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phrase == null)
            {
                return NotFound();
            }

            return View(phrase);
        }

        // GET: Phrases/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Phrases/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BasePharse,TranslatedPharse,TranslationPronunciation,AudioPath,ImagePath,RemaiderTime,Id,CreateDateUtc,UpdateTimeUtc")] Phrase phrase)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phrase);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phrase);
        }

        // GET: Phrases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phrase = await _context.Phrases.FindAsync(id);
            if (phrase == null)
            {
                return NotFound();
            }
            return View(phrase);
        }

        // POST: Phrases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BasePharse,TranslatedPharse,TranslationPronunciation,AudioPath,ImagePath,RemaiderTime,Id,CreateDateUtc,UpdateTimeUtc")] Phrase phrase)
        {
            if (id != phrase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phrase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhraseExists(phrase.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(phrase);
        }

        // GET: Phrases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phrase = await _context.Phrases
                .FirstOrDefaultAsync(m => m.Id == id);
            if (phrase == null)
            {
                return NotFound();
            }

            return View(phrase);
        }

        // POST: Phrases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var phrase = await _context.Phrases.FindAsync(id);
            _context.Phrases.Remove(phrase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhraseExists(int id)
        {
            return _context.Phrases.Any(e => e.Id == id);
        }
    }
}