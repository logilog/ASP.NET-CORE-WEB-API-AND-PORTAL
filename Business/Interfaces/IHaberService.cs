using Entities.BUSINESS;
using Entities.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IHaberService
    {
        Task<ResultModel<Haber>> Get(Haber haber);
        Task<ResultModel<List<Haber>>> GetList();
        Task<ResultModel<object>> Add(Haber haber);
        Task<ResultModel<object>> Update(Haber haber);
    }
}
