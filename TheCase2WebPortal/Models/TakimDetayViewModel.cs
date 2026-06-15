using Entities.BUSINESS;
using System.Collections.Generic;
using TheCase2WebPortal.Helpers;

namespace TheCase2WebPortal.Models
{
    public class TakimDetayViewModel : ViewModelBase
    {
        public Takim Takim { get; set; }
        public List<Oyuncu> Kadro { get; set; }
        public List<Mac> Maclar { get; set; }
    }
}
