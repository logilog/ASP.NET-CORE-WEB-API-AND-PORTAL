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
    public class TahminController : TheCase2WebPortalBaseController
    {
        private readonly IHttpClientFactoryService<List<Tahmin>> _tahminListService;
        private readonly IHttpClientFactoryService<Mac> _macService;
        private readonly IHttpClientFactoryService<object> _objectService;
        public TahminController(
            ILogger<TahminController> Logger,
            IOptions<ApiSettings> ApiSettings,
            IOptions<JwtOptions> JwtOptions,
            IHttpClientFactoryService<List<Tahmin>> TahminListService,
            IHttpClientFactoryService<Mac> MacService,
            IHttpClientFactoryService<object> ObjectService
            ) : base(Logger, ApiSettings, JwtOptions)
        {
            _tahminListService = TahminListService;
            _macService = MacService;
            _objectService = ObjectService;
        }

        public async Task<IActionResult> Liste()
        {
            var httpRequestRes = await _tahminListService.Execute(
                new RequestModel()
                {
                    BaseUrl = _apiSettings.BaseUrl,
                    Metod = "/Tahmin/Listem",
                    RequestParam = string.Empty,
                    MetodType = HttpMethod.Get,
                    HeaderList = AuthHeader()
                });

            return View(new TahminViewModel()
            {
                TahminListesi = httpRequestRes.Data,
                Message = httpRequestRes.Message,
                Success = httpRequestRes.Success
            });
        }

        [HttpGet]
        public async Task<IActionResult> Tahminle(int id)
        {
            var httpRequestRes = await _macService.Execute(
                new RequestModel()
                {
                    BaseUrl = _apiSettings.BaseUrl,
                    Metod = "/Mac/GetById",
                    RequestParam = $"id={id}",
                    MetodType = HttpMethod.Get,
                    HeaderList = AuthHeader()
                });

            var vm = new TahminViewModel()
            {
                Success = httpRequestRes.Success,
                Message = httpRequestRes.Message
            };
            var mac = httpRequestRes.Data;
            if (mac != null)
            {
                vm.Tahmin = new Tahmin()
                {
                    MACID = mac.ID,
                    EVSAHIBIAD = mac.EVSAHIBIAD,
                    DEPLASMANAD = mac.DEPLASMANAD,
                    MACTARIHI = mac.TARIH
                };
            }
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Tahminle(TahminViewModel tahminViewModel)
        {
            var payload = new Tahmin()
            {
                MACID = tahminViewModel.Tahmin.MACID,
                EVSAHIBISKOR = tahminViewModel.Tahmin.EVSAHIBISKOR,
                DEPLASMANSKOR = tahminViewModel.Tahmin.DEPLASMANSKOR
            };

            var httpRequestRes = await _objectService.Execute(
                new RequestModel()
                {
                    BaseUrl = _apiSettings.BaseUrl,
                    Metod = "/Tahmin/Tahminle",
                    RequestParam = JsonSerializer.Serialize(payload),
                    MetodType = HttpMethod.Post,
                    HeaderList = AuthHeader()
                });

            if (httpRequestRes.Success)
            {
                return RedirectToAction("Liste", "Tahmin");
            }

            tahminViewModel.Success = false;
            tahminViewModel.Message = httpRequestRes.Message;
            return View(tahminViewModel);
        }
    }
}
