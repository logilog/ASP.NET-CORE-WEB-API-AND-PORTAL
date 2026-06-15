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
    public class HaberController : TheCase2WebPortalBaseController
    {
        private readonly IHttpClientFactoryService<List<Haber>> _listService;
        private readonly IHttpClientFactoryService<Haber> _haberService;
        public HaberController(
            ILogger<HaberController> Logger,
            IOptions<ApiSettings> ApiSettings,
            IOptions<JwtOptions> JwtOptions,
            IHttpClientFactoryService<List<Haber>> ListService,
            IHttpClientFactoryService<Haber> HaberService
            ) : base(Logger, ApiSettings, JwtOptions)
        {
            _listService = ListService;
            _haberService = HaberService;
        }

        public async Task<IActionResult> Liste()
        {
            var res = await _listService.Execute(new RequestModel()
            {
                BaseUrl = _apiSettings.BaseUrl,
                Metod = "/Haber/GetList",
                RequestParam = string.Empty,
                MetodType = HttpMethod.Get,
                HeaderList = AuthHeader()
            });
            return View(new HaberViewModel()
            {
                HaberListesi = res.Data,
                Message = res.Message,
                Success = res.Success
            });
        }

        public async Task<IActionResult> Detay(int id)
        {
            var res = await _haberService.Execute(new RequestModel()
            {
                BaseUrl = _apiSettings.BaseUrl,
                Metod = "/Haber/GetById",
                RequestParam = $"id={id}",
                MetodType = HttpMethod.Get,
                HeaderList = AuthHeader()
            });
            return View(new HaberViewModel()
            {
                Haber = res != null ? res.Data : null,
                Message = res != null ? res.Message : null,
                Success = res != null && res.Success
            });
        }
    }
}
