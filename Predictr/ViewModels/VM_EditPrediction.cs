using System;
using System.ComponentModel.DataAnnotations;

namespace Predictr.ViewModels
{
    public class VM_EditPrediction
    {
        public String HomeTeam { get; set; }
        public String AwayTeam { get; set; }

        [Required(ErrorMessage = "Home Score Required")]
        [Range(0, 100, ErrorMessage = "Score should be in the range of 0 to 100")]
        public int HomeScore { get; set; }

        [Required(ErrorMessage = "Away Score Required")]
        [Range(0, 100, ErrorMessage = "Score should be in the range of 0 to 100")]
        public int AwayScore { get; set; }

        public Boolean DoubleUp { get; set; }
        public Boolean Joker { get; set; }
    }
}
