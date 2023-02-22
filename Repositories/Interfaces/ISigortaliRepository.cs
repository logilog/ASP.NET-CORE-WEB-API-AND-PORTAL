using Entities.General;
using Entities.REPOSITORY;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ISigortaliRepository
    {
        Task<MiddlewareResult<SigortaliDTO>> Get(SigortaliDTO SigortaliDTO);
        Task<MiddlewareResult<List<SigortaliDTO>>> GetList();
        public Task<MiddlewareResult<object>> Add(SigortaliDTO sigortaliDTO);

        Task<MiddlewareResult<object>> Update(SigortaliDTO sigortaliDTO);
        public bool Delete();
    }
}
