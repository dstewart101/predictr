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
using Predictr.ViewModels;

namespace Predictr.Controllers
{
    [RequireHttps]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        // GET: ApplicationUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var predictions = _context.Predictions.Include("Fixture").Where(p => p.ApplicationUserId == id).Where(p => p.Fixture.FixtureDateTime <= DateTime.Now).ToList();

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            VM_User vm = new VM_User();
            vm.Id = applicationUser.Id;
            vm.FirstName = applicationUser.FirstName;
            vm.Surname = applicationUser.Surname;
            vm.Predictions = predictions;
            vm.Points = predictions.Sum(p => p.Points);

            return View(vm);
        }


        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }
    }
}
