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
    public class LiderlikController : The_Case2BaseController
    {
        private readonly ILiderlikService _liderlikService;
        public LiderlikController(ILiderlikService LiderlikService)
        {
            _liderlikService = LiderlikService;
        }
        /// <summary>
        /// Toplam puana göre sıralı liderlik tablosunu döner.
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<List<Liderlik>>> GetList()
        {
            return await _liderlikService.GetList();
        }
    }
}
