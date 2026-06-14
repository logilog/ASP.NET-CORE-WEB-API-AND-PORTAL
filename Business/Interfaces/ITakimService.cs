using Entities.BUSINESS;
using Entities.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ITakimService
    {
        Task<ResultModel<Takim>> Get(Takim takim);
        Task<ResultModel<List<Takim>>> GetList();
        Task<ResultModel<object>> Add(Takim takim);
        Task<ResultModel<object>> Update(Takim takim);
    }
}
