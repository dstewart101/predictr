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
    public class PredictionsRepository : IPredictionRepository
    {
        private ApplicationDbContext _context;

        public PredictionsRepository(ApplicationDbContext context) {
            _context = context;
        }

        public Task SaveChanges() => _context.SaveChangesAsync();

        public Task<List<Prediction>> GetAll() => _context.Predictions.ToListAsync();

        public Task<List<Prediction>> GetPredictionsToUpdate(int id) => _context.Predictions.Where(p => p.FixtureId == id).ToListAsync();

        public Boolean PredictionExists(int id) => _context.Predictions.Any(p => p.Id == id);

        public void Delete(Prediction prediction) => _context.Predictions.Remove(prediction);

        public Task<Prediction> GetSinglePrediction(int id) => _context.Predictions.Include("Fixture").SingleOrDefaultAsync(p => p.Id == id);

        public void Add(Prediction prediction) => _context.Add(prediction);
        public void Update(Prediction prediction) => _context.Update(prediction);
    }
}
