using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
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
	public class TourQueryTests : BaseToursIntegrationTest
	{
		public TourQueryTests(ToursTestFactory factory) : base(factory)
		{
		}

		[Fact]
		public void Retrieves_all()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();
			var controller = CreateController(scope);

			// Act
			var result = ((ObjectResult)controller.GetByUserId(1,0,0).Result)?.Value as PagedResult<TourDTO>;

			// Assert
			result.ShouldNotBeNull();
			result.Results.Count.ShouldBe(3);
			result.TotalCount.ShouldBe(3);
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
