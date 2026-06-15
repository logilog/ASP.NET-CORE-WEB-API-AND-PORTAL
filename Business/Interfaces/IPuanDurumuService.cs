using Entities.BUSINESS;
using Entities.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IPuanDurumuService
    {
        Task<ResultModel<List<PuanDurumu>>> GetList();
    }
}
