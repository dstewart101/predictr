﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Predictr.Data;
using Predictr.Models;
using Predictr.ViewModels;

namespace Predictr.Controllers
{
    [Authorize]
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
        public IActionResult Create(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            else
            {
                var _fixtureId = id;
                VM_CreatePrediction _thisPrediction = new VM_CreatePrediction();

                var fixture = _context.Fixtures.SingleOrDefault(f => f.Id == _fixtureId);

                if (fixture == null)
                {
                    return NotFound();
                }

                _thisPrediction.HomeTeam = fixture.Home;
                _thisPrediction.AwayTeam = fixture.Away;

                return View(_thisPrediction);
            }
        }

        // POST: Predictions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VM_CreatePrediction prediction, int id)
        {
            if (ModelState.IsValid)
            {
                VM_CreatePrediction _prediction = prediction;

                Prediction _fullPrediction = new Prediction();

                _fullPrediction.ApplicationUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                _fullPrediction.FixtureId = id;
                _fullPrediction.HomeScore = prediction.HomeScore;
                _fullPrediction.AwayScore = prediction.AwayScore;

               
                _context.Add(_fullPrediction);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "MyPredictr");
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

            var prediction = await _context.Predictions.Include("Fixture").SingleOrDefaultAsync(m => m.Id == id);

            if (prediction == null)
            {
                return NotFound();
            }

            VM_EditPrediction vm = new VM_EditPrediction();

            vm.HomeTeam = prediction.Fixture.Home;
            vm.AwayTeam = prediction.Fixture.Away;
            vm.HomeScore = prediction.HomeScore;
            vm.HomeScore = prediction.AwayScore;

            return View(vm);
        }

        // POST: Predictions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VM_EditPrediction prediction)
        {
            var predictionToUpdate = await _context.Predictions
                .SingleOrDefaultAsync(m => m.Id == id);

            if (predictionToUpdate == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    predictionToUpdate.HomeScore = prediction.HomeScore;
                    predictionToUpdate.AwayScore = prediction.AwayScore;

                    _context.Update(predictionToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PredictionExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "MyPredictr");
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
