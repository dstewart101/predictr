using System.Collections.Generic;
using Predictr.Models;

namespace Predictr.ViewModels
{
    public class VM_MyPredictr
    {
        public List<Fixture> UnPredictedFixtures { get; set; }
        public List<Prediction> Predictions { get; set; }

        public int Points { get; set; }
    }
}
