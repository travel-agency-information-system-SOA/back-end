using AutoMapper;
using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.Core.UseCases
{
    public class BlogPostService : CrudService<BlogPostDto, BlogPost>, IBlogPostService
    {
        public BlogPostService(ICrudRepository<BlogPost> crudRepository, IMapper mapper) : base(crudRepository, mapper)
        {
        }

        public BlogPost GetById(int blogPostId)
        {
            return CrudRepository.Get(blogPostId);
        }

        public Result<BlogPostDto> AddComment(int blogPostId, BlogPostCommentDto comment)
        {
            try
            {
                // Retrieve the blog post from the repository
                var blogPost = GetById(blogPostId);

                // Map the DTO to the domain entity
                //var commentEntity = MapToDomain(comment);


                // Add the comment to the blog post
                blogPost.AddComment(comment);

                // Update the blog post in the repository
                CrudRepository.Update(blogPost);

                // Map the updated blog post back to DTO
                var updatedBlogPostDto = MapToDto(blogPost);
                return updatedBlogPostDto;
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
            
        }

        public Result<BlogPostDto> RemoveComment(int blogPostId, int userId, DateTime creationTime)
        {
            try
            {
                // Retrieve the blog post from the repository
                var blogPost = GetById(blogPostId);

                // Assuming that RemoveComment method in BlogPost entity handles the removal logic
                blogPost.RemoveComment(userId, creationTime);

                // Update the blog post in the repository
                CrudRepository.Update(blogPost);

                // Map the updated blog post back to DTO
                var updatedBlogPostDto = MapToDto(blogPost);
                return updatedBlogPostDto;
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

        public Result<BlogPostDto> UpdateComment(int blogPostId, BlogPostCommentDto editedComment)
        {
            try
            {
                // Retrieve the blog post from the repository
                var blogPost = GetById(blogPostId);

                // Assuming BlogPost entity has an EditComment method
                blogPost.EditComment(editedComment);

                // Update the blog post in the repository
                CrudRepository.Update(blogPost);

                // Map the updated blog post back to DTO
                var updatedBlogPostDto = MapToDto(blogPost);
                return updatedBlogPostDto;
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }
}
