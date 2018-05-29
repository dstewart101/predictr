using Predictr.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Predictr.Interfaces
{
    public interface IPredictionRepository
    {
        Task<List<Prediction>> GetAll();
        Task<Prediction> GetSinglePrediction(int id);
        void Add(Prediction prediction);
        void Delete(Prediction prediction);
        Boolean PredictionExists(int id);
        Task SaveChanges();
    }
}
