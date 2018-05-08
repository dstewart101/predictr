using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Predictr.ViewModels
{
    public class VM_CreatePrediction
    {
        public String HomeTeam { get; set; }
        public String AwayTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public int FixtureId { get; set; }
    }
}
