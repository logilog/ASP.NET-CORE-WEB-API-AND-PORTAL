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
    public class PersonController : TheCase2WebPortalBaseController
    {
        private readonly IHttpClientFactoryService<List<person>> _httpClientServiceImplementation;
        private readonly IHttpClientFactoryService<person> _httpClientServiceImplementation2;
        private readonly IHttpClientFactoryService<object> _httpClientServiceImplementation3;
        public PersonController(
            ILogger<PersonController> Logger,
            IOptions<ApiSettings> ApiSettings,
            IOptions<JwtOptions> JwtOptions,
            IHttpClientFactoryService<List<person>> HttpClientServiceImplementation,
            IHttpClientFactoryService<person> HttpClientServiceImplementation2,
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
                   Metod = "/Person/GetList",
                   RequestParam = string.Empty,
                   MetodType = HttpMethod.Get,
                   HeaderList = new Dictionary<string, string>() { { "Authorization", $"Bearer {User.FindFirst("AuthToken").Value}" } }

               });

            PersonViewModel personViewModel = new PersonViewModel()
            {
                PersonListesi = httpRequestRes.Data,
                Message = httpRequestRes.Message,
                Success = httpRequestRes.Success
            };
            return View(personViewModel);
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
                      Metod = "/Person/GetById",
                      RequestParam = $"id={id}",
                      MetodType = HttpMethod.Get,
                      HeaderList = new Dictionary<string, string>() { { "Authorization", $"Bearer {User.FindFirst("AuthToken").Value}" } }

                  });
            PersonViewModel personViewModel = new PersonViewModel()

            {
                Person = httpRequestRes.Data,
                Message = httpRequestRes.Message,
                Success = httpRequestRes.Success
            };
            return View(personViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Ekleme(PersonViewModel personViewModel)
        {
            var httpRequestRes = await _httpClientServiceImplementation3.Execute(
                 new RequestModel()
                 {
                     BaseUrl = _apiSettings.BaseUrl,
                     Metod = "/Person/Add",
                     RequestParam = JsonSerializer.Serialize(personViewModel.Person),
                     MetodType = HttpMethod.Post,
                     HeaderList = new Dictionary<string, string>() { { "Authorization", $"Bearer {User.FindFirst("AuthToken").Value}" } }

                 });



            if (httpRequestRes.Success)//gelen kayıt db ye başarılı kaydedilmiş ise listeme ekrananıa git
            {
                return RedirectToAction("Liste", "Person");
            }
            else
            {
                personViewModel.Message = httpRequestRes.Message;
                return View(personViewModel);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Guncelleme(PersonViewModel personViewModel)
        {
            var httpRequestRes = await _httpClientServiceImplementation3.Execute(
                new RequestModel()
                {
                    BaseUrl = _apiSettings.BaseUrl,
                    Metod = "/Person/Update",
                    RequestParam = JsonSerializer.Serialize(personViewModel.Person),
                    MetodType = HttpMethod.Post,
                    HeaderList = new Dictionary<string, string>() { { "Authorization", $"Bearer {User.FindFirst("AuthToken").Value}" } }

                });



            if (httpRequestRes.Success)//gelen kayıt db ye başarılı kaydedilmiş ise listeme ekrananıa git
            {
                return RedirectToAction("Liste", "Person");
            }
            else
            {
                personViewModel.Message = httpRequestRes.Message;
                return View(personViewModel);
            }
        }
    }
}
