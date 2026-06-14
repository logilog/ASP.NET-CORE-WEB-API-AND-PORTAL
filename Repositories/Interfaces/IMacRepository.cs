using Entities.General;
using Entities.REPOSITORY;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IMacRepository
    {
        Task<MiddlewareResult<MacDTO>> Get(MacDTO macDTO);
        Task<MiddlewareResult<List<MacDTO>>> GetList();
        Task<MiddlewareResult<object>> Add(MacDTO macDTO);
        Task<MiddlewareResult<object>> Update(MacDTO macDTO);
        Task<MiddlewareResult<object>> SonucGuncelle(MacDTO macDTO);
    }
}
