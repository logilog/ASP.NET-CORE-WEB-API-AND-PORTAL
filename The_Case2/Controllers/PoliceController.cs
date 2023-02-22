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

    public class PoliceController : The_Case2BaseController
    {

        private readonly IPoliceService _policeService;
        public PoliceController(IPoliceService policeService)
        {

            _policeService = policeService;
        }
        /// <summary>
        /// police liste bilgisini döner
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<List<Police>>> GetList()
        {
            ResultModel<List<Police>> ResultList = await _policeService.GetList();

            return ResultList;
        }
        /// <summary>
        /// Gönderilen Id bilgisine göre police detay bilgilerini getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<Police>> Get([FromBody] Police police)
        {
            ResultModel<Police> Result = await _policeService.Get(police);

            return Result;
        }
        /// <summary>
        /// Gönderilen Id bilgisine göre Police detay bilgilerini getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<Police>> GetById(int Id)
        {
            ResultModel<Police> Result = await _policeService.Get(new Police() { ID = Id });

            return Result;
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Add([FromBody] Police police)
        {

            ResultModel<object> Result = await _policeService.Add(police);

            return Result;

        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Update(Police police)
        {
            ResultModel<object> Result = await _policeService.Update(police);

            return Result;
        }

    }
}