using Entities.BUSINESS;
using Entities.General;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IIstatistikService
    {
        Task<ResultModel<Istatistik>> Get();
    }
}
