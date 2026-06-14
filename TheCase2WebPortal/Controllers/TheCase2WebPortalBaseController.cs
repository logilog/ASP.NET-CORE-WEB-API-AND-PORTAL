using Entities.CONFS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
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

        /// <summary>
        /// Çerezde saklanan JWT'yi API çağrıları için Bearer Authorization başlığına dönüştürür.
        /// </summary>
        protected Dictionary<string, string> AuthHeader()
            => new Dictionary<string, string>() { { "Authorization", $"Bearer {User.FindFirst("AuthToken")?.Value}" } };
    }
}
