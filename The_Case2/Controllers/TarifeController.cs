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
    public class TarifeController : The_Case2BaseController
    {

        private readonly ITarifeService _tarifeService;
        public TarifeController(ITarifeService TarifeService)
        {

            _tarifeService = TarifeService;
        }
        /// <summary>
        /// person liste bilgisini döner
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<List<Tarife>>> GetList()
        {
            ResultModel<List<Tarife>> ResultList = await _tarifeService.GetList();

            return ResultList;
        }
        /// <summary>
        /// Gönderilen Id bilgisine göre person detay bilgilerini getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<Tarife>> Get([FromBody] Tarife tarife)
        {
            ResultModel<Tarife> Result = await _tarifeService.Get(tarife);

            return Result;
        }
        /// <summary>
        /// Gönderilen Id bilgisine göre person detay bilgilerini getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<Tarife>> GetById(int Id)
        {
            ResultModel<Tarife> Result = await _tarifeService.Get(new Tarife() { ID = Id });

            return Result;
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Add([FromBody] Tarife tarife)
        {
            ResultModel<object> Result = await _tarifeService.Add(tarife);

            return Result;

        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Update(Tarife tarife)
        {
            ResultModel<object> Result = await _tarifeService.Update(tarife);

            return Result;
        }

    }
}