using Entities.BUSINESS;
using Entities.CONFS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TheCase2WebPortal.Helpers;
using TheCase2WebPortal.Models;

namespace TheCase2WebPortal.Controllers
{
    public class SozlesmeController : TheCase2WebPortalBaseController
    {
        private readonly IHttpClientFactoryService<Police> _httpClientServiceImplementation;
        public SozlesmeController(
            ILogger<SozlesmeController> Logger,
            IOptions<ApiSettings> ApiSettings,
            IOptions<JwtOptions> JwtOptions
            ) : base(Logger, ApiSettings, JwtOptions)
        {
        }
        public IActionResult Liste()
        {
            return View();
        }
        public IActionResult Ekleme()
        {
            return View();
        }
        public IActionResult Guncelleme()
        {
            return View();
        }
    }
}
