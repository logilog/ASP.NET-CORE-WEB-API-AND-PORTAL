using Entities.General;
using Entities.REPOSITORY;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IZeyltipsRepository
    {
        Task<MiddlewareResult<ZeyltipsDTO>> Get(ZeyltipsDTO zeyltipsDTO);
        Task<MiddlewareResult<List<ZeyltipsDTO>>> GetList();
        public Task<MiddlewareResult<object>> Add(ZeyltipsDTO zeyltipsDTO);

        Task<MiddlewareResult<object>> Update(ZeyltipsDTO zeyltipsDTO);
        public bool Delete();
    }
}
