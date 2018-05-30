using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Predictr.Data;
using Predictr.Interfaces;
using Predictr.Models;
using Predictr.Repositories;
using Predictr.Services;
using Predictr.ViewModels;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Predictr.Controllers
{
    [Authorize]
    public class PredictionsController : Controller
    {
        private IPredictionRepository _predictionsRepository;
        private IFixtureRepository _fixturesRepository;

        public PredictionsController(IPredictionRepository predictionRepository, IFixtureRepository fixtureRepository)
        {
            _predictionsRepository = predictionRepository;
            _fixturesRepository = fixtureRepository;
        }

        // GET: Predictions
        public async Task<IActionResult> Index()
        {
            return View(await _predictionsRepository.GetAll());
        }

        // GET: Predictions/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var prediction = await _predictionsRepository.GetSinglePrediction(id);

            if (prediction == null)
            {
                return NotFound();
            }

            return View(prediction);
        }

        // GET: Predictions/Create
        public async Task<IActionResult> CreateAsync(int id)
        {
            VM_CreatePrediction _thisPrediction = new VM_CreatePrediction();

            var fixture = await _fixturesRepository.GetSingleFixture(id);

            String currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var currentPredictions = await _predictionsRepository.GetAll();

            PredictionHandler ph = new PredictionHandler(currentPredictions, User.FindFirst(ClaimTypes.NameIdentifier).Value);

            int doublesUsed = ph.CountDoublesPlayed();
            int jokersUsed = ph.CountJokersPlayed();



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
            else
            {
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



                var currentPredictions = await _predictionsRepository.GetAll();

                PredictionHandler ph = new PredictionHandler(currentPredictions, User.FindFirst(ClaimTypes.NameIdentifier).Value);

                int doublesUsed = ph.CountDoublesPlayed();
                int jokersUsed = ph.CountJokersPlayed();

                var fixture = await _fixturesRepository.GetSingleFixture(id);

                if (fixture.FixtureDateTime < DateTime.Now)
                {
                    return RedirectToAction("Index", "MyPredictr");
                }

                _fullPrediction.ApplicationUserId = currentUserId;
                _fullPrediction.FixtureId = id;
                _fullPrediction.HomeScore = prediction.HomeScore;
                _fullPrediction.AwayScore = prediction.AwayScore;

                if (jokersUsed < 3 || prediction.Joker == false)
                {
                    _fullPrediction.Joker = prediction.Joker;
                }
                else if (jokersUsed == 3 && _prediction.Joker == true)
                {
                    // back to the offending prediction
                    return RedirectToAction("Create", "Predictions", new { id }); // redirect to create prediction 
                }

                if (doublesUsed < 3 || prediction.DoubleUp == false)
                {
                    _fullPrediction.DoubleUp = prediction.DoubleUp;
                }

                else if (doublesUsed == 3 && _prediction.DoubleUp == true)
                {
                    // back to the offending prediction
                    return RedirectToAction("Create", "Predictions", new { id });
                }

                
                _predictionsRepository.Add(_fullPrediction);
                await _predictionsRepository.SaveChanges();
                return RedirectToAction("Index", "MyPredictr");
            }
            return View(prediction);
        }

        // GET: Predictions/Edit/5
        public async Task<IActionResult> Edit(int id)
        {

            var prediction = await _predictionsRepository.GetSinglePrediction(id);

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

            String currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var currentPredictions = await _predictionsRepository.GetAll();
            PredictionHandler ph = new PredictionHandler(currentPredictions, User.FindFirst(ClaimTypes.NameIdentifier).Value);


            int doublesUsed = ph.CountDoublesPlayed();
            int jokersUsed = ph.CountJokersPlayed();

            vm.HomeTeam = prediction.Fixture.Home;
            vm.AwayTeam = prediction.Fixture.Away;
            vm.HomeScore = prediction.HomeScore;
            vm.AwayScore = prediction.AwayScore;
            vm.Joker = prediction.Joker;
            vm.DoubleUp = prediction.DoubleUp;

            if (jokersUsed < 3)
            {
                vm.JokerDisabled = false;
            }
            else
            {
                if (prediction.Joker == true)
                {
                    vm.JokerDisabled = false;
                }
                else
                {
                    vm.JokerDisabled = true;
                }
            }

            if (doublesUsed < 3)
            {
                vm.DoubleUpDisabled = false;
            }
            else
            {
                if (prediction.DoubleUp == true)
                {
                    vm.DoubleUpDisabled = false;
                }
                else
                {
                    vm.DoubleUpDisabled = true;
                }
            }

            return View(vm);
        }

        // POST: Predictions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VM_EditPrediction prediction)
        {

            String currentUserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var predictionToUpdate = await _predictionsRepository.GetSinglePrediction(id);

            if (predictionToUpdate == null)
            {
                return NotFound();
            }

            if (predictionToUpdate.ApplicationUserId != currentUserId)
            {
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



                    var currentPredictions = await _predictionsRepository.GetAll();

                    PredictionHandler ph = new PredictionHandler(currentPredictions, User.FindFirst(ClaimTypes.NameIdentifier).Value);

                    int doublesUsed = ph.CountDoublesPlayed();
                    int jokersUsed = ph.CountJokersPlayed();

                    predictionToUpdate.HomeScore = prediction.HomeScore;
                    predictionToUpdate.AwayScore = prediction.AwayScore;


                    if (jokersUsed < 3 || prediction.Joker == false)
                    {
                        predictionToUpdate.Joker = prediction.Joker;
                    }
                    else
                    {
                        if (predictionToUpdate.Joker == true)
                        {
                            predictionToUpdate.Joker = prediction.Joker;
                        }
                        else
                        {
                            // back to the offending prediction
                            return RedirectToAction("Edit", "Predictions", new { id }); // redirect to create prediction
                        }
                    }

                    if (doublesUsed < 3 || prediction.DoubleUp == false)
                    {
                        predictionToUpdate.DoubleUp = prediction.DoubleUp;
                    }
                    else
                    {
                        if (predictionToUpdate.DoubleUp == true)
                        {
                            predictionToUpdate.DoubleUp = prediction.DoubleUp;
                        }
                        else
                        {
                            // back to the offending prediction
                            return RedirectToAction("Edit", "Predictions", new { id }); // redirect to create prediction
                        }
                    }

                    predictionToUpdate.Joker = prediction.Joker;
                    predictionToUpdate.DoubleUp = prediction.DoubleUp;

                    _predictionsRepository.Update(predictionToUpdate);
                    await _predictionsRepository.SaveChanges();
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
        public async Task<IActionResult> Delete(int id)
        {
            var prediction = await _predictionsRepository.GetSinglePrediction(id);
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
            var prediction = await _predictionsRepository.GetSinglePrediction(id);
            _predictionsRepository.Delete(prediction);
            await _predictionsRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool PredictionExists(int id)
        {
            return _predictionsRepository.PredictionExists(id);
        }
    }
}
