namespace RP.Business.Models
{
    public class Widget
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Type { get; set; }

        public Options Options{ get; set; }

        public Size Size { get; set; }

        public Position Position { get; set; }
    }
}
