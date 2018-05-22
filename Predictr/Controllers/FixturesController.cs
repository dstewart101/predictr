using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Predictr.Data;
using Predictr.Models;
using Predictr.Services;
using Predictr.ViewModels;

namespace Predictr.Controllers
{
    public class FixturesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FixturesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fixtures
        public async Task<IActionResult> Index()
        {

            List<Fixture> fixtures = _context.Fixtures.OrderBy(f => f.FixtureDateTime).ToList();

            List<VM_Fixture> vm_fixtures = new List<VM_Fixture>();

            foreach (Fixture fixture in fixtures) {
                vm_fixtures.Add(new VM_Fixture {
                    MatchDetails = fixture.FixtureDateTime.ToString("d MMM H:mm") + " / " + fixture.Home + " vs " + fixture.Away,
                    Score = fixture.HomeScore + " - " + fixture.AwayScore
                }
                );
            }

            return View(vm_fixtures);
        }

        // GET: Fixtures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixture = await _context.Fixtures
                .SingleOrDefaultAsync(m => m.Id == id);
            if (fixture == null)
            {
                return NotFound();
            }

            return View(fixture);
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
                _context.Add(fixture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index", "Admin");
        }

        // GET: Fixtures/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixture = await _context.Fixtures.SingleOrDefaultAsync(m => m.Id == id);
            if (fixture == null)
            {
                return NotFound();
            }

            var vm_fixture = new VM_EditFixture();
            vm_fixture.Home = fixture.Home;
            vm_fixture.Away = fixture.Away;
            vm_fixture.HomeScore = fixture.HomeScore;
            vm_fixture.AwayScore = fixture.AwayScore;
            vm_fixture.FixtureDateTime = fixture.FixtureDateTime;
            vm_fixture.Group = fixture.Group;



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


            var actualFixture = await _context.Fixtures.SingleOrDefaultAsync(m => m.Id == id);

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

                    _context.Update(actualFixture);

                    if (scoreHasChanged)
                    {
                        var predictions = _context.Predictions.Where(p => p.FixtureId == actualFixture.Id).ToList();
                        PredictionHandler pp = new PredictionHandler(predictions, actualFixture);
                        predictions = pp.updatePredictions();
                    }
                    _context.SaveChanges();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fixture = await _context.Fixtures
                .SingleOrDefaultAsync(m => m.Id == id);
            if (fixture == null)
            {
                return NotFound();
            }

            return View(fixture);
        }

        // POST: Fixtures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fixture = await _context.Fixtures.SingleOrDefaultAsync(m => m.Id == id);
            _context.Fixtures.Remove(fixture);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Admin");
        }

        private bool FixtureExists(int id)
        {
            return _context.Fixtures.Any(e => e.Id == id);
        }
    }
}
