using Entities.BUSINESS;
using Entities.CONFS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;
using TheCase2WebPortal.Helpers;
using TheCase2WebPortal.Models;
using TheCase2WebPortal.Models.Helpers;

namespace TheCase2WebPortal.Controllers
{
    [Authorize]
    public class IstatistikController : TheCase2WebPortalBaseController
    {
        private readonly IHttpClientFactoryService<Istatistik> _service;
        public IstatistikController(
            ILogger<IstatistikController> Logger,
            IOptions<ApiSettings> ApiSettings,
            IOptions<JwtOptions> JwtOptions,
            IHttpClientFactoryService<Istatistik> Service
            ) : base(Logger, ApiSettings, JwtOptions)
        {
            _service = Service;
        }

        public async Task<IActionResult> Index()
        {
            var res = await _service.Execute(new RequestModel()
            {
                BaseUrl = _apiSettings.BaseUrl,
                Metod = "/Istatistik/Get",
                RequestParam = string.Empty,
                MetodType = HttpMethod.Get,
                HeaderList = AuthHeader()
            });
            return View(new IstatistikViewModel()
            {
                Istatistik = res != null ? res.Data : null,
                Message = res != null ? res.Message : null,
                Success = res != null && res.Success
            });
        }
    }
}
