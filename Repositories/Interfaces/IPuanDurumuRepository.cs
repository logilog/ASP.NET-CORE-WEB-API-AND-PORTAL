using Entities.General;
using Entities.REPOSITORY;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IPuanDurumuRepository
    {
        Task<MiddlewareResult<List<PuanDurumuDTO>>> GetList();
    }
}
