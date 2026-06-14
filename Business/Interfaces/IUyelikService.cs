using Entities.BUSINESS;
using Entities.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IUyelikService
    {
        Task<ResultModel<List<UyelikPaket>>> Paketler();
        Task<ResultModel<Uyelik>> Durum(int kullaniciId);
        Task<ResultModel<object>> SatinAl(Uyelik uyelik);
    }
}
