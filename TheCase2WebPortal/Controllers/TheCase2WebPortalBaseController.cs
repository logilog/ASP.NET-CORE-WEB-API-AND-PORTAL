using Entities.CONFS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TheCase2WebPortal.Models;

namespace TheCase2WebPortal.Controllers
{
    public class TheCase2WebPortalBaseController : Controller
    {
        protected readonly ILogger _logger;
        protected readonly ApiSettings _apiSettings;
        protected readonly JwtOptions _jwtOptions;
        public TheCase2WebPortalBaseController(
            ILogger Logger,
            IOptions<ApiSettings> ApiSettings,
            IOptions<JwtOptions> JwtOptions
            )
        {
            _logger = Logger;
            _apiSettings = ApiSettings.Value;
            _jwtOptions = JwtOptions.Value;
        }
    }
}
