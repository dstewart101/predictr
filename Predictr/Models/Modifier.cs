using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Predictr.Models
{
    public class Modifier
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public decimal CorrectScoreMultiplier { get; set; }
        public decimal CorrectResultMultiplier { get; set; }
    }
}
