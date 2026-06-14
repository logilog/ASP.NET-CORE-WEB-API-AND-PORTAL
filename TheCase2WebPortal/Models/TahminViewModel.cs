using Entities.BUSINESS;
using System.Collections.Generic;
using TheCase2WebPortal.Helpers;

namespace TheCase2WebPortal.Models
{
    public class TahminViewModel : ViewModelBase
    {
        public List<Tahmin> TahminListesi { get; set; }
        public Tahmin Tahmin { get; set; }
    }
}
