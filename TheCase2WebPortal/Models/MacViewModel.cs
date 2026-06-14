using Entities.BUSINESS;
using System.Collections.Generic;
using TheCase2WebPortal.Helpers;

namespace TheCase2WebPortal.Models
{
    public class MacViewModel : ViewModelBase
    {
        public List<Mac> MacListesi { get; set; }
        public Mac Mac { get; set; }
    }
}
