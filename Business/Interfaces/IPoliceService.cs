using Entities.BUSINESS;
using Entities.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IPoliceService
    {
        public Task<ResultModel<Police>> Get(Police police);
        public Task<ResultModel<List<Police>>> GetList();
        public Task<ResultModel<object>> Add(Police police);
        public Task<ResultModel<object>> Update(Police police);

    }
}
