namespace RP.Business.Web.Models
{
    public class DashboardModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is DashboardModel model &&
                   Name == model.Name &&
                   Description == model.Description &&
                   Owner == model.Owner;
        }
    }
}
