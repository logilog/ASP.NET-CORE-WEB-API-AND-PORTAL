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
    public class ZeyltipsController : The_Case2BaseController
    {

        private readonly IZeyltipsService _zeyltipsService;
        public ZeyltipsController(IZeyltipsService ZeyltipsService)
        {

            _zeyltipsService = ZeyltipsService;
        }
        /// <summary>
        /// person liste bilgisini döner
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<List<Zeyltips>>> GetList()
        {
            ResultModel<List<Zeyltips>> ResultList = await _zeyltipsService.GetList();

            return ResultList;
        }
        /// <summary>
        /// Gönderilen Id bilgisine göre person detay bilgilerini getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<Zeyltips>> Get([FromBody] Zeyltips zeyltips)
        {
            ResultModel<Zeyltips> Result = await _zeyltipsService.Get(zeyltips);

            return Result;
        }
        /// <summary>
        /// Gönderilen Id bilgisine göre person detay bilgilerini getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<Zeyltips>> GetById(int Id)
        {
            ResultModel<Zeyltips> Result = await _zeyltipsService.Get(new Zeyltips() { ID = Id });

            return Result;
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Add([FromBody] Zeyltips zeyltips)
        {
            ResultModel<object> Result = await _zeyltipsService.Add(zeyltips);

            return Result;

        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Update(Zeyltips zeyltips)
        {
            ResultModel<object> Result = await _zeyltipsService.Update(zeyltips);

            return Result;
        }

    }
}