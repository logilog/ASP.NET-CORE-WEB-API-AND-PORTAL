using Entities.General;
using Entities.REPOSITORY;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Repositories.Interfaces
{
    public interface IKullaniciRepository
    {
        Task<MiddlewareResult<KullaniciDTO>> Get(KullaniciDTO kullaniciDTO);
        Task<MiddlewareResult<List<KullaniciDTO>>> GetList();
        public Task<MiddlewareResult<object>> Add(KullaniciDTO kullaniciDTO);

        Task<MiddlewareResult<object>> Update(KullaniciDTO kullaniciDTO);
        public bool Delete();

    }
}
