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
    public class MacController : TheCase2WebPortalBaseController
    {
        private readonly IHttpClientFactoryService<List<Mac>> _macListService;
        public MacController(
            ILogger<MacController> Logger,
            IOptions<ApiSettings> ApiSettings,
            IOptions<JwtOptions> JwtOptions,
            IHttpClientFactoryService<List<Mac>> MacListService
            ) : base(Logger, ApiSettings, JwtOptions)
        {
            _macListService = MacListService;
        }

        public async Task<IActionResult> Liste()
        {
            var httpRequestRes = await _macListService.Execute(
                new RequestModel()
                {
                    BaseUrl = _apiSettings.BaseUrl,
                    Metod = "/Mac/GetList",
                    RequestParam = string.Empty,
                    MetodType = HttpMethod.Get,
                    HeaderList = AuthHeader()
                });

            return View(new MacViewModel()
            {
                MacListesi = httpRequestRes.Data,
                Message = httpRequestRes.Message,
                Success = httpRequestRes.Success
            });
        }
    }
}
