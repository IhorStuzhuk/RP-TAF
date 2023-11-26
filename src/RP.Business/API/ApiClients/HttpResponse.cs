using System.Net;

namespace RP.Business.API.ApiClients
{
    public class HttpResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Content { get; set; }
    }
}
