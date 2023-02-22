using Entities.API;
using Entities.CONFS;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using TheCase2WebPortal.Helpers;
using TheCase2WebPortal.Models;
using TheCase2WebPortal.Models.Helpers;

namespace TheCase2WebPortal.Controllers
{
    public class LoginController : TheCase2WebPortalBaseController
    {
        private readonly IHttpClientFactoryService<LoginResult> _httpClientServiceImplementation;

        public LoginController(
            ILogger<LoginController> Logger,
            IOptions<ApiSettings> ApiSettings,
            IOptions<JwtOptions> JwtOptions,
            IHttpClientFactoryService<LoginResult> HttpClientServiceImplementation
            ) : base(Logger, ApiSettings, JwtOptions)
        {
            _httpClientServiceImplementation = HttpClientServiceImplementation;

        }

        public IActionResult Index([FromQuery] string ReturnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (string.IsNullOrEmpty(ReturnUrl))
                    return RedirectToAction("Liste", "Sozlesme");
                else
                    return Redirect(ReturnUrl);
            }
            else

                return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index([FromQuery] string ReturnUrl, [FromForm] LoginViewModel LoginModel)
        {
            var httpRequestRes = await _httpClientServiceImplementation.Execute(
                new RequestModel()
                {
                    BaseUrl = _apiSettings.BaseUrl,
                    Metod = "/Login/Token",
                    RequestParam = JsonSerializer.Serialize(LoginModel),
                });

            if (httpRequestRes.Success)
            {
                var claims = JwtExtension.JwtTokenDecode(httpRequestRes.Data.Token, _jwtOptions.Key, _jwtOptions.Issuer, _jwtOptions.Audience);

                var claims1 = new List<Claim>();
                claims1.Add(new Claim("AuthToken", httpRequestRes.Data.Token));

                var ClaimsIdentity = new ClaimsIdentity(claims1, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal Principal = new ClaimsPrincipal(ClaimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, Principal);
                if (string.IsNullOrEmpty(ReturnUrl))
                    return Redirect("Sozlesme/Liste");
                else
                    return Redirect(ReturnUrl);
            }
            LoginModel.Message = httpRequestRes.Message;
            return View(LoginModel);
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Login", null);
        }
    }
}
