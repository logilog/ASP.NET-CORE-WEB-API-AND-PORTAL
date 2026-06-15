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
    public class HaberController : The_Case2BaseController
    {
        private readonly IHaberService _haberService;
        public HaberController(IHaberService HaberService)
        {
            _haberService = HaberService;
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<List<Haber>>> GetList()
        {
            return await _haberService.GetList();
        }
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<Haber>> GetById(int Id)
        {
            return await _haberService.Get(new Haber() { ID = Id });
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Add([FromBody] Haber haber)
        {
            return await _haberService.Add(haber);
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Update([FromBody] Haber haber)
        {
            return await _haberService.Update(haber);
        }
    }
}
