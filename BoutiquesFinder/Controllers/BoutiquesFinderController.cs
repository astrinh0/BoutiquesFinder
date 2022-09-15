namespace BoutiquesFinder.Controllers
{
    using BoutiquesFinder.Application.Services.Interface;
    using BoutiquesFinder.Domain.Models;
    using Microsoft.AspNetCore.Mvc;
    using Swashbuckle.AspNetCore.Annotations;

    [Route("")]
    [ApiController]
    public class BoutiquesFinderController : ControllerBase
    {
        private readonly IBoutiqueFinderService boutiqueFinderService;
        private readonly ILogger<BoutiquesFinderController> logger;

        public BoutiquesFinderController(ILogger<BoutiquesFinderController> logger,
            IBoutiqueFinderService boutiqueFinderService)
        {
            this.logger = logger;
            this.boutiqueFinderService = boutiqueFinderService;
        }

        [SwaggerOperation("Get 5 closest boutiques", null, Tags = new[] { "TROUVA API" })]
        [SwaggerResponse(StatusCodes.Status200OK, Description = "Method successfully executed.", Type = typeof(NearBotiques))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, Description = "The endpoint or data structure is not in line with expectations.", Type = typeof(BadRequestResult))]
        [SwaggerResponse(StatusCodes.Status404NotFound, Description = "The requested resource was not found.", Type = typeof(NotFoundResult))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, Description = "An unexpected API error has occurred.", Type = typeof(StatusCodeResult))]
        [HttpGet]
        [Route("getclosest")]
        public async Task<NearBotiques> GetNearBoutiques(double latitude, double longitude)
        {
            var result = await boutiqueFinderService.GetNearBoutiques(latitude, longitude);
            return result;
        }
    }
}