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
        public int HomeTeamId { get; set; }
        public int HomeScore { get; set; }
        public int AwayTeamId { get; set; }
        public int AwayTeamScore { get; set; }
        public int ResultId { get; set; }
    }
}
