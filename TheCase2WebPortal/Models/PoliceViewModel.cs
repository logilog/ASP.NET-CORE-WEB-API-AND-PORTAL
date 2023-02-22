using Entities.BUSINESS;
using System.Collections.Generic;
using TheCase2WebPortal.Helpers;

namespace TheCase2WebPortal.Models
{
    public class PoliceViewModel : ViewModelBase
    {
        public List<Police> PoliceListesi { get; set; }
        public Police Police { get; set; }
    }
}
