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
        private readonly IHttpClientFactoryService<Mac> _macService;
        private readonly IHttpClientFactoryService<List<Gol>> _golService;
        private readonly IHttpClientFactoryService<Oran> _oranService;
        public MacController(
            ILogger<MacController> Logger,
            IOptions<ApiSettings> ApiSettings,
            IOptions<JwtOptions> JwtOptions,
            IHttpClientFactoryService<List<Mac>> MacListService,
            IHttpClientFactoryService<Mac> MacService,
            IHttpClientFactoryService<List<Gol>> GolService,
            IHttpClientFactoryService<Oran> OranService
            ) : base(Logger, ApiSettings, JwtOptions)
        {
            _macListService = MacListService;
            _macService = MacService;
            _golService = GolService;
            _oranService = OranService;
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

        public async Task<IActionResult> Detay(int id)
        {
            var macRes = await _macService.Execute(new RequestModel()
            {
                BaseUrl = _apiSettings.BaseUrl,
                Metod = "/Mac/GetById",
                RequestParam = $"id={id}",
                MetodType = HttpMethod.Get,
                HeaderList = AuthHeader()
            });
            var golRes = await _golService.Execute(new RequestModel()
            {
                BaseUrl = _apiSettings.BaseUrl,
                Metod = "/Gol/MacGollari",
                RequestParam = $"macId={id}",
                MetodType = HttpMethod.Get,
                HeaderList = AuthHeader()
            });
            var oranRes = await _oranService.Execute(new RequestModel()
            {
                BaseUrl = _apiSettings.BaseUrl,
                Metod = "/Oran/GetByMac",
                RequestParam = $"macId={id}",
                MetodType = HttpMethod.Get,
                HeaderList = AuthHeader()
            });

            return View(new MacDetayViewModel()
            {
                Mac = macRes != null ? macRes.Data : null,
                Goller = golRes != null ? golRes.Data : null,
                Oran = oranRes != null ? oranRes.Data : null,
                Success = macRes != null && macRes.Success,
                Message = macRes != null ? macRes.Message : null
            });
        }
    }
}
