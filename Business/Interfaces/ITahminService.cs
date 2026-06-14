using Entities.BUSINESS;
using Entities.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ITahminService
    {
        Task<ResultModel<List<Tahmin>>> GetListByKullanici(int kullaniciId);
        Task<ResultModel<object>> Add(Tahmin tahmin);
        Task<ResultModel<object>> MacSonuclariniPuanla(int macId, int evSkor, int depSkor);
    }
}
