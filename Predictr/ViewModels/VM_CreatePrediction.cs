﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Predictr.ViewModels
{
    public class VM_CreatePrediction
    {
        public String HomeTeam { get; set; }
        public String AwayTeam { get; set; }

        [Required]
        [Range(0, 100)]
        public int HomeScore { get; set; }

        [Required]
        [Range(0, 100)]
        public int AwayScore { get; set; }
    }
}
