using Entities.General;
using Entities.REPOSITORY;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IOyuncuRepository
    {
        Task<MiddlewareResult<OyuncuDTO>> Get(OyuncuDTO oyuncuDTO);
        Task<MiddlewareResult<List<OyuncuDTO>>> GetListByTakim(int takimId);
        Task<MiddlewareResult<object>> Add(OyuncuDTO oyuncuDTO);
        Task<MiddlewareResult<object>> Update(OyuncuDTO oyuncuDTO);
    }
}
