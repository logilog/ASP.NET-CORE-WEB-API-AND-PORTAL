using Entities.General;
using Entities.REPOSITORY;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IOranRepository
    {
        Task<MiddlewareResult<OranDTO>> GetByMac(int macId);
        Task<MiddlewareResult<object>> Kaydet(OranDTO oranDTO);
    }
}
