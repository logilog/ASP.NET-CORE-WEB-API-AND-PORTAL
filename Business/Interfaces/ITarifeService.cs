using Entities.BUSINESS;
using Entities.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ITarifeService
    {
        public Task<ResultModel<Tarife>> Get(Tarife tarife);
        public Task<ResultModel<List<Tarife>>> GetList();
        public Task<ResultModel<object>> Add(Tarife tarife);
        public Task<ResultModel<object>> Update(Tarife tarife);


    }
}
