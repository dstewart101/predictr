using System;
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

            var prediction = await _context.Predictions.Include("Fixture")
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

                String currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var currentPredictions = _context.Predictions.ToList();

                int doublesUsed = currentPredictions
                                    .Where(p => p.ApplicationUserId == currentUserId)
                                    .Where(p => p.DoubleUp == true)
                                    .Count();

                int jokersUsed = currentPredictions
                                    .Where(p => p.ApplicationUserId == currentUserId)
                                    .Where(p => p.Joker == true)
                                    .Count();



                if (fixture.FixtureDateTime < DateTime.Now)
                {
                    return RedirectToAction("Index", "MyPredictr");
                }

                if (fixture == null)
                {
                    return NotFound();
                }

                _thisPrediction.HomeTeam = fixture.Home;
                _thisPrediction.AwayTeam = fixture.Away;

                if (jokersUsed < 3)
                {
                    _thisPrediction.JokerDisabled = false;
                }
                else {
                    _thisPrediction.JokerDisabled = true;
                }

                if (doublesUsed < 3)
                {
                    _thisPrediction.DoubleUpDisabled = false;
                }
                else
                {
                    _thisPrediction.DoubleUpDisabled = true;
                }
                
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

                String currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                var currentPredictions = _context.Predictions.ToList();

                int doublesUsed = currentPredictions
                                    .Where(p => p.ApplicationUserId == currentUserId)
                                    .Where(p => p.DoubleUp == true)
                                    .Count();

                int jokersUsed = currentPredictions
                                    .Where(p => p.ApplicationUserId == currentUserId)
                                    .Where(p => p.Joker == true)
                                    .Count();

                var fixture = _context.Fixtures.SingleOrDefault(f => f.Id == id);

                if (fixture.FixtureDateTime < DateTime.Now)
                {
                    return RedirectToAction("Index", "MyPredictr");
                }

                _fullPrediction.ApplicationUserId = currentUserId;
                _fullPrediction.FixtureId = id;
                _fullPrediction.HomeScore = prediction.HomeScore;
                _fullPrediction.AwayScore = prediction.AwayScore;

                if (jokersUsed < 3)
                {
                    _fullPrediction.Joker = prediction.Joker;
                }
                else
                {
                    // back to the offending prediction
                    return RedirectToAction("Create", "Predictions", new { id }); // redirect to create prediction 
                }

                if (doublesUsed < 3)
                {
                    _fullPrediction.DoubleUp = prediction.DoubleUp;
                }
                else
                {
                    // back to the offending prediction
                    return RedirectToAction("Create", "Predictions", new { id });
                }

                _fullPrediction.Joker = prediction.Joker;
                _fullPrediction.DoubleUp = prediction.DoubleUp;

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

            if (prediction.Fixture.FixtureDateTime < DateTime.Now)
            {
                return RedirectToAction("Index", "MyPredictr");
            }

            if (prediction.ApplicationUserId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
            {
                return Unauthorized();
            }

            VM_EditPrediction vm = new VM_EditPrediction();

            vm.HomeTeam = prediction.Fixture.Home;
            vm.AwayTeam = prediction.Fixture.Away;
            vm.HomeScore = prediction.HomeScore;
            vm.AwayScore = prediction.AwayScore;
            vm.Joker = prediction.Joker;
            vm.DoubleUp = prediction.DoubleUp;

            return View(vm);
        }

        // POST: Predictions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VM_EditPrediction prediction)
        {
            var predictionToUpdate = await _context.Predictions.Include("Fixture")
                .SingleOrDefaultAsync(m => m.Id == id);

            if (predictionToUpdate == null)
            {
                return NotFound();
            }

            if (predictionToUpdate.ApplicationUserId != User.FindFirst(ClaimTypes.NameIdentifier).Value) {
                return Unauthorized();
            }

            if (predictionToUpdate.Fixture.FixtureDateTime < DateTime.Now)
            {
                return RedirectToAction("Index", "MyPredictr");
            }

            if (ModelState.IsValid)
            {
                try
                {
                    predictionToUpdate.HomeScore = prediction.HomeScore;
                    predictionToUpdate.AwayScore = prediction.AwayScore;
                    predictionToUpdate.Joker = prediction.Joker;
                    predictionToUpdate.DoubleUp = prediction.DoubleUp;

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
