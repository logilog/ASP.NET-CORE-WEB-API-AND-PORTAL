using Entities.BUSINESS;
using Entities.General;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IKullaniciService
    {
        public Task<ResultModel<kullanici>> Get(kullanici person);
        public Task<ResultModel<List<kullanici>>> GetList();
        public Task<ResultModel<object>> Add(kullanici person);
        public Task<ResultModel<object>> Update(kullanici kullanici);

    }
}
