using Entities.BUSINESS;
using System.Collections.Generic;
using TheCase2WebPortal.Helpers;

namespace TheCase2WebPortal.Models
{
    public class PersonViewModel : ViewModelBase
    {
        public List<person> PersonListesi { get; set; }
        public person Person { get; set; }
    }
}
