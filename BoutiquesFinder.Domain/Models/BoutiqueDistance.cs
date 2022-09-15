namespace BoutiquesFinder.Domain.Models
{
    public class BoutiqueDistance
    {
        public BoutiqueDistance(Boutique boutique, double distance)
        {
            Boutique = boutique;
            Distance = distance;
        }

        public Boutique Boutique { get; set; }
        public double Distance { get; set; }
    }
}