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
    public class TarifeController : TheCase2WebPortalBaseController
    {
        private readonly IHttpClientFactoryService<List<Tarife>> _httpClientServiceImplementation;
        private readonly IHttpClientFactoryService<Tarife> _httpClientServiceImplementation2;
        private readonly IHttpClientFactoryService<object> _httpClientServiceImplementation3;
        public TarifeController(
            ILogger<TarifeController> Logger,
            IOptions<ApiSettings> ApiSettings,
            IOptions<JwtOptions> JwtOptions,
            IHttpClientFactoryService<List<Tarife>> HttpClientServiceImplementation,
            IHttpClientFactoryService<Tarife> HttpClientServiceImplementation2,
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
                   Metod = "/Tarife/GetList",
                   RequestParam = string.Empty,
                   MetodType = HttpMethod.Get,
                   HeaderList = new Dictionary<string, string>() { { "Authorization", $"Bearer {User.FindFirst("AuthToken").Value}" } }

               });

            TarifeViewModel tarifeViewModel = new TarifeViewModel()
            {
                TarifeListesi = httpRequestRes.Data,
                Message = httpRequestRes.Message,
                Success = httpRequestRes.Success
            };
            return View(tarifeViewModel);
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
                      Metod = "/Tarife/GetById",
                      RequestParam = $"id={id}",
                      MetodType = HttpMethod.Get,
                      HeaderList = new Dictionary<string, string>() { { "Authorization", $"Bearer {User.FindFirst("AuthToken").Value}" } }

                  });
            TarifeViewModel tarifeViewModel = new TarifeViewModel()


            {
                Tarife = httpRequestRes.Data,
                Message = httpRequestRes.Message,
                Success = httpRequestRes.Success
            };
            return View(tarifeViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Ekleme(TarifeViewModel tarifeViewModel)
        {
            var httpRequestRes = await _httpClientServiceImplementation3.Execute(
                 new RequestModel()
                 {
                     BaseUrl = _apiSettings.BaseUrl,
                     Metod = "/Tarife/Add",
                     RequestParam = JsonSerializer.Serialize(tarifeViewModel.Tarife),
                     MetodType = HttpMethod.Post,
                     HeaderList = new Dictionary<string, string>() { { "Authorization", $"Bearer {User.FindFirst("AuthToken").Value}" } }

                 });



            if (httpRequestRes.Success)//gelen kayıt db ye başarılı kaydedilmiş ise listeme ekrananıa git
            {
                return RedirectToAction("Liste", "Tarife");
            }
            else
            {
                tarifeViewModel.Message = httpRequestRes.Message;
                return View(tarifeViewModel);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Guncelleme(TarifeViewModel tarifeViewModel)
        {
            var httpRequestRes = await _httpClientServiceImplementation3.Execute(
                new RequestModel()
                {
                    BaseUrl = _apiSettings.BaseUrl,
                    Metod = "/Tarife/Update",
                    RequestParam = JsonSerializer.Serialize(tarifeViewModel.Tarife),
                    MetodType = HttpMethod.Post,
                    HeaderList = new Dictionary<string, string>() { { "Authorization", $"Bearer {User.FindFirst("AuthToken").Value}" } }

                });



            if (httpRequestRes.Success)//gelen kayıt db ye başarılı kaydedilmiş ise listeme ekrananıa git
            {
                return RedirectToAction("Liste", "Tarife");
            }
            else
            {
                tarifeViewModel.Message = httpRequestRes.Message;
                return View(tarifeViewModel);
            }
        }
    }
}
