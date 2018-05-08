﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Predictr.Models
{
    public class Prediction
    {
        public int Id { get; set; }
        public int FixtureId { get; set; }
        public String ApplicationUser { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public int? ModifierId { get; set; }
        public int Points { get; set; }

        public virtual Fixture Fixture { get; set; }
    }
}
