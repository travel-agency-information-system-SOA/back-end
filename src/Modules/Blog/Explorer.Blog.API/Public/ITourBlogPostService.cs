using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Blog.API.Public
{
    public interface ITourBlogPostService
    {
        Result<PagedResult<TourBlogPostDto>> GetPaged(int page, int pageSize);
        Result<TourBlogPostDto> Create(TourBlogPostDto blogPost);
        Result<TourBlogPostDto> Update(TourBlogPostDto blogPost);
        Result Delete(int id);

        Result<TourBlogPostDto> GetById(int id);

        Result<PagedResult<TourBlogPostDto>> GetAll(int page, int pageSize);
        Result<TourBlogPostDto> AddComment(int blogPostId, BlogPostCommentDto comment);
        Result<TourBlogPostDto> RemoveComment(int blogPostId, int userId, DateTime creationTime);
        Result<TourBlogPostDto> UpdateComment(int blogPostId, BlogPostCommentDto editedComment);

        Result<TourBlogPostDto> AddRating(int blogPostId, BlogPostRatingDto comment);
        Result<TourBlogPostDto> RemoveRating(int blogPostId, int userId);
    
}
}
