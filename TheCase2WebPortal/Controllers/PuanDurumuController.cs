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
    public class PuanDurumuController : TheCase2WebPortalBaseController
    {
        private readonly IHttpClientFactoryService<List<PuanDurumu>> _service;
        public PuanDurumuController(
            ILogger<PuanDurumuController> Logger,
            IOptions<ApiSettings> ApiSettings,
            IOptions<JwtOptions> JwtOptions,
            IHttpClientFactoryService<List<PuanDurumu>> Service
            ) : base(Logger, ApiSettings, JwtOptions)
        {
            _service = Service;
        }

        public async Task<IActionResult> Liste()
        {
            var res = await _service.Execute(new RequestModel()
            {
                BaseUrl = _apiSettings.BaseUrl,
                Metod = "/PuanDurumu/GetList",
                RequestParam = string.Empty,
                MetodType = HttpMethod.Get,
                HeaderList = AuthHeader()
            });
            return View(new PuanDurumuViewModel()
            {
                PuanDurumuListesi = res.Data,
                Message = res.Message,
                Success = res.Success
            });
        }
    }
}
