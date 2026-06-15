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
    public class PuanDurumuController : The_Case2BaseController
    {
        private readonly IPuanDurumuService _puanDurumuService;
        public PuanDurumuController(IPuanDurumuService PuanDurumuService)
        {
            _puanDurumuService = PuanDurumuService;
        }
        /// <summary>
        /// Tüm grupların puan durumunu (oynanmış maçlardan hesaplanmış) döner.
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<List<PuanDurumu>>> GetList()
        {
            return await _puanDurumuService.GetList();
        }
    }
}
