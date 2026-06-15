using Entities.General;
using Entities.REPOSITORY;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IHaberRepository
    {
        Task<MiddlewareResult<HaberDTO>> Get(HaberDTO haberDTO);
        Task<MiddlewareResult<List<HaberDTO>>> GetList();
        Task<MiddlewareResult<object>> Add(HaberDTO haberDTO);
        Task<MiddlewareResult<object>> Update(HaberDTO haberDTO);
    }
}
