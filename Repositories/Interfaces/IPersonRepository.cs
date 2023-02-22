using Entities.General;
using Entities.REPOSITORY;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Repositories.Interfaces
{
    public interface IPersonRepository
    {
        Task<MiddlewareResult<PersonDTO>> Get(PersonDTO personDTO);
        Task<MiddlewareResult<List<PersonDTO>>> GetList();
        public Task<MiddlewareResult<object>> Add(PersonDTO personDTO );

        Task<MiddlewareResult<object>> Update(PersonDTO personDTO);
        public bool Delete();

    }
}
