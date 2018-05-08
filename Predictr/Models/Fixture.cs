using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Predictr.Models
{
    public class Fixture
    {
        public int Id { get; set; }
        public DateTime FixtureDateTime { get; set; }
        public String Home { get; set; }
        public int? HomeScore { get; set; }
        public String Away { get; set; }
        public int? AwayTeamScore { get; set; }
        public String Result { get; set; }
        public String Group { get; set; }
    }
}
