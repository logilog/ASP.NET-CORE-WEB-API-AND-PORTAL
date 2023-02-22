using Entities.BUSINESS;
using Entities.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ISigortaliService
    {
        public Task<ResultModel<sigortali>> Get(sigortali sigortali);
        public Task<ResultModel<List<sigortali>>> GetList();
        public Task<ResultModel<object>> Add(sigortali sigortali);
        public Task<ResultModel<object>> Update(sigortali sigortali);



    }
}
