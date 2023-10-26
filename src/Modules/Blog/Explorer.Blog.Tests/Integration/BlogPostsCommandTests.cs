using Explorer.API.Controllers.Administrator.Administration;
using Explorer.API.Controllers.Tourist.Blog;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.Blog.Infrastructure.Database;
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

namespace Explorer.Blog.Tests.Integration
{
    public class BlogPostsCommandTests : BaseBlogIntegrationTest
    {
        public BlogPostsCommandTests(BlogTestFactory factory) : base(factory) { }
        [Fact]
        public void Creates()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var dbContext = scope.ServiceProvider.GetRequiredService<BlogContext>();
            var newEntity = new BlogPostDto
            {
                Title = "Probna CREATE tura",
                Description = "Obilazak etno sela",
                CreationDate = DateTime.Now.ToUniversalTime(),
                Status = BlogPostStatus.DRAFT.ToString()
            };

            // Act
            var result = ((ObjectResult)controller.Create(newEntity).Result)?.Value as BlogPostDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldNotBe(0);
            result.Title.ShouldBe(newEntity.Title);
            result.Description.ShouldBe(newEntity.Description);
            result.Status.ShouldBe(newEntity.Status);

            // Assert - Database
            var storedEntity = dbContext.BlogPosts.FirstOrDefault(i => i.Title == newEntity.Title);
            storedEntity.ShouldNotBeNull();
            storedEntity.Id.ShouldBe(result.Id);
        }

        [Fact]
        public void Create_fails_invalid_data()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new BlogPostDto
            {
                Description = "Test"
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
            var updatedEntity = new BlogPostDto
            {
                Id = -1,
                Title = "Test Update",
                Description = "Papi secer",
                CreationDate = DateTime.Now.ToUniversalTime(),
                Status = BlogPostStatus.DRAFT.ToString()
            };

            // Act
            var result = ((ObjectResult)controller.Update(updatedEntity).Result)?.Value as BlogPostDto;

            // Assert - Response
            result.ShouldNotBeNull();
            result.Id.ShouldBe(-1);
            result.Title.ShouldBe(updatedEntity.Title);
            result.Description.ShouldBe(updatedEntity.Description);
            result.CreationDate.ShouldBe(updatedEntity.CreationDate);
            result.Status.ShouldBe(updatedEntity.Status);

            // Assert - Database
            var storedEntity = dbContext.BlogPosts.FirstOrDefault(i => i.Title == "Test Update");
            storedEntity.ShouldNotBeNull();
            storedEntity.Description.ShouldBe(updatedEntity.Description);
            storedEntity.CreationDate.ShouldBe(updatedEntity.CreationDate);
            var oldEntity = dbContext.BlogPosts.FirstOrDefault(i => i.Title == "Obilazak FG");
            oldEntity.ShouldBeNull();
        }

        [Fact]
        public void Update_fails_invalid_id()
        {
            // Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = CreateController(scope);
            var updatedEntity = new BlogPostDto
            {
                Id = -1000,
                Title = "Test bad Update",
                Description = "Random words",
                CreationDate = DateTime.Now.ToUniversalTime(),
                Status = BlogPostStatus.DRAFT.ToString()
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
            var storedCourse = dbContext.BlogPosts.FirstOrDefault(i => i.Id == -3);
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

        private static BlogPostController CreateController(IServiceScope scope)
        {
            return new BlogPostController(scope.ServiceProvider.GetRequiredService<IBlogPostService>())
            {
                ControllerContext = BuildContext("-1")
            };
        }
    }
}
