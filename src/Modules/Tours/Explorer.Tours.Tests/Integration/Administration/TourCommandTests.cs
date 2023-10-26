using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
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
		public void Creates()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();
			var controller = CreateController(scope);
			var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
			var newEntity = new TourDTO
			{
				Id=5,
				Name = "Dubai",
				Description = "Grad sa najvisim gradjevinama",
				DifficultyLevel = "Easy",
				
				Price = 0,
				Status = "Draft",
				GuideId=1
			};
			newEntity.Tags = new List<string> { "aaa", "aaa" };

			// Act
			var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourDTO;

			// Assert - Response
			result.ShouldNotBeNull();
			result.Id.ShouldNotBe(0);
			result.Name.ShouldBe(newEntity.Name);

			// Assert - Database
			var storedEntity = dbContext.Tours.FirstOrDefault(i => i.Name == newEntity.Name);
			storedEntity.ShouldNotBeNull();
			//storedEntity.Id.ShouldBe(result.Id);
		}

		[Fact]
		public void Create_fails_invalid_data()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();
			var controller = CreateController(scope);
			var updatedEntity = new TourDTO
			{
				Description = "Test"
			};

			// Act
			var result = (ObjectResult)controller.Create(updatedEntity).Result;

			// Assert
			result.ShouldNotBeNull();
			result.StatusCode.ShouldBe(400);
		}

		public void Deletes()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();
			var controller = CreateController(scope);
			var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();

			// Act
			var result = (OkResult)controller.Delete(-3);

			// Assert - Response
			result.ShouldNotBeNull();
			result.StatusCode.ShouldBe(200);

			// Assert - Database
			var storedCourse = dbContext.Tours.FirstOrDefault(i => i.Id == -3);
			storedCourse.ShouldBeNull();
		}

		[Fact]
		public void Delete_fails_invalid_id()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();
			var controller = CreateController(scope);

			// Act
			var result = (ObjectResult)controller.Delete(-1000);

			// Assert
			result.ShouldNotBeNull();
			result.StatusCode.ShouldBe(404);
		}

		private static TourController CreateController(IServiceScope scope)
		{
			return new TourController(scope.ServiceProvider.GetRequiredService<ITourService>())
			{
				ControllerContext = BuildContext("-1")
			};
		}
	}

	
}
