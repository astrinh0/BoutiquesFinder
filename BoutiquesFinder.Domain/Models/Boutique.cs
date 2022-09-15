namespace BoutiquesFinder.Domain.Models
{
    public class Boutique
    {
        public string _Id { get; set; }
        public string? Description { get; set; }
        public string? Founder_Quote { get; set; }
        public Location? Location { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
    }
}