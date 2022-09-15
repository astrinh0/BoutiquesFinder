namespace BoutiquesFinder.Domain.Models
{
    using System.Collections.Generic;

    public class NearBotiques
    {
        public IEnumerable<Boutique> Boutiques { get; set; }
    }
}