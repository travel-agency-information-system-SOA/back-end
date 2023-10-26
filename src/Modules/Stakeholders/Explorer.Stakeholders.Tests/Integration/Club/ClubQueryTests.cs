using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist.Club;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
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

namespace Explorer.Stakeholders.Tests.Integration.Club
{
    [Collection("Sequential")]
    public class ClubQueryTests : BaseStakeholdersIntegrationTest
    {
        public ClubQueryTests(StakeholdersTestFactory factory): base(factory) { }

        [Fact]
        public void Retrieves_by_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetByUserID(-21, 0, 0).Result)?.Value as PagedResult<ClubDto>;

            // Assert
            result.ShouldNotBeNull();
            result.TotalCount.ShouldBe(1);
        }
        private static ClubController CreateController(IServiceScope scope)
        {
            return new ClubController(scope.ServiceProvider.GetRequiredService<IClubService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
