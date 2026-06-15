using Entities.General;
using Entities.REPOSITORY;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IGolRepository
    {
        Task<MiddlewareResult<List<GolDTO>>> GetByMac(int macId);
        Task<MiddlewareResult<List<GolKraliDTO>>> GolKraliList();
        Task<MiddlewareResult<object>> Add(GolDTO golDTO);
    }
}
