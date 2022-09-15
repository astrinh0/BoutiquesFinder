namespace BoutiquesFinder.Application.Services.Implementation
{
    using System.Runtime.Caching;
    using System.Threading.Tasks;
    using BoutiquesFinder.Application.Services.Interface;
    using BoutiquesFinder.Domain.Models;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;

    public class HttpService : IHttpService
    {
        private const string CacheKey = "NearBotiquesList";
        private readonly ObjectCache cache;
        private readonly HttpClient client;

        public HttpService(IConfiguration configuration)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(configuration.GetRequiredSection("BaseUrl").Value);
            cache = MemoryCache.Default;
        }

        public async Task<NearBotiques> RequestToTrouvaApi()
        {
            if (cache.Contains(CacheKey))
            {
                return (NearBotiques)cache.Get(CacheKey);
            }
            else
            {
                var responseMessage = await client.GetAsync("");
                var result = JsonConvert.DeserializeObject<NearBotiques>(await responseMessage.Content.ReadAsStringAsync());
                if (result != null)
                {
                    CacheItemPolicy cacheItemPolicy = new()
                    {
                        AbsoluteExpiration = DateTime.Now.AddHours(1.0)
                    };
                    cache.Add(CacheKey, result, cacheItemPolicy);
                    return result;
                }
                else
                {
                    return new NearBotiques();
                }
            }
        }
    }
}