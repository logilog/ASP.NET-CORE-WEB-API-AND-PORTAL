using Entities.BUSINESS;
using System.Collections.Generic;
using TheCase2WebPortal.Helpers;

namespace TheCase2WebPortal.Models
{
    public class MacDetayViewModel : ViewModelBase
    {
        public Mac Mac { get; set; }
        public List<Gol> Goller { get; set; }
        public Oran Oran { get; set; }
    }
}
