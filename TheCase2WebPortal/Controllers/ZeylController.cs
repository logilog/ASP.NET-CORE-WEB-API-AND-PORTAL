using Entities.BUSINESS;
using Entities.CONFS;
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
    public class ZeylController : TheCase2WebPortalBaseController
    {
        private readonly IHttpClientFactoryService<List<Zeyl>> _httpClientServiceImplementation;
        private readonly IHttpClientFactoryService<Zeyl> _httpClientServiceImplementation2;
        private readonly IHttpClientFactoryService<object> _httpClientServiceImplementation3;
        public ZeylController(
            ILogger<ZeylController> Logger,
            IOptions<ApiSettings> ApiSettings,
            IOptions<JwtOptions> JwtOptions,
            IHttpClientFactoryService<List<Zeyl>> HttpClientServiceImplementation,
            IHttpClientFactoryService<Zeyl> HttpClientServiceImplementation2,
             IHttpClientFactoryService<object> HttpClientServiceImplementation3
            ) : base(Logger, ApiSettings, JwtOptions)
        {
            _httpClientServiceImplementation = HttpClientServiceImplementation;
            _httpClientServiceImplementation2 = HttpClientServiceImplementation2;
            _httpClientServiceImplementation3 = HttpClientServiceImplementation3;
        }
        public async Task<IActionResult> Liste()
        {
            var httpRequestRes = await _httpClientServiceImplementation.Execute(
               new RequestModel()
               {
                   BaseUrl = _apiSettings.BaseUrl,
                   Metod = "/Zeyl/GetList",
                   RequestParam = string.Empty,
                   MetodType = HttpMethod.Get,
                   HeaderList = new Dictionary<string, string>() { { "Authorization", $"Bearer {User.FindFirst("AuthToken").Value}" } }

               });

            ZeylViewModel zeylViewModel = new ZeylViewModel()
            {
                ZeylListesi = httpRequestRes.Data,
                Message = httpRequestRes.Message,
                Success = httpRequestRes.Success
            };
            return View(zeylViewModel);
        }
        public IActionResult Ekleme()
        {
            return View();
        }
        public async Task<IActionResult> Guncelleme(int id)
        {
            var httpRequestRes = await _httpClientServiceImplementation2.Execute(
                  new RequestModel()
                  {
                      BaseUrl = _apiSettings.BaseUrl,
                      Metod = "/Zeyl/GetById",
                      RequestParam = $"id={id}",
                      MetodType = HttpMethod.Get,
                      HeaderList = new Dictionary<string, string>() { { "Authorization", $"Bearer {User.FindFirst("AuthToken").Value}" } }

                  });
            ZeylViewModel zeylViewModel = new ZeylViewModel()

            {
                Zeyl = httpRequestRes.Data,
                Message = httpRequestRes.Message,
                Success = httpRequestRes.Success
            };
            return View(zeylViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Ekleme(ZeylViewModel zeylViewModel)
        {
            var httpRequestRes = await _httpClientServiceImplementation3.Execute(
                 new RequestModel()
                 {
                     BaseUrl = _apiSettings.BaseUrl,
                     Metod = "/Zeyl/Add",
                     RequestParam = JsonSerializer.Serialize(zeylViewModel.Zeyl),
                     MetodType = HttpMethod.Post,
                     HeaderList = new Dictionary<string, string>() { { "Authorization", $"Bearer {User.FindFirst("AuthToken").Value}" } }

                 });



            if (httpRequestRes.Success)//gelen kayıt db ye başarılı kaydedilmiş ise listeme ekrananıa git
            {
                return RedirectToAction("Liste", "Zeyl");
            }
            else
            {
                zeylViewModel.Message = httpRequestRes.Message;
                return View(zeylViewModel);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Guncelleme(ZeylViewModel zeylViewModel)
        {
            var httpRequestRes = await _httpClientServiceImplementation3.Execute(
                new RequestModel()
                {
                    BaseUrl = _apiSettings.BaseUrl,
                    Metod = "/Zeyl/Update",
                    RequestParam = JsonSerializer.Serialize(zeylViewModel.Zeyl),
                    MetodType = HttpMethod.Post,
                    HeaderList = new Dictionary<string, string>() { { "Authorization", $"Bearer {User.FindFirst("AuthToken").Value}" } }

                });



            if (httpRequestRes.Success)//gelen kayıt db ye başarılı kaydedilmiş ise listeme ekrananıa git
            {
                return RedirectToAction("Liste", "Zeyl");
            }
            else
            {
                zeylViewModel.Message = httpRequestRes.Message;
                return View(zeylViewModel);
            }
        }
    }
}
