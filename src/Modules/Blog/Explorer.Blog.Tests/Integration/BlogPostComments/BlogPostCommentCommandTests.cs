using Explorer.Tours.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.API.Controllers.Administrator.Administration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Infrastructure.Database;
using Explorer.Tours.API.Public.Administration;
using Explorer.API.Controllers.Tourist;
using Explorer.Blog.API.Public;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.Infrastructure.Database;

namespace Explorer.Blog.Tests.Integration.BlogPostComments;

[Collection("Sequential")]
public class BlogPostCommentCommandTests : BaseBlogIntegrationTest
{
    public BlogPostCommentCommandTests(BlogTestFactory factory) : base(factory) { }

    [Fact]
    public void Creates()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
        var newEntity = new BlogPostCommentDto
        {
            Text = "Kreiranje komentara",
            BlogId = 1,
            UserId = 1,
            CreationTime = DateTime.Now.ToUniversalTime(),
            LastUpdatedTime = DateTime.Now.ToUniversalTime()
        };

        // Act
        var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as BlogPostCommentDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldNotBe(0);
        result.Text.ShouldBe(newEntity.Text);
        result.UserId.ShouldBe(newEntity.UserId);
        result.BlogId.ShouldBe(newEntity.BlogId);
        result.CreationTime.ShouldBe(newEntity.CreationTime);
        result.LastUpdatedTime.ShouldBe(newEntity.LastUpdatedTime);

        // Assert - Database
        var storedEntity = dbContext.BlogPostComments.FirstOrDefault(i => i.Text == newEntity.Text);
        storedEntity.ShouldNotBeNull();
        storedEntity.Id.ShouldBe(result.Id);
    }

    [Fact]
    public void Create_fails_invalid_data()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new BlogPostCommentDto
        {
            BlogId = 1,
            UserId = 1,
            CreationTime = DateTime.Now.ToUniversalTime(),
            LastUpdatedTime = DateTime.Now.ToUniversalTime()

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
        var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
        var updatedEntity = new BlogPostCommentDto
        {
            Id = -1,
            Text = "Comment1",
            BlogId = 1,
            UserId = 1,
            CreationTime = DateTime.Now.ToUniversalTime(),
            LastUpdatedTime = DateTime.Now.ToUniversalTime()
        };

        // Act
        var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as BlogPostCommentDto;

        // Assert - Response
        result.ShouldNotBeNull();
        result.Id.ShouldBe(-1);
        result.Text.ShouldBe(updatedEntity.Text);
        result.BlogId.ShouldBe(updatedEntity.BlogId);
        result.UserId.ShouldBe(updatedEntity.UserId);
        result.CreationTime.ShouldBe(updatedEntity.CreationTime);
        result.LastUpdatedTime.ShouldBe(updatedEntity.LastUpdatedTime);


        // Assert - Database
        var storedEntity = dbContext.BlogPostComments.FirstOrDefault(i => i.Text == "Comment1");
        storedEntity.ShouldNotBeNull();
        storedEntity.UserId.ShouldBe(updatedEntity.UserId);
        storedEntity.BlogId.ShouldBe(updatedEntity.BlogId);
        storedEntity.LastUpdatedTime.ShouldBe(updatedEntity.LastUpdatedTime);
        storedEntity.CreationTime.ShouldBe(updatedEntity.CreationTime);
        var oldEntity = dbContext.BlogPostComments.FirstOrDefault(i => i.Text == "Komentar1");
        oldEntity.ShouldBeNull();
    }

    [Fact]
    public void Update_fails_invalid_id()
    {
        // Arrange
        using var scope = Factory.Services.CreateScope();
        var controller = CreateController(scope);
        var updatedEntity = new BlogPostCommentDto
        {
            Id = -1000,
            Text = "test",
            BlogId = 1,
            UserId = 1,
            CreationTime = DateTime.Now.ToUniversalTime(),
            LastUpdatedTime = DateTime.Now.ToUniversalTime()
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
        var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();

        // Act
        var result = (OkResult)controller.Delete(-3);

        // Assert - Response
        result.ShouldNotBeNull();
        result.StatusCode.ShouldBe(200);

        // Assert - Database
        var storedCourse = dbContext.BlogPostComments.FirstOrDefault(i => i.Id == -3);
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

    private static BlogPostCommentController CreateController(IServiceScope scope)
    {
        return new BlogPostCommentController(scope.ServiceProvider.GetRequiredService<IBlogPostCommentService>())
        {
            ControllerContext = BuildContext("-1")
        };
    }

}
