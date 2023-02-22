using System.Collections.Generic;
using System.Net.Http;

namespace TheCase2WebPortal.Models.Helpers
{
    public class RequestModel
    {
        public string RequestParam { get; set; }
        public string BaseUrl { get; set; }
        public string Metod { get; set; }
        public Dictionary<string, string> HeaderList { get; set; }
        public HttpMethod MetodType { get; set; }
    }
}
