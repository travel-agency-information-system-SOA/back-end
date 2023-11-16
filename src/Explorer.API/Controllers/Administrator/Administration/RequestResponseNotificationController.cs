using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    [Route("api/administration/requestResponseNotification")]
    public class RequestResponseNotificationController : BaseApiController
    {
        private readonly IRequestResponseNotificationService _requestResponseNotificationService;
        public RequestResponseNotificationController(IRequestResponseNotificationService requestResponseNotificationService)
        {
            _requestResponseNotificationService = requestResponseNotificationService;
        }
        [Authorize(Policy = "authorPolicy")]
        [HttpGet("{authorId:int}")]
        public ActionResult<PagedResult<RequestResponseNotificationDto>> GetByAuthorId(int authorId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _requestResponseNotificationService.GetByAuthorId(authorId, page, pageSize);
            return CreateResponse(result);
        }

        [Authorize(Policy = "administratorPolicy")]
        [HttpPost]
        public ActionResult<RequestResponseNotificationDto> Create([FromBody] RequestResponseNotificationDto notification)
        {
            var result = _requestResponseNotificationService.Create(notification);
            return CreateResponse(result);
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _requestResponseNotificationService.Delete(id);
            return CreateResponse(result);
        }
    }
}
