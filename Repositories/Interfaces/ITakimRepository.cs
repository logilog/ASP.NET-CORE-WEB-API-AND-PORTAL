using Entities.General;
using Entities.REPOSITORY;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ITakimRepository
    {
        Task<MiddlewareResult<TakimDTO>> Get(TakimDTO takimDTO);
        Task<MiddlewareResult<List<TakimDTO>>> GetList();
        Task<MiddlewareResult<object>> Add(TakimDTO takimDTO);
        Task<MiddlewareResult<object>> Update(TakimDTO takimDTO);
    }
}
