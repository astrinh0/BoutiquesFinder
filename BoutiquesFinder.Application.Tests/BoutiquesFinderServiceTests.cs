namespace BoutiquesFinder.Application.Tests
{
    using BoutiquesFinder.Application.Services.Implementation;
    using BoutiquesFinder.Application.Services.Interface;
    using BoutiquesFinder.Domain.Models;
    using FluentAssertions;
    using Moq;
    using Xunit;

    public class BoutiquesFinderServiceTests
    {
        [Fact]
        public async Task BoutiquesFinderService_GetNearBoutiques_ReturnNull()
        {
            //ARRANGE
            var httpServiceMock = new Mock<IHttpService>();
            httpServiceMock.Setup(x => x.RequestToTrouvaApi())
                .Returns(Task.FromResult(new NearBotiques()));

            var boutiqueService = new BoutiqueFinderService(httpServiceMock.Object);
            var latitude = 20;
            var longitude = 20;

            //ACT
            var result = await boutiqueService.GetNearBoutiques(latitude, longitude);

            //ASSERT
            Assert.Null(result.Boutiques);
        }

        [Fact]
        public async Task BoutiquesFinderService_GetNearBoutiques_ReturnSuccess()
        {
            //ARRANGE
            var expectedResult = new NearBotiques
            {
                Boutiques = new List<Boutique>
                    {
                        new Boutique
                        {
                            Slug = "Slug this",
                            _Id = "This Id",
                            Description = "Description this",
                            Founder_Quote = "Testing this",
                            Name = "Joao Veloso",
                            Location = new Location
                            {
                                Lat = 20,
                                Lon = 20
                            }                        }
                    }
            };

            var httpServiceMock = new Mock<IHttpService>();
            httpServiceMock.Setup(x => x.RequestToTrouvaApi())
                .Returns(Task.FromResult(expectedResult));

            var boutiqueService = new BoutiqueFinderService(httpServiceMock.Object);
            var latitude = 20;
            var longitude = 20;

            //ACT
            var result = await boutiqueService.GetNearBoutiques(latitude, longitude);

            //ASSERT
            Assert.NotNull(result.Boutiques);
            Assert.Single(result.Boutiques);
            Assert.Equal(result.Boutiques.First(), expectedResult.Boutiques.First());
        }

        [Fact]
        public async Task BoutiquesFinderService_GetNearBoutiques_WrongLatitudeReturnException()
        {
            //ARRANGE
            var httpServiceMock = new Mock<IHttpService>();
            httpServiceMock.Setup(x => x.RequestToTrouvaApi())
                .Returns(Task.FromResult(
                    new NearBotiques
                    {
                        Boutiques = new List<Boutique>()
                    })
                );

            var boutiqueService = new BoutiqueFinderService(httpServiceMock.Object);
            var latitude = -91;
            var longitude = 20;

            //ACT
            Func<Task<NearBotiques>> result = async () => await boutiqueService.GetNearBoutiques(latitude, longitude);

            //ASSERT
            await result.Should().ThrowAsync<ArgumentException>().Where(e => e.Message == "Latitude must be between -90 to 90 and longetitude must be between -180 to 180");
        }

        [Fact]
        public async Task BoutiquesFinderService_GetNearBoutiques_WrongLongitudeReturnException()
        {
            //ARRANGE
            var httpServiceMock = new Mock<IHttpService>();
            httpServiceMock.Setup(x => x.RequestToTrouvaApi())
                .Returns(Task.FromResult(
                    new NearBotiques
                    {
                        Boutiques = new List<Boutique>()
                    })
                );

            var boutiqueService = new BoutiqueFinderService(httpServiceMock.Object);
            var latitude = -90;
            var longitude = -181;

            //ACT
            Func<Task<NearBotiques>> result = async () => await boutiqueService.GetNearBoutiques(latitude, longitude);

            //ASSERT
            await result.Should().ThrowAsync<ArgumentException>().Where(e => e.Message == "Latitude must be between -90 to 90 and longetitude must be between -180 to 180");
        }
    }
}