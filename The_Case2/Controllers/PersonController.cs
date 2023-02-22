using Business.Interfaces;
using Entities.BUSINESS;
using Entities.General;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace The_Case2.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonController : The_Case2BaseController
    {

        private readonly IPersonService _personService;
        public PersonController(IPersonService PersonService)
        {

            _personService = PersonService;
        }
        /// <summary>
        /// person liste bilgisini döner
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<List<person>>> GetList()
        {
            ResultModel<List<person>> ResultList = await _personService.GetList();

            return ResultList;
        }
        /// <summary>
        /// Gönderilen Id bilgisine göre person detay bilgilerini getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<person>> Get([FromBody] person person)
        {
            ResultModel<person> Result = await _personService.Get(person);

            return Result;
        }
        /// <summary>
        /// Gönderilen Id bilgisine göre person detay bilgilerini getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<person>> GetById(int Id)
        {
            ResultModel<person> Result = await _personService.Get(new person() {ID=Id });

            return Result;
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Add([FromBody] person person)
        {
            ResultModel<object> Result = await _personService.Add(person);

            return Result;

        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<object>> Update(person person)
        {
            ResultModel<object> Result = await _personService.Update(person);

            return Result;
        }

    }
}



/*namespace The_Case2.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class PersonController : The_Case2BaseController
    {

        private readonly IPersonService _personService;
        public PersonController(IPersonService personService)
        {

            _personService = personService;
        }
        /// <summary>
        /// person liste bilgisini döner
        /// </summary>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<List<person>>> GetList()
        {
            ResultModel<List<person>> ResultList = default;

            List<person> BusinessResult = await _personService.GetList();

            if (BusinessResult == null)
            {
                ResultList = new ResultModel<List<person>>($"Person liste bilgisi alınmadı.");
            }
            else
            {
                ResultList = new ResultModel<List<person>>(BusinessResult);
            }
            return ResultList;
        }
        /// <summary>
        /// Gönderilen Id bilgisine göre person detay bilgilerini getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<person>> Get([FromBody] person person)
        {
            ResultModel<person> Result = default;

            person BusinessResult = await _personService.Get(person);

            if (BusinessResult == null)
            {
                Result = new ResultModel<person>($"Person liste bilgisi alınmadı.");
            }
            else
            {
                Result = new ResultModel<person>(BusinessResult);
            }

            return Result;
        }
        /// <summary>
        /// Gönderilen Id bilgisine göre person detay bilgilerini getirir.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("[action]")]
        [HttpGet]
        public async Task<ResultModel<person>> GetById(int Id)
        {
            ResultModel<person> Result = default;

            person BusinessResult = await _personService.Get(new person() { ID = Id });

            if (BusinessResult == null)
            {
                Result = new ResultModel<person>($"Person liste bilgisi alınmadı.");
            }
            else
            {
                Result = new ResultModel<person>(BusinessResult);
            }

            return Result;
        }
        [Route("[action]")]
        [HttpPost]
        public async Task<ResultModel<person>> Add([FromBody] person person)
        {

            ResultModel<person> Result = default;

            person BusinessResult = await _personService.Add(new person());

            if (BusinessResult == null)
            {
                Result = new ResultModel<person>($"person kişi listeye eklenemedi.");
            }
            else
            {
                Result = new ResultModel<person>(BusinessResult);
            }

            return Result;

        }
        [Route("[action]")]
        [HttpPost]
        public IActionResult Update(person person)
        {
            return View();
        }

    }
} */