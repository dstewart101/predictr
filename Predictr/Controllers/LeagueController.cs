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
    
    public class LeagueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeagueController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Fixtures
        public IActionResult Index()
        {

            var predictions = _context.Predictions.Include("ApplicationUser").ToList();

            VM_League vm = new VM_League();

            IEnumerable<PlayerScore> scores = from p in predictions
                                    group p by p.ApplicationUser.UserName into g
                                    select new PlayerScore {
                                        Username = g.Key,
                                        TotalPoints = g.Sum(p => p.Points),
                                        FirstName = g.Select(f => f.ApplicationUser.FirstName).FirstOrDefault(),
                                        Surname = g.Select(f => f.ApplicationUser.Surname).FirstOrDefault(),
                                        Guid = g.Select(f => f.ApplicationUser.Id).FirstOrDefault()
                                    };

            vm.PlayerScores = scores.ToList();

            return View("Index", vm);
        }
    }

}
