using Explorer.API.Controllers.Tourist.Marketplace;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Marketplace;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Marketplace
{
    [Collection("Sequential")]
    public class PreferencesCommandTests : BaseToursIntegrationTest
    {
        public PreferencesCommandTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var newEntity = new PreferencesDto
            {
                UserId = 5,
                PreferredDifficulty = "Easy",
                TransportationPreferences = new List<int> { 3, 2, 1, 0 },
                InterestTags = new List<string> { "Centar" }
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as PreferencesDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.UserId.ShouldBe(newEntity.UserId);

            // Assert - Database
            var storedEntity = dbContext.Preferences.FirstOrDefault(i => i.UserId == newEntity.UserId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new PreferencesDto
            {
                PreferredDifficulty = "Moderate"
            };

            // Act
            var result = (ObjectResult)controller.Create(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(400);
        }

        [Fact]
        public void Updates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
            var updatedEntity = new PreferencesDto
            {
                Id = -1,
                UserId = 1,
                PreferredDifficulty = "Difficult",
                TransportationPreferences = new List<int> { 2, 2, 2, 2 },
                InterestTags = new List<string> { "Trg" }
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as PreferencesDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.UserId.ShouldBe(updatedEntity.UserId);
            result.PreferredDifficulty.ShouldBe(updatedEntity.PreferredDifficulty);
            result.TransportationPreferences.ShouldBe(updatedEntity.TransportationPreferences);
            result.InterestTags.ShouldBe(updatedEntity.InterestTags);

            // Assert - Database
            var storedEntity = dbContext.Preferences.FirstOrDefault(i => i.UserId == 1);
            storedEntity.ShouldNotBeNull();
            //storedEntity.PreferredDifficulty.ShouldBe(updatedEntity.PreferredDifficulty);
            storedEntity.TransportationPreferences.ShouldBe(updatedEntity.TransportationPreferences);
            storedEntity.InterestTags.ShouldBe(updatedEntity.InterestTags);
            var oldEntity = dbContext.Preferences.FirstOrDefault(i => i.UserId == 1 && i.PreferredDifficulty == 0);
            oldEntity.ShouldBeNull();
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new PreferencesDto
            {
                Id = -1000,
                UserId = 1,
                PreferredDifficulty = "Easy"
            };

            // Act
            var result = (ObjectResult)controller.Update(updatedEntity).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
        }

        [Fact]
        public void Deletes()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (OkResult)controller.Delete(-3).Result;

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            var storedCourse = dbContext.Preferences.FirstOrDefault(i => i.Id == -3);
            storedCourse.ShouldBeNull();
        }

        [Fact]
        public void Delete_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = (ObjectResult)controller.Delete(-1000).Result;

            // Assert
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(404);
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
