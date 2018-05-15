using Predictr.Models;
using System.Collections.Generic;

namespace Predictr.ViewModels
{
    public class VM_Admin
    {
        public List<Fixture> Fixtures { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}
