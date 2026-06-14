using Entities.BUSINESS;
using System.Collections.Generic;
using TheCase2WebPortal.Helpers;

namespace TheCase2WebPortal.Models
{
    public class UyelikViewModel : ViewModelBase
    {
        public List<UyelikPaket> PaketListesi { get; set; }
        public Uyelik AktifUyelik { get; set; }
    }
}
