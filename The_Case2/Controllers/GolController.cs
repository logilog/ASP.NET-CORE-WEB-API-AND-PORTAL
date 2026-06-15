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
    public class GolController : The_Case2BaseController
    {
        private readonly IGolService _golService;
        public GolController(IGolService GolService)
        {
            _golService = GolService;
        }
        /// <summary>
        /// Bir maçın gollerini (maç olayları) döner.
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<List<Gol>>> MacGollari(int MacId)
        {
            return await _golService.MacGollari(MacId);
        }
        /// <summary>
        /// Gol krallığı tablosunu döner.
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<List<GolKrali>>> GolKrali()
        {
            return await _golService.GolKrali();
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Add([FromBody] Gol gol)
        {
            return await _golService.Add(gol);
        }
    }
}
