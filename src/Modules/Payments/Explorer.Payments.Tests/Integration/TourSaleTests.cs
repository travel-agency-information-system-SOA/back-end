using Explorer.API.Controllers.Author.Administration;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Payments.Tests.Integration
{
    [Collection("Sequential")]
    public class TourSaleTests : BasePaymentsIntegrationTest
    {
        public TourSaleTests(PaymentsTestFactory factory) : base(factory) { }
        // fali dole, podaci, sve, autor id
        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var newEntity = new TourSaleDto
            {
                TourIds = new List<int> { 0 },
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                SalePercentage = 50,
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as TourSaleDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.SalePercentage.ShouldBe(newEntity.SalePercentage);

            // Assert - Database
            /*var storedEntity = dbContext.GuideReviews.FirstOrDefault(i => i.Comment == newEntity.Comment);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);*/
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TourSaleDto
            {
                SalePercentage = 550,
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
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();
            var updatedEntity = new TourSaleDto
            {
                Id = -1,
                TourIds = new List<int> { 0 },
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                SalePercentage = 50,
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as TourSaleDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.SalePercentage.ShouldBe(updatedEntity.SalePercentage);

            // Assert - Database
            /*var storedEntity = dbContext.GuideReviews.FirstOrDefault(i => i.Comment == "njnjljlj");
            storedEntity.ShouldNotBeNull();
            storedEntity.Rating.ShouldBe(updatedEntity.Rating);
            var oldEntity = dbContext.GuideReviews.FirstOrDefault(i => i.Comment == "top");
            oldEntity.ShouldBeNull();*/
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new TourSaleDto
            {
                Id = -1,
                TourIds = new List<int> { 0 },
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow,
                SalePercentage = 50,
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
            var dbContext = scope.ServiceProvider.GetRequiredService<PaymentsContext>();

            // Act
            var result = (OkResult)controller.Delete(-3);

            // Assert - Response
            result.ShouldNotBeNull();
            result.StatusCode.ShouldBe(200);

            // Assert - Database
            /*var storedCourse = dbContext.GuideReviews.FirstOrDefault(i => i.Id == -3);
            storedCourse.ShouldBeNull();*/
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

        [Fact]
        public void Retrieves_all()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetAllByAuthor(0, 0, 3).Result)?.Value as PagedResult<TourSaleDto>;

            // Assert
            result.ShouldNotBeNull();
            result.Results.Count.ShouldBe(1);
            result.TotalCount.ShouldBe(1);
        }

        [Fact]
        public void Retrieves_discount()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);

            // Act
            var result = ((ObjectResult)controller.GetDiscount(2).Result)?.Value as TourSaleDto;

            // Assert
            result.ShouldNotBeNull();
            result.SalePercentage.ShouldBe(50);
        }

        private static TourSaleController CreateController(IServiceScope scope)
        {
            return new TourSaleController(scope.ServiceProvider.GetRequiredService<ITourSaleService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
