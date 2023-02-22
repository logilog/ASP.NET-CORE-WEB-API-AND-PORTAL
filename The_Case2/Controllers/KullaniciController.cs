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
    [Authorize(AuthenticationSchemes= JwtBearerDefaults.AuthenticationScheme)]
    public class KullaniciController : The_Case2BaseController
    {

        private readonly IKullaniciService _kullaniciService;
        public KullaniciController(IKullaniciService KullaniciService)
        {

            _kullaniciService = KullaniciService;
        }
        /// <summary>
        /// person liste bilgisini döner
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<List<kullanici>>> GetList()
        {
            ResultModel<List<kullanici>> ResultList  = await _kullaniciService.GetList();

            return ResultList;
        }
        /// <summary>
        /// Gönderilen Id bilgisine göre person detay bilgilerini getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<kullanici>> Get([FromBody] kullanici person)
        {
            ResultModel<kullanici> Result =await _kullaniciService.Get(person);

            return Result;
        }
        /// <summary>
        /// Gönderilen Id bilgisine göre person detay bilgilerini getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<kullanici>> GetById(int Id)
        {
            ResultModel<kullanici> Result = await _kullaniciService.Get(new kullanici() { ID = Id });

            return Result;
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Add([FromBody] kullanici person)
        {
            ResultModel<object> Result =await _kullaniciService.Add(new kullanici());

            return Result;

        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult Update(kullanici person)
        {
            return View();
        }

    }
}