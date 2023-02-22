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
    public class SigortaliController : TheCase2WebPortalBaseController
    {
        private readonly IHttpClientFactoryService<List<sigortali>> _httpClientServiceImplementation;
        private readonly IHttpClientFactoryService<sigortali> _httpClientServiceImplementation2;
        private readonly IHttpClientFactoryService<object> _httpClientServiceImplementation3;
        public SigortaliController(
            ILogger<SigortaliController> Logger,
            IOptions<ApiSettings> ApiSettings,
            IOptions<JwtOptions> JwtOptions,
            IHttpClientFactoryService<List<sigortali>> HttpClientServiceImplementation,
            IHttpClientFactoryService<sigortali> HttpClientServiceImplementation2,
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
                   Metod = "/sigortali/GetList",
                   RequestParam = string.Empty,
                   MetodType = HttpMethod.Get,
                   HeaderList = new Dictionary<string, string>() { { "Authorization", $"Bearer {User.FindFirst("AuthToken").Value}" } }

               });

            SigortaliViewModel sigortaliViewModel = new SigortaliViewModel()
            {
                SigortaliListesi = httpRequestRes.Data,
                Message = httpRequestRes.Message,
                Success = httpRequestRes.Success
            };
            return View(sigortaliViewModel);
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
                      Metod = "/sigortali/GetById",
                      RequestParam = $"id={id}",
                      MetodType = HttpMethod.Get,
                      HeaderList = new Dictionary<string, string>() { { "Authorization", $"Bearer {User.FindFirst("AuthToken").Value}" } }

                  });
            SigortaliViewModel sigortaliViewModel = new SigortaliViewModel()

            {
                sigortali = httpRequestRes.Data,
                Message = httpRequestRes.Message,
                Success = httpRequestRes.Success
            };
            return View(sigortaliViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Ekleme(SigortaliViewModel sigortaliViewModel)
        {
            var httpRequestRes = await _httpClientServiceImplementation3.Execute(
                 new RequestModel()
                 {
                     BaseUrl = _apiSettings.BaseUrl,
                     Metod = "/sigortali/Add",
                     RequestParam = JsonSerializer.Serialize(sigortaliViewModel.sigortali),
                     MetodType = HttpMethod.Post,
                     HeaderList = new Dictionary<string, string>() { { "Authorization", $"Bearer {User.FindFirst("AuthToken").Value}" } }

                 });



            if (httpRequestRes.Success)//gelen kayıt db ye başarılı kaydedilmiş ise listeme ekrananıa git
            {
                return RedirectToAction("Liste", "sigortali");
            }
            else
            {
                sigortaliViewModel.Message = httpRequestRes.Message;
                return View(sigortaliViewModel);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Guncelleme(SigortaliViewModel sigortaliViewModel)
        {
            var httpRequestRes = await _httpClientServiceImplementation3.Execute(
                new RequestModel()
                {
                    BaseUrl = _apiSettings.BaseUrl,
                    Metod = "/sigortali/Update",
                    RequestParam = JsonSerializer.Serialize(sigortaliViewModel.sigortali),
                    MetodType = HttpMethod.Post,
                    HeaderList = new Dictionary<string, string>() { { "Authorization", $"Bearer {User.FindFirst("AuthToken").Value}" } }

                });



            if (httpRequestRes.Success)//gelen kayıt db ye başarılı kaydedilmiş ise listeme ekrananıa git
            {
                return RedirectToAction("Liste", "sigortali");
            }
            else
            {
                sigortaliViewModel.Message = httpRequestRes.Message;
                return View(sigortaliViewModel);
            }
        }
    }
}
