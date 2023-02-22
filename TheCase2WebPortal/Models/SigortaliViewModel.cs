using Entities.BUSINESS;
using System.Collections.Generic;
using TheCase2WebPortal.Helpers;

namespace TheCase2WebPortal.Models
{
    public class SigortaliViewModel : ViewModelBase
    {
        public List<sigortali> SigortaliListesi { get; set; }
        public sigortali sigortali{ get; set; }
    }
}
