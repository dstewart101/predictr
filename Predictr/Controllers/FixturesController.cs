using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Predictr.Data;
using Predictr.Interfaces;
using Predictr.Models;
using Predictr.Services;
using Predictr.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Predictr.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FixturesController : Controller
    {
        private IFixtureRepository _fixtureRepository;
        private ApplicationDbContext _context;

        public FixturesController(IFixtureRepository fixtureRepository, ApplicationDbContext context)
        {
            _fixtureRepository = fixtureRepository;
            _context = context;
        }

        // GET: Fixtures
        public async Task<IActionResult> Index()
        {

            Task<List<Fixture>> fixtures = _fixtureRepository.GetAll();

            List<VM_Fixture> vm_fixtures = new List<VM_Fixture>();

            foreach (Fixture fixture in await fixtures)
            {
                vm_fixtures.Add(new VM_Fixture
                {
                    MatchDetails = fixture.FixtureDateTime.ToString("d MMM H:mm") + " / " + fixture.Home + " vs " + fixture.Away,
                    Score = fixture.HomeScore + " - " + fixture.AwayScore
                }
                );
            }

            return View(vm_fixtures);
        }

        // GET: Fixtures/Details/5
        public async Task<IActionResult> Details(int id)
        {
            Task<Fixture> fixture = _fixtureRepository.GetSingleFixture(id);

            if (fixture == null)
            {
                return NotFound();
            }

            return View(await fixture);
        }

        [Authorize(Roles="Admin")]
        // GET: Fixtures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fixtures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FixtureDateTime,Home,HomeScore,Away,AwayScore,Result,Group")] Fixture fixture)
        {
            if (ModelState.IsValid)
            {
                _fixtureRepository.Add(fixture);
                await _fixtureRepository.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "Admin");
        }

        // GET: Fixtures/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var fixture = _fixtureRepository.GetSingleFixture(id);
            if (fixture == null)
            {
                return NotFound();
            }

            var vm_fixture = new VM_EditFixture();

            vm_fixture.Home = fixture.Result.Home;
            vm_fixture.Away = fixture.Result.Away;
            vm_fixture.HomeScore = fixture.Result.HomeScore;
            vm_fixture.AwayScore = fixture.Result.AwayScore;
            vm_fixture.FixtureDateTime = fixture.Result.FixtureDateTime;
            vm_fixture.Group = fixture.Result.Group;

            return View(vm_fixture);
        }

        // POST: Fixtures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id, HomeScore, AwayScore")] VM_EditFixture fixture)
        {
            Boolean scoreHasChanged = false;

            var actualFixture = await _fixtureRepository.GetSingleFixture(id);

            if (id != actualFixture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if ((fixture.HomeScore != actualFixture.HomeScore) || fixture.AwayScore != actualFixture.AwayScore)
                    {
                        scoreHasChanged = true;
                    }

                    actualFixture.HomeScore = fixture.HomeScore;
                    actualFixture.AwayScore = fixture.AwayScore;
                    actualFixture.Result = fixture.HomeScore + " - " + fixture.AwayScore;

                    await _fixtureRepository.SaveChanges();

                    if (scoreHasChanged)
                    {
                        var predictions = _context.Predictions.Where(p => p.FixtureId == actualFixture.Id).ToList();
                        PredictionHandler pp = new PredictionHandler(predictions, actualFixture);
                        predictions = pp.updatePredictions();
                    }

                    await _fixtureRepository.SaveChanges();
                }


                catch (DbUpdateConcurrencyException)
                {
                    if (!FixtureExists(actualFixture.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Admin");
            }
            return View(fixture);
        }

        // GET: Fixtures/Delete/5

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var fixture = _fixtureRepository.GetSingleFixture(id);
            if (fixture == null)
            {
                return NotFound();
            }

            return View(await fixture);
        }

        // POST: Fixtures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fixture = await _fixtureRepository.GetSingleFixture(id);
            _fixtureRepository.Delete(fixture);
            await _fixtureRepository.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }

        private bool FixtureExists(int id)
        {
            return _fixtureRepository.FixtureExists(id);
        }
    }
}
