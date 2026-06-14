using Entities.General;
using Entities.REPOSITORY;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IUyelikRepository
    {
        Task<MiddlewareResult<List<UyelikPaketDTO>>> PaketGetList();
        Task<MiddlewareResult<UyelikDTO>> GetByKullanici(int kullaniciId);
        Task<MiddlewareResult<object>> SatinAl(UyelikDTO uyelikDTO);
    }
}
