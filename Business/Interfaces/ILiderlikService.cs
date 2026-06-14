using Entities.BUSINESS;
using Entities.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface ILiderlikService
    {
        Task<ResultModel<List<Liderlik>>> GetList();
    }
}
