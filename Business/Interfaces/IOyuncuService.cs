using Entities.BUSINESS;
using Entities.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IOyuncuService
    {
        Task<ResultModel<Oyuncu>> Get(Oyuncu oyuncu);
        Task<ResultModel<List<Oyuncu>>> GetListByTakim(int takimId);
        Task<ResultModel<object>> Add(Oyuncu oyuncu);
        Task<ResultModel<object>> Update(Oyuncu oyuncu);
    }
}
