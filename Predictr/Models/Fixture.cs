using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Predictr.Models
{
    public class Fixture
    {
        public int Id { get; set; }

        [DisplayFormat(DataFormatString = "{0:d MMM H:mm}")]
        [Display(Name = "Date/Time")]
        public DateTime FixtureDateTime { get; set; }

        [Display(Name = "Home")]
        public String Home { get; set; }

        [Display(Name = "Home Score")]
        public int? HomeScore { get; set; }

        [Display(Name = "Away")]
        public String Away { get; set; }

        [Display(Name = "Away Score")]
        public int? AwayScore { get; set; }
        public String Result { get; set; }

        [Display(Name = "Group/Stage")]
        public String Group { get; set; }
    }
}
