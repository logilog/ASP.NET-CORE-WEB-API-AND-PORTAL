using Entities.BUSINESS;
using System.Collections.Generic;
using TheCase2WebPortal.Helpers;

namespace TheCase2WebPortal.Models
{
    public class HaberViewModel : ViewModelBase
    {
        public List<Haber> HaberListesi { get; set; }
        public Haber Haber { get; set; }
    }
}
