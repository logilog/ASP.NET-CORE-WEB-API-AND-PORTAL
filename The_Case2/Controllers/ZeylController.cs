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
    public class ZeylController : The_Case2BaseController
    {

        private readonly IZeylService _zeylService;
        public ZeylController(IZeylService ZeylService)
        {

            _zeylService = ZeylService;
        }
        /// <summary>
        /// person liste bilgisini döner
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<List<Zeyl>>> GetList()
        {
            ResultModel<List<Zeyl>> ResultList = await _zeylService.GetList();

            return ResultList;
        }
        /// <summary>
        /// Gönderilen Id bilgisine göre person detay bilgilerini getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<Zeyl>> Get([FromBody] Zeyl zeyl)
        {
            ResultModel<Zeyl> Result = await _zeylService.Get(zeyl);

            return Result;
        }
        /// <summary>
        /// Gönderilen Id bilgisine göre person detay bilgilerini getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<Zeyl>> GetById(int Id)
        {
            ResultModel<Zeyl> Result = await _zeylService.Get(new Zeyl() { ID = Id });

            return Result;
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Add([FromBody] Zeyl zeyl)
        {
            ResultModel<object> Result = await _zeylService.Add(zeyl);

            return Result;

        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Update(Zeyl zeyl)
        {
            ResultModel<object> Result = await _zeylService.Update(zeyl);

            return Result;
        }

    }
}