using Entities.BUSINESS;
using Entities.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IGolService
    {
        Task<ResultModel<List<Gol>>> MacGollari(int macId);
        Task<ResultModel<List<GolKrali>>> GolKrali();
        Task<ResultModel<object>> Add(Gol gol);
    }
}
