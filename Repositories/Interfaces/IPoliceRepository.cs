using Entities.General;
using Entities.REPOSITORY;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Repositories.Interfaces
{
    public interface IPoliceRepository
    {
        Task<MiddlewareResult<PoliceDTO>> Get(PoliceDTO sigortaliDTO);
        Task<MiddlewareResult<List<PoliceDTO>>> GetList();
        public Task<MiddlewareResult<object>> Add(PoliceDTO policeDTO);

        Task<MiddlewareResult<object>> Update(PoliceDTO policeDTO);
        public bool Delete();
    }
}
