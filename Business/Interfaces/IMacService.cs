using Entities.BUSINESS;
using Entities.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IMacService
    {
        Task<ResultModel<Mac>> Get(Mac mac);
        Task<ResultModel<List<Mac>>> GetList();
        Task<ResultModel<object>> Add(Mac mac);
        Task<ResultModel<object>> Update(Mac mac);
        Task<ResultModel<object>> SonucGir(Mac mac);
        Task<ResultModel<List<Mac>>> Canli();
        Task<ResultModel<List<Mac>>> Gunluk(string tarih);
        Task<ResultModel<object>> CanliGuncelle(Mac mac);
    }
}
