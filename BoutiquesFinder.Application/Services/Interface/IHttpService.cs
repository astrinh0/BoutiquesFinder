namespace BoutiquesFinder.Application.Services.Interface
{
    using BoutiquesFinder.Domain.Models;

    public interface IHttpService
    {
        public Task<NearBotiques> RequestToTrouvaApi();
    }
}