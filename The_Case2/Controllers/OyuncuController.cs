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
    public class OyuncuController : The_Case2BaseController
    {
        private readonly IOyuncuService _oyuncuService;
        public OyuncuController(IOyuncuService OyuncuService)
        {
            _oyuncuService = OyuncuService;
        }
        /// <summary>
        /// Belirtilen takımın kadrosunu döner.
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<List<Oyuncu>>> GetByTakim(int TakimId)
        {
            return await _oyuncuService.GetListByTakim(TakimId);
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<Oyuncu>> GetById(int Id)
        {
            return await _oyuncuService.Get(new Oyuncu() { ID = Id });
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Add([FromBody] Oyuncu oyuncu)
        {
            return await _oyuncuService.Add(oyuncu);
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Update([FromBody] Oyuncu oyuncu)
        {
            return await _oyuncuService.Update(oyuncu);
        }
    }
}
