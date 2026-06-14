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
    public class UyelikController : The_Case2BaseController
    {
        private readonly IUyelikService _uyelikService;
        public UyelikController(IUyelikService UyelikService)
        {
            _uyelikService = UyelikService;
        }
        /// <summary>
        /// Satın alınabilir üyelik paketlerini (FREE / VIP / PRO) döner.
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<List<UyelikPaket>>> Paketler()
        {
            return await _uyelikService.Paketler();
        }
        /// <summary>
        /// Oturum açan kullanıcının aktif üyelik durumunu döner.
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<Uyelik>> Durumum()
        {
            return await _uyelikService.Durum(CurrentUserId());
        }
        /// <summary>
        /// Oturum açan kullanıcı adına seçilen üyelik paketini satın alır.
        /// Ücret katalogdan alınır; istemciden gelen tutara güvenilmez.
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> SatinAl([FromBody] Uyelik uyelik)
        {
            uyelik.KULLANICIID = CurrentUserId();
            return await _uyelikService.SatinAl(uyelik);
        }
    }
}
