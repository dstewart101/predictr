using System;

namespace Predictr.Models
{
    public class PlayerScore
    {
        public String Guid { get; set; }
        public String Username { get; set; }
        public String FirstName { get; set; }
        public String Surname { get; set; }
        public int TotalPoints { get; set; }
    }
}
