using System;

namespace Predictr.Models
{
    public class Prediction
    {
        public int Id { get; set; }
        public int FixtureId { get; set; }
        public String ApplicationUserId { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public int? ModifierId { get; set; }
        public int Points { get; set; }

        public Boolean DoubleUp { get; set; }
        public Boolean Joker { get; set; }

        public virtual Fixture Fixture { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
