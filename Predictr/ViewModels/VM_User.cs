using Predictr.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Predictr.ViewModels
{
    public class VM_User
    {
        public String Id { get; set; }
        public String FirstName { get; set; }
        public String Surname { get; set; }
        public String Email { get; set; }
        public List<Prediction> Predictions { get; set; }
        public int Points { get; set; }
    }
}
