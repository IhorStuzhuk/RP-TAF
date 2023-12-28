namespace RP.Business.API.Models
{
    public class DashboardDto
    {
        public Guid Id { get; set; }

        public string? Description { get; set; }

        public string Name { get; set; }

        public string Owner { get; set; }

        public List<Widget> Widgets { get; set; }
    }
}
