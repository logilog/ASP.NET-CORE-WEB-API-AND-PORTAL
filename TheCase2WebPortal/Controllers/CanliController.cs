using Entities.BUSINESS;
using Entities.CONFS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TheCase2WebPortal.Helpers;
using TheCase2WebPortal.Models;
using TheCase2WebPortal.Models.Helpers;

namespace TheCase2WebPortal.Controllers
{
    [Authorize]
    public class CanliController : TheCase2WebPortalBaseController
    {
        private readonly IHttpClientFactoryService<List<Mac>> _macListService;
        public CanliController(
            ILogger<CanliController> Logger,
            IOptions<ApiSettings> ApiSettings,
            IOptions<JwtOptions> JwtOptions,
            IHttpClientFactoryService<List<Mac>> MacListService
            ) : base(Logger, ApiSettings, JwtOptions)
        {
            _macListService = MacListService;
        }

        public async Task<IActionResult> Index()
        {
            var res = await _macListService.Execute(new RequestModel()
            {
                BaseUrl = _apiSettings.BaseUrl,
                Metod = "/Mac/Canli",
                RequestParam = string.Empty,
                MetodType = HttpMethod.Get,
                HeaderList = AuthHeader()
            });
            return View(new MacViewModel()
            {
                MacListesi = res.Data,
                Message = res.Message,
                Success = res.Success
            });
        }

        public async Task<IActionResult> Program(string tarih)
        {
            if (string.IsNullOrEmpty(tarih))
                tarih = DateTime.Now.ToString("yyyy-MM-dd");

            var res = await _macListService.Execute(new RequestModel()
            {
                BaseUrl = _apiSettings.BaseUrl,
                Metod = "/Mac/Gunluk",
                RequestParam = $"tarih={tarih}",
                MetodType = HttpMethod.Get,
                HeaderList = AuthHeader()
            });
            ViewData["Tarih"] = tarih;
            return View(new MacViewModel()
            {
                MacListesi = res.Data,
                Message = res.Message,
                Success = res.Success
            });
        }
    }
}
