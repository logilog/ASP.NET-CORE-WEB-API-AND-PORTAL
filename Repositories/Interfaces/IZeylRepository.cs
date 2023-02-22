using Entities.General;
using Entities.REPOSITORY;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IZeylRepository
    {
        Task<MiddlewareResult<ZeylDTO>> Get(ZeylDTO zeylDTO);
        Task<MiddlewareResult<List<ZeylDTO>>> GetList();
        public Task<MiddlewareResult<object>> Add(ZeylDTO zeylDTO);

        Task<MiddlewareResult<object>> Update(ZeylDTO zeylDTO);
        public bool Delete();
    }
}
