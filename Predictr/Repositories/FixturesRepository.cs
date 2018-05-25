using Microsoft.EntityFrameworkCore;
using Predictr.Data;
using Predictr.Interfaces;
using Predictr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Predictr.Repositories
{
    public class FixturesRepository : IFixtureRepository
    {
        private ApplicationDbContext _context;

        public FixturesRepository(ApplicationDbContext context) {
            _context = context;
        }

        public Task<List<Fixture>> GetAll() => _context.Fixtures.ToListAsync();

        public Task<Fixture> GetSingleFixture(int id) => _context.Fixtures.SingleOrDefaultAsync(f => f.Id == id);

        public void Add(Fixture fixture) => _context.Add(fixture);

        public Task SaveChanges() => _context.SaveChangesAsync();

        public void Delete(Fixture fixture) => _context.Fixtures.Remove(fixture);

        public Boolean FixtureExists(int id) => _context.Fixtures.Any(e => e.Id == id);
    }
}
