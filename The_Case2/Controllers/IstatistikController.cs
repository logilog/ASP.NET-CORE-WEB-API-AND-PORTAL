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
    public class IstatistikController : The_Case2BaseController
    {
        private readonly IIstatistikService _istatistikService;
        public IstatistikController(IIstatistikService IstatistikService)
        {
            _istatistikService = IstatistikService;
        }
        /// <summary>
        /// Turnuva geneli istatistik özetini döner.
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<Istatistik>> Get()
        {
            return await _istatistikService.Get();
        }
    }
}
