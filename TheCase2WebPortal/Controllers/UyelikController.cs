using Entities.BUSINESS;
using Entities.CONFS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TheCase2WebPortal.Helpers;
using TheCase2WebPortal.Models;
using TheCase2WebPortal.Models.Helpers;

namespace TheCase2WebPortal.Controllers
{
    [Authorize]
    public class UyelikController : TheCase2WebPortalBaseController
    {
        private readonly IHttpClientFactoryService<List<UyelikPaket>> _paketListService;
        private readonly IHttpClientFactoryService<Uyelik> _uyelikService;
        private readonly IHttpClientFactoryService<object> _objectService;
        public UyelikController(
            ILogger<UyelikController> Logger,
            IOptions<ApiSettings> ApiSettings,
            IOptions<JwtOptions> JwtOptions,
            IHttpClientFactoryService<List<UyelikPaket>> PaketListService,
            IHttpClientFactoryService<Uyelik> UyelikService,
            IHttpClientFactoryService<object> ObjectService
            ) : base(Logger, ApiSettings, JwtOptions)
        {
            _paketListService = PaketListService;
            _uyelikService = UyelikService;
            _objectService = ObjectService;
        }

        public async Task<IActionResult> Paketler()
        {
            var paketRes = await _paketListService.Execute(
                new RequestModel()
                {
                    BaseUrl = _apiSettings.BaseUrl,
                    Metod = "/Uyelik/Paketler",
                    RequestParam = string.Empty,
                    MetodType = HttpMethod.Get,
                    HeaderList = AuthHeader()
                });

            var durumRes = await _uyelikService.Execute(
                new RequestModel()
                {
                    BaseUrl = _apiSettings.BaseUrl,
                    Metod = "/Uyelik/Durumum",
                    RequestParam = string.Empty,
                    MetodType = HttpMethod.Get,
                    HeaderList = AuthHeader()
                });

            return View(new UyelikViewModel()
            {
                PaketListesi = paketRes.Data,
                AktifUyelik = durumRes != null ? durumRes.Data : null,
                Message = TempData["UyelikMesaj"] as string,
                Success = paketRes.Success
            });
        }

        [HttpPost]
        public async Task<IActionResult> SatinAl(string paketKod)
        {
            var payload = new Uyelik() { PAKETKOD = paketKod };

            var httpRequestRes = await _objectService.Execute(
                new RequestModel()
                {
                    BaseUrl = _apiSettings.BaseUrl,
                    Metod = "/Uyelik/SatinAl",
                    RequestParam = JsonSerializer.Serialize(payload),
                    MetodType = HttpMethod.Post,
                    HeaderList = AuthHeader()
                });

            TempData["UyelikMesaj"] = httpRequestRes.Message;
            return RedirectToAction("Paketler", "Uyelik");
        }
    }
}
