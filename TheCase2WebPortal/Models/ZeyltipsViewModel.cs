using Entities.BUSINESS;
using System.Collections.Generic;
using TheCase2WebPortal.Helpers;

namespace TheCase2WebPortal.Models
{
    public class ZeyltipsViewModel : ViewModelBase
    {
        public List<Zeyltips> ZeyltipsListesi { get; set; }
        public Zeyltips Zeyltips { get; set; }
    }
}
