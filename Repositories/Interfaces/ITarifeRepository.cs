using Entities.General;
using Entities.REPOSITORY;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ITarifeRepository
    {
        Task<MiddlewareResult<TarifeDTO>> Get(TarifeDTO TarifeDTO);
        Task<MiddlewareResult<List<TarifeDTO>>> GetList();
        public Task<MiddlewareResult<object>> Add(TarifeDTO tarifeDTO);

        Task<MiddlewareResult<object>> Update(TarifeDTO tarifeDTO);
        public bool Delete();
    }
}
