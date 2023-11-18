using Newtonsoft.Json;
namespace RP.Business.API.Models
{
    public class DashboardResponceDto
    {
        [JsonProperty(PropertyName = "content")]
        public List<DashboardDto> Dashboards { get; set; }

        [JsonProperty(PropertyName = "page")]
        public Page Page { get; set; } 
    }

    public class Page
    {
        public int number { get; set; }
        public int size { get; set; }

        public int totalElements { get; set; }

        public int totalPages { get; set; }
    }
}
