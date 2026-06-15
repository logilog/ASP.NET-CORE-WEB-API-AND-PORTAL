using Business.Interfaces;
using Entities.BUSINESS;
using Entities.General;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace The_Case2.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OranController : The_Case2BaseController
    {
        private readonly IOranService _oranService;
        public OranController(IOranService OranService)
        {
            _oranService = OranService;
        }
        /// <summary>
        /// Bir maçın iddaa oranlarını döner.
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<Oran>> GetByMac(int MacId)
        {
            return await _oranService.GetByMac(MacId);
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Kaydet([FromBody] Oran oran)
        {
            return await _oranService.Kaydet(oran);
        }
    }
}
