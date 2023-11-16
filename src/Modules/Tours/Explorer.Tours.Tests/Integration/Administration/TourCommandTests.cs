using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author.Authoring;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Tests.Integration.Administration
{
    [Collection("Sequential")]
	public class TourCommandTests : BaseToursIntegrationTest
	{
		public TourCommandTests(ToursTestFactory factory) : base(factory)
		{
		}

        
        [Fact]
        public void Publish_succeeds()
        {
            // Arrange - Input data
            var authorId = "-1";
            var tourId = -1;
            var expectedResponseCode = 200;
            var expectedStatus = TourStatus.Published;
            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope,authorId);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (ObjectResult)controller.Publish(tourId);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);
            // Assert - Database
            var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ShouldBe(expectedStatus);
        }

        [Fact]
        public void Publish_fails_invalid_checkpoints()
        {
            // Arrange - Input data
            var authorId = "-1";
            var tourId = -2;
            var expectedResponseCode = 400;
            var expectedStatus = TourStatus.Draft;
            // Arrange - Controller and dbContext
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope, authorId);
            var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

            // Act
            var result = (ObjectResult)controller.Publish(tourId);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(expectedResponseCode);

            // Assert - Database
            var storedEntity = dbContext.Tours.FirstOrDefault(t => t.Id == tourId);
            storedEntity.ShouldNotBeNull();
            storedEntity.Status.ShouldBe(expectedStatus);
        }

        private static TourController CreateController(IServiceScope scope, string ss)
        {
            return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }

    }


}
