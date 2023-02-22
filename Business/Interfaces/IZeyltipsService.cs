using Entities.BUSINESS;
using Entities.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IZeyltipsService
    {
        public Task<ResultModel<Zeyltips>> Get(Zeyltips zeyltips);
        public Task<ResultModel<List<Zeyltips>>> GetList();
        public Task<ResultModel<object>> Add(Zeyltips zeyltips);
        public Task<ResultModel<object>> Update(Zeyltips zeyltips);


    }
}
