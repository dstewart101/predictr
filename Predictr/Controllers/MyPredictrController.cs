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
    public class MyPredictrController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MyPredictrController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fixtures
        public IActionResult Index()
        {
            var user = User.FindFirst(ClaimTypes.NameIdentifier);

            VM_MyPredictr vm = new VM_MyPredictr();
            vm.Predictions = _context.Predictions.Where(p => p.ApplicationUser == user.Value).ToList();

            var allFixtures = _context.Fixtures.ToList();

            foreach (Fixture fixture in allFixtures.ToList())
            {
                int searchingFor = fixture.Id;

                foreach (Prediction prediction in vm.Predictions)
                {
                    if (prediction.FixtureId == searchingFor)
                    {
                        allFixtures.Remove(fixture);
                    }
                }

            }

            vm.UnPredictedFixtures = allFixtures.OrderBy(p => p.FixtureDateTime).ToList();



            return View("Index", vm);
        }
    }
        
}
