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
    public interface IBlogPostService
    {
        Result<PagedResult<BlogPostDto>> GetPaged(int page, int pageSize);
        Result<BlogPostDto> Create(BlogPostDto blogPost);
        Result<BlogPostDto> Update(BlogPostDto blogPost);
        Result Delete(int id);

        Result<BlogPostDto> AddComment(int blogPostId, BlogPostCommentDto comment);
        Result<BlogPostDto> RemoveComment(int blogPostId, int userId, DateTime creationTime);
        Result<BlogPostDto> UpdateComment(int blogPostId, BlogPostCommentDto editedComment);
    }
}

