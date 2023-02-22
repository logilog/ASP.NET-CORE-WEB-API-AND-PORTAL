using Entities.BUSINESS;
using Entities.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IPersonService
    {
        public Task<ResultModel<person>> Get(person person);
        public Task<ResultModel<List<person>>> GetList();
        public Task<ResultModel<object>> Add(person person);
        public Task<ResultModel<object>> Update(person person);


    }
}
