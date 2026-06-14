using Business.Interfaces;
using Entities.BUSINESS;
using Entities.General;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace The_Case2.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MacController : The_Case2BaseController
    {
        private readonly IMacService _macService;
        public MacController(IMacService MacService)
        {
            _macService = MacService;
        }
        /// <summary>
        /// Tüm maçları (fikstür) takım adları ile birlikte döner.
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<List<Mac>>> GetList()
        {
            return await _macService.GetList();
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<Mac>> GetById(int Id)
        {
            return await _macService.Get(new Mac() { ID = Id });
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Add([FromBody] Mac mac)
        {
            return await _macService.Add(mac);
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Update([FromBody] Mac mac)
        {
            return await _macService.Update(mac);
        }
        /// <summary>
        /// Maç sonucunu girer ve ilgili tüm tahminleri otomatik puanlar.
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> SonucGir([FromBody] Mac mac)
        {
            return await _macService.SonucGir(mac);
        }
    }
}
