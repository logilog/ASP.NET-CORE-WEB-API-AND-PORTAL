using Entities.BUSINESS;
using Entities.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IZeylService
    {
        public Task<ResultModel<Zeyl>> Get(Zeyl zeyl);
        public Task<ResultModel<List<Zeyl>>> GetList();
        public Task<ResultModel<object>> Add(Zeyl zeyl);
        public Task<ResultModel<object>> Update(Zeyl zeyl);


    }
}
