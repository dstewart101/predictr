using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Predictr.Data;
using Predictr.Models;

namespace Predictr.Controllers
{
    public class PredictionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PredictionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Predictions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Predictions.ToListAsync());
        }

        // GET: Predictions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prediction = await _context.Predictions
                .SingleOrDefaultAsync(m => m.Id == id);
            if (prediction == null)
            {
                return NotFound();
            }

            return View(prediction);
        }

        // GET: Predictions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Predictions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FixtureId,ApplicationUser,HomeScore,AwayScore,ModifierId,Points")] Prediction prediction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(prediction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(prediction);
        }

        // GET: Predictions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prediction = await _context.Predictions.SingleOrDefaultAsync(m => m.Id == id);
            if (prediction == null)
            {
                return NotFound();
            }
            return View(prediction);
        }

        // POST: Predictions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FixtureId,ApplicationUser,HomeScore,AwayScore,ModifierId,Points")] Prediction prediction)
        {
            if (id != prediction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prediction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PredictionExists(prediction.Id))
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
            return View(prediction);
        }

        // GET: Predictions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prediction = await _context.Predictions
                .SingleOrDefaultAsync(m => m.Id == id);
            if (prediction == null)
            {
                return NotFound();
            }

            return View(prediction);
        }

        // POST: Predictions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prediction = await _context.Predictions.SingleOrDefaultAsync(m => m.Id == id);
            _context.Predictions.Remove(prediction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PredictionExists(int id)
        {
            return _context.Predictions.Any(e => e.Id == id);
        }
    }
}
