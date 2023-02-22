using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TheCase2WebPortal.Models.Helpers;
using TheCase2WebPortal.Models.Results;
using static System.Net.Mime.MediaTypeNames;

namespace TheCase2WebPortal.Helpers
{
    public interface IHttpClientFactoryService<T> where T : class
    {
        Task<ResultBase<T>> Execute(RequestModel requestModel);
    }
    public class HttpClientFactoryService<T> : IHttpClientFactoryService<T> where T : class
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly JsonSerializerOptions _options;
        public HttpClientFactoryService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<ResultBase<T>> Execute(RequestModel requestModel)
        {
            ResultBase<T> Result = null;
            try
            {
                var _httpClient = _httpClientFactory.CreateClient();
                if (requestModel.HeaderList != null && requestModel.HeaderList.Any())
                {
                    foreach (var HeaderItem in requestModel.HeaderList)
                    {
                        _httpClient.DefaultRequestHeaders.Add(HeaderItem.Key, HeaderItem.Value);
                    }
                }

                if (requestModel.MetodType == HttpMethod.Get)
                {
                    string Url = $"{requestModel.BaseUrl}{requestModel.Metod}";

                    if (!string.IsNullOrEmpty(requestModel.RequestParam))
                    {
                        Url = $"{Url}?{requestModel.RequestParam}";
                    }
                    using (var response = await _httpClient.GetAsync($"{Url}", HttpCompletionOption.ResponseHeadersRead))
                    {
                        response.EnsureSuccessStatusCode();
                        var stream = await response.Content.ReadAsStreamAsync();

                        Result = await JsonSerializer.DeserializeAsync<ResultBase<T>>(stream, _options);
                    }
                }
                else
                {
                    var todoItemJson = new StringContent(requestModel.RequestParam, Encoding.UTF8, Application.Json); // using static System.Net.Mime.MediaTypeNames;

                    using var httpResponseMessage =
                        await _httpClient.PostAsync($"{requestModel.BaseUrl}{requestModel.Metod}", todoItemJson);
                    httpResponseMessage.EnsureSuccessStatusCode();
                    var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
                    stream.Position = 0;
                    Result = await JsonSerializer.DeserializeAsync<ResultBase<T>>(stream, _options);
                }
            }
            catch (Exception ex)
            {
                Result = new ResultBase<T>() { Message = $"{requestModel.BaseUrl}{requestModel.Metod} servisi hata oluştu: {ex.Message}" };
            }
            return Result;
        }
    }
}
