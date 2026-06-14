using Entities.BUSINESS;
using Entities.CONFS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TheCase2WebPortal.Helpers;
using TheCase2WebPortal.Models;
using TheCase2WebPortal.Models.Helpers;

namespace TheCase2WebPortal.Controllers
{
    [Authorize]
    public class LiderlikController : TheCase2WebPortalBaseController
    {
        private readonly IHttpClientFactoryService<List<Liderlik>> _liderlikListService;
        public LiderlikController(
            ILogger<LiderlikController> Logger,
            IOptions<ApiSettings> ApiSettings,
            IOptions<JwtOptions> JwtOptions,
            IHttpClientFactoryService<List<Liderlik>> LiderlikListService
            ) : base(Logger, ApiSettings, JwtOptions)
        {
            _liderlikListService = LiderlikListService;
        }

        public async Task<IActionResult> Liste()
        {
            var httpRequestRes = await _liderlikListService.Execute(
                new RequestModel()
                {
                    BaseUrl = _apiSettings.BaseUrl,
                    Metod = "/Liderlik/GetList",
                    RequestParam = string.Empty,
                    MetodType = HttpMethod.Get,
                    HeaderList = AuthHeader()
                });

            return View(new LiderlikViewModel()
            {
                LiderlikListesi = httpRequestRes.Data,
                Message = httpRequestRes.Message,
                Success = httpRequestRes.Success
            });
        }
    }
}
