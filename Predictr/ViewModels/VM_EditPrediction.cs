using Predictr.Models;
using System;

namespace Predictr.ViewModels
{
    public class VM_EditPrediction
    {
        public String HomeTeam { get; set; }
        public String AwayTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }

        public virtual Fixture Fixture { get; set; }
    }
}
