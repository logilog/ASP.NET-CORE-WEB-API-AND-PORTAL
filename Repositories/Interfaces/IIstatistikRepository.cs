using Entities.General;
using Entities.REPOSITORY;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IIstatistikRepository
    {
        Task<MiddlewareResult<IstatistikDTO>> Get();
    }
}
