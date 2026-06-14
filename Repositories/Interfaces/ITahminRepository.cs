using Entities.General;
using Entities.REPOSITORY;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ITahminRepository
    {
        Task<MiddlewareResult<List<TahminDTO>>> GetListByKullanici(int kullaniciId);
        Task<MiddlewareResult<object>> Add(TahminDTO tahminDTO);
        Task<MiddlewareResult<object>> Puanla(int macId, int evSkor, int depSkor);
    }
}
