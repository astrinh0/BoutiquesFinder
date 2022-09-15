namespace BoutiquesFinder.Application.Services.Implementation
{
    using BoutiquesFinder.Application.Services.Interface;
    using BoutiquesFinder.Domain.Models;
    using GeoCoordinatePortable;

    public class BoutiqueFinderService : IBoutiqueFinderService
    {
        private readonly IHttpService httpService;

        public BoutiqueFinderService(IHttpService httpService)
        {
            this.httpService = httpService;
        }

        public async Task<NearBotiques> GetNearBoutiques(double latitude, double longitude)
        {
            if (!CheckLatitude(latitude) || !CheckLongitude(longitude))
            {
                throw new ArgumentException("Latitude must be between -90 to 90 and longetitude must be between -180 to 180");
            }

            var trouvaBoutiques = await httpService.RequestToTrouvaApi();

            var result = new NearBotiques();

            var coordinateToFind = new GeoCoordinate(latitude, longitude);

            if (trouvaBoutiques.Boutiques == null || !trouvaBoutiques.Boutiques.Any())
            {
                return result;
            }

            result.Boutiques = CalculateDistance(coordinateToFind, trouvaBoutiques.Boutiques);

            return result;
        }

        private static IEnumerable<Boutique> CalculateDistance(GeoCoordinate coordinateToFind, IEnumerable<Boutique> boutiques)
        {
            var boutiquesDistance = new List<BoutiqueDistance>();

            if (boutiques.Any())
            {
                foreach (var boutique in boutiques)
                {
                    if (boutique.Location != null)
                    {
                        var boutiqueCoordinate = new GeoCoordinate(boutique.Location.Lat, boutique.Location.Lon);

                        var distance = coordinateToFind.GetDistanceTo(boutiqueCoordinate);

                        boutiquesDistance.Add(new BoutiqueDistance(boutique, distance));
                    }
                }

                return boutiquesDistance.OrderBy(b => b.Distance).Select(b => b.Boutique).Take(5);
            }
            else
            {
                return boutiques;
            }
        }

        private static bool CheckLatitude(double latitude)
        {
            if (latitude < -90 || latitude > 90) return false;
            else
                return true;
        }

        private static bool CheckLongitude(double longitude)
        {
            if (longitude < -180 || longitude > 180) return false;
            else
                return true;
        }
    }
}