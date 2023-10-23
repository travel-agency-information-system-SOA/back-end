using Explorer.Blog.API.Dtos;
using Explorer.Blog.API.Public;
using Explorer.Blog.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/blogPostComment")]
    // mzd ce menjati
    public class BlogPostCommentController : BaseApiController
    {
        private readonly IBlogPostCommentService _blogPostCommentService;

        public BlogPostCommentController(IBlogPostCommentService blogPostCommentService)
        {
            _blogPostCommentService = blogPostCommentService;
        }

        [HttpGet]
        public ActionResult<PagedResult<BlogPostCommentDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _blogPostCommentService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<BlogPostCommentDto> Create([FromBody] BlogPostCommentDto blogPostComment)
        {
            var result = _blogPostCommentService.Create(blogPostComment);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<BlogPostCommentDto> Update([FromBody] BlogPostCommentDto blogPostComment)
        {
            var result = _blogPostCommentService.Update(blogPostComment);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _blogPostCommentService.Delete(id);
            return CreateResponse(result);
        }

    }
}
