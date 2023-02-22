using Entities.BUSINESS;
using System.Collections.Generic;
using TheCase2WebPortal.Helpers;

namespace TheCase2WebPortal.Models
{
    public class TarifeViewModel : ViewModelBase
    {
        public List<Tarife> TarifeListesi { get; set; }
        public Tarife Tarife { get; set; }
    }
}
