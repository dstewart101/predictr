using Predictr.Models;
using System.Collections;

namespace Predictr.Interfaces
{
    public interface IPrediction
    {
        void Add(Prediction p);
        void Edit(Prediction p);
        IEnumerable GetPredictions();
    }
}
