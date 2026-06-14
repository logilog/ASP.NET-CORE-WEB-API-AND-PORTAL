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
    public class TakimController : The_Case2BaseController
    {
        private readonly ITakimService _takimService;
        public TakimController(ITakimService TakimService)
        {
            _takimService = TakimService;
        }
        /// <summary>
        /// Dünya Kupası 2026'ya katılan tüm takımları döner.
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<List<Takim>>> GetList()
        {
            return await _takimService.GetList();
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<Takim>> GetById(int Id)
        {
            return await _takimService.Get(new Takim() { ID = Id });
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Add([FromBody] Takim takim)
        {
            return await _takimService.Add(takim);
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Update([FromBody] Takim takim)
        {
            return await _takimService.Update(takim);
        }
    }
}
