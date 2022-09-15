namespace BoutiquesFinder.Application.Services.Interface
{
    using System.Threading.Tasks;
    using BoutiquesFinder.Domain.Models;

    public interface IBoutiqueFinderService
    {
        public Task<NearBotiques> GetNearBoutiques(double latitude, double longitude);
    }
}