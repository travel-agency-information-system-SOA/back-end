using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Author.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
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
    public class TourObjectQueryTests : BaseToursIntegrationTest
    {
        public TourObjectQueryTests(ToursTestFactory factory) : base(factory) { }

        [Fact]
        public void Retrieves_all()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            var result = ((ObjectResult)controller.GetAll(0, 0).Result)?.Value as PagedResult<TourObjectDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(3);
            result.TotalCount.ShouldBe(3);
        }

        private static ObjectController CreateController(IServiceScope scope)
        {
            return new ObjectController(scope.ServiceProvider.GetRequiredService<ITourObjectService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
