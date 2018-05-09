using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Predictr.ViewModels
{
    public class VM_EditFixture
    {
        [Display(Name = "Date/Time")]
        public DateTime FixtureDateTime { get; set; }

        [Display(Name = "Home Team")]
        public String Home { get; set; }

        [Display(Name = "Home Score")]
        [Range(0,50)]
        public int? HomeScore { get; set; }

        [Display(Name = "Away Team")]
        public String Away { get; set; }

        [Display(Name = "Away Score")]
        [Range(0, 50)]
        public int? AwayScore { get; set; }

        [Display(Name = "Group")]
        public String Group { get; set; }
    }
}
