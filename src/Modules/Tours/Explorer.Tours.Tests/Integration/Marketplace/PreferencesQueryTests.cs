using Explorer.API.Controllers.Tourist.Marketplace;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Marketplace;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Marketplace
{
    [Collection("Sequential")]
    public class PreferencesQueryTests : BaseToursIntegrationTest
    {
        public PreferencesQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<PreferencesDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3);
            result.TotalCount.ShouldBe(3);
        }

        private static PreferencesController CreateController(IServiceScope scope)
        {
            return new PreferencesController(scope.ServiceProvider.GetRequiredService<IPreferencesService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
