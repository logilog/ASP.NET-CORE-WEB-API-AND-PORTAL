using Entities.BUSINESS;
using Entities.General;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IOranService
    {
        Task<ResultModel<Oran>> GetByMac(int macId);
        Task<ResultModel<object>> Kaydet(Oran oran);
    }
}
