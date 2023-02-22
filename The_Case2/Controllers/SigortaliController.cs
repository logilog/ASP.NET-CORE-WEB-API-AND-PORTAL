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
    public class SigortaliController : The_Case2BaseController
    {

        private readonly ISigortaliService _sigortaliService;
        public SigortaliController(ISigortaliService SigortaliService)
        {

            _sigortaliService = SigortaliService;
        }
        /// <summary>
        /// person liste bilgisini döner
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<List<sigortali>>> GetList()
        {
            ResultModel<List<sigortali>> ResultList = await _sigortaliService.GetList();

            return ResultList;
        }
        /// <summary>
        /// Gönderilen Id bilgisine göre person detay bilgilerini getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<sigortali>> Get([FromBody] sigortali sigortali)
        {
            ResultModel<sigortali> Result = await _sigortaliService.Get(sigortali);

            return Result;
        }
        /// <summary>
        /// Gönderilen Id bilgisine göre person detay bilgilerini getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<sigortali>> GetById(int Id)
        {
            ResultModel<sigortali> Result = await _sigortaliService.Get(new sigortali() { ID = Id });

            return Result;
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Add([FromBody] sigortali sigortali)
        {
            ResultModel<object> Result = await _sigortaliService.Add(sigortali);

            return Result;

        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Update(sigortali sigortali)
        {
            ResultModel<object> Result = await _sigortaliService.Update(sigortali);

            return Result;
        }

    }
}
