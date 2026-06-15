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
    public class GolKraliController : TheCase2WebPortalBaseController
    {
        private readonly IHttpClientFactoryService<List<GolKrali>> _service;
        public GolKraliController(
            ILogger<GolKraliController> Logger,
            IOptions<ApiSettings> ApiSettings,
            IOptions<JwtOptions> JwtOptions,
            IHttpClientFactoryService<List<GolKrali>> Service
            ) : base(Logger, ApiSettings, JwtOptions)
        {
            _service = Service;
        }

        public async Task<IActionResult> Liste()
        {
            var res = await _service.Execute(new RequestModel()
            {
                BaseUrl = _apiSettings.BaseUrl,
                Metod = "/Gol/GolKrali",
                RequestParam = string.Empty,
                MetodType = HttpMethod.Get,
                HeaderList = AuthHeader()
            });
            return View(new GolKraliViewModel()
            {
                GolKraliListesi = res.Data,
                Message = res.Message,
                Success = res.Success
            });
        }
    }
}
