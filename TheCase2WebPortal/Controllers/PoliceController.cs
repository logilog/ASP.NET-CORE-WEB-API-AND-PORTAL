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
    public class PoliceController : TheCase2WebPortalBaseController
    {
        private readonly IHttpClientFactoryService<List<Police>> _httpClientServiceImplementation;
        private readonly IHttpClientFactoryService<Police> _httpClientServiceImplementation2;
        private readonly IHttpClientFactoryService<object> _httpClientServiceImplementation3;
        public PoliceController(
            ILogger<SozlesmeController> Logger,
            IOptions<ApiSettings> ApiSettings,
            IOptions<JwtOptions> JwtOptions,
            IHttpClientFactoryService<List<Police>> HttpClientServiceImplementation,
            IHttpClientFactoryService<Police> HttpClientServiceImplementation2,
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
                   Metod = "/Police/GetList",
                   RequestParam = string.Empty,
                   MetodType = HttpMethod.Get,
                   HeaderList = new Dictionary<string, string>() { { "Authorization", $"Bearer {User.FindFirst("AuthToken").Value}" } }
               });

            PoliceViewModel policeViewModel = new PoliceViewModel()
            {
                PoliceListesi = httpRequestRes.Data,
                Message = httpRequestRes.Message,
                Success = httpRequestRes.Success
            };
            return View(policeViewModel);
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
                      Metod = "/Police/GetById",
                      RequestParam = $"id={id}",
                      MetodType = HttpMethod.Get,
                      HeaderList = new Dictionary<string, string>() { { "Authorization", $"Bearer {User.FindFirst("AuthToken").Value}" } }

                  });
            PoliceViewModel policeViewModel = new PoliceViewModel()
            {
                Police = httpRequestRes.Data,
                Message = httpRequestRes.Message,
                Success = httpRequestRes.Success
            };
            return View(policeViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Ekleme(PoliceViewModel policeViewModel)
        {
            var httpRequestRes = await _httpClientServiceImplementation3.Execute(
                 new RequestModel()
                 {
                     BaseUrl = _apiSettings.BaseUrl,
                     Metod = "/Police/Add",
                     RequestParam = JsonSerializer.Serialize(policeViewModel.Police),
                     MetodType = HttpMethod.Post,
                     HeaderList = new Dictionary<string, string>() { { "Authorization", $"Bearer {User.FindFirst("AuthToken").Value}" } }

                 });



            if (httpRequestRes.Success)//gelen kayıt db ye başarılı kaydedilmiş ise listeme ekrananıa git
            {
                return RedirectToAction("Liste", "Police");
            }
            else
            {
                policeViewModel.Message = httpRequestRes.Message;
                return View(policeViewModel);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Guncelleme(PoliceViewModel policeViewModel)
        {
            var httpRequestRes = await _httpClientServiceImplementation3.Execute(
                new RequestModel()
                {
                    BaseUrl = _apiSettings.BaseUrl,
                    Metod = "/Police/Update",
                    RequestParam = JsonSerializer.Serialize(policeViewModel.Police),
                    MetodType = HttpMethod.Post,
                    HeaderList = new Dictionary<string, string>() { { "Authorization", $"Bearer {User.FindFirst("AuthToken").Value}" } }

                });



            if (httpRequestRes.Success)//gelen kayıt db ye başarılı kaydedilmiş ise listeme ekrananıa git
            {
                return RedirectToAction("Liste", "Police");
            }
            else
            {
                policeViewModel.Message = httpRequestRes.Message;
                return View(policeViewModel);
            }
        }
    }
}
