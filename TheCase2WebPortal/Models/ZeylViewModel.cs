using Entities.BUSINESS;
using System.Collections.Generic;
using TheCase2WebPortal.Helpers;

namespace TheCase2WebPortal.Models
{
    public class ZeylViewModel : ViewModelBase
    {
        public List<Zeyl> ZeylListesi { get; set; }
        public Zeyl Zeyl { get; set; }
    }
}
