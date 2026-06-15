using Entities.BUSINESS;
using Entities.CONFS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TheCase2WebPortal.Helpers;
using TheCase2WebPortal.Models;
using TheCase2WebPortal.Models.Helpers;

namespace TheCase2WebPortal.Controllers
{
    [Authorize]
    public class TakimController : TheCase2WebPortalBaseController
    {
        private readonly IHttpClientFactoryService<Takim> _takimService;
        private readonly IHttpClientFactoryService<List<Oyuncu>> _oyuncuService;
        private readonly IHttpClientFactoryService<List<Mac>> _macListService;
        public TakimController(
            ILogger<TakimController> Logger,
            IOptions<ApiSettings> ApiSettings,
            IOptions<JwtOptions> JwtOptions,
            IHttpClientFactoryService<Takim> TakimService,
            IHttpClientFactoryService<List<Oyuncu>> OyuncuService,
            IHttpClientFactoryService<List<Mac>> MacListService
            ) : base(Logger, ApiSettings, JwtOptions)
        {
            _takimService = TakimService;
            _oyuncuService = OyuncuService;
            _macListService = MacListService;
        }

        public async Task<IActionResult> Detay(int id)
        {
            var takimRes = await _takimService.Execute(new RequestModel()
            {
                BaseUrl = _apiSettings.BaseUrl,
                Metod = "/Takim/GetById",
                RequestParam = $"id={id}",
                MetodType = HttpMethod.Get,
                HeaderList = AuthHeader()
            });
            var kadroRes = await _oyuncuService.Execute(new RequestModel()
            {
                BaseUrl = _apiSettings.BaseUrl,
                Metod = "/Oyuncu/GetByTakim",
                RequestParam = $"takimId={id}",
                MetodType = HttpMethod.Get,
                HeaderList = AuthHeader()
            });
            var macRes = await _macListService.Execute(new RequestModel()
            {
                BaseUrl = _apiSettings.BaseUrl,
                Metod = "/Mac/GetList",
                RequestParam = string.Empty,
                MetodType = HttpMethod.Get,
                HeaderList = AuthHeader()
            });

            var maclar = (macRes != null && macRes.Data != null)
                ? macRes.Data.Where(m => m.EVSAHIBIID == id || m.DEPLASMANID == id).ToList()
                : new List<Mac>();

            return View(new TakimDetayViewModel()
            {
                Takim = takimRes != null ? takimRes.Data : null,
                Kadro = kadroRes != null ? kadroRes.Data : null,
                Maclar = maclar,
                Success = takimRes != null && takimRes.Success,
                Message = takimRes != null ? takimRes.Message : null
            });
        }
    }
}
