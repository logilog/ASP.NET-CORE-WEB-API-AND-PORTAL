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
    public class TahminController : The_Case2BaseController
    {
        private readonly ITahminService _tahminService;
        public TahminController(ITahminService TahminService)
        {
            _tahminService = TahminService;
        }
        /// <summary>
        /// Oturum açan kullanıcının tüm tahminlerini (maç bilgisi ve kazanılan puan ile) döner.
        /// </summary>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<List<Tahmin>>> Listem()
        {
            return await _tahminService.GetListByKullanici(CurrentUserId());
        }
        /// <summary>
        /// Oturum açan kullanıcı adına maç skor tahmini kaydeder/günceller.
        /// Kullanıcı kimliği body'den değil, token'dan alınır.
        /// </summary>
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Tahminle([FromBody] Tahmin tahmin)
        {
            tahmin.KULLANICIID = CurrentUserId();
            return await _tahminService.Add(tahmin);
        }
    }
}
