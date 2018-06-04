using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Predictr.Data;
using Predictr.ViewModels;
using System.Linq;

namespace Predictr.Controllers
{
    [RequireHttps]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public IActionResult Index()
        {

            VM_Admin vm = new VM_Admin();

            var allFixtures = _context.Fixtures.ToList();
            vm.Fixtures = allFixtures.OrderBy(p => p.FixtureDateTime).ToList();

            var allUsers = _context.Users.ToList();
            vm.Users = allUsers.OrderBy(p => p.Surname).ToList();

            return View("Index", vm);
        }
    }
}