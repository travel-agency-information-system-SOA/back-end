using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist.Review;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Explorer.Tours.Tests.Integration.Review;

[Collection("Sequential")]
public class GuideReviewCommandTests : BaseToursIntegrationTest
{
    public GuideReviewCommandTests(ToursTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<ToursContext>();
        var newEntity = new GuideReviewDto
        {
            UserId = -21,
            GuideId = -11,
            Rating = 4,
            Comment = "njnj",
            SubmissionDate = DateTime.UtcNow
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as GuideReviewDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Rating.ShouldBe(newEntity.Rating);

        // Assert - Database
        var storedEntity = dbContext.GuideReviews.FirstOrDefault(i => i.Comment == newEntity.Comment);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }

    [Fact]
    public void Create_fails_invalid_data()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new GuideReviewDto
        {
            Comment = "Test"
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
        var updatedEntity = new GuideReviewDto
        {
            Id = -1,
            UserId = -21,
            GuideId = -11,
            Rating = 2,
            Comment = "njnjljlj",
            SubmissionDate = DateTime.UtcNow
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as GuideReviewDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-1);
        result.Comment.ShouldBe(updatedEntity.Comment);
        result.Rating.ShouldBe(updatedEntity.Rating);

        // Assert - Database
        var storedEntity = dbContext.GuideReviews.FirstOrDefault(i => i.Comment == "njnjljlj");
        storedEntity.ShouldNotBeNull();
        storedEntity.Rating.ShouldBe(updatedEntity.Rating);
        var oldEntity = dbContext.GuideReviews.FirstOrDefault(i => i.Comment == "top");
        oldEntity.ShouldBeNull();
    }

    [Fact]
    public void Update_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new GuideReviewDto
        {
            Id = -1000,
            UserId = -21,
            GuideId = -11,
            Rating = 2,
            Comment = "njnjljlj",
            SubmissionDate = DateTime.UtcNow
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
        var result = (OkResult)controller.Delete(-3);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        // Assert - Database
        var storedCourse = dbContext.GuideReviews.FirstOrDefault(i => i.Id == -3);
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

    private static GuideReviewController CreateController(IServiceScope scope)
    {
        return new GuideReviewController(scope.ServiceProvider.GetRequiredService<IGuideReviewService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }
}