using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;


namespace Explorer.API.Controllers
{

    [Route("api/followerMessage")]
    public class FollowerMessageController : BaseApiController
    {

        private readonly IFollowerMessageService _messageService;

        public FollowerMessageController(IFollowerMessageService followerService)
        {
            _messageService = followerService;
        }


        [HttpPost]
        public ActionResult<FollowerMessageDto> Create([FromBody] FollowerMessageDto message)
        {
            var result = _messageService.Create(message);

            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _messageService.Delete(id);
            return CreateResponse(result);
        }


        [HttpPut("{id:int}")]
        public ActionResult<FollowerMessageDto> Update([FromBody] FollowerMessageDto messageDto)
        {
            var result = _messageService.Update(messageDto);
            return CreateResponse(result);
        }

        [HttpPut("markAsRead/{id:int}")]
        public ActionResult<FollowerMessageDto> MarkAsRead([FromBody] FollowerMessageDto messageDto)
        {
            var result = _messageService.MarkAsRead(messageDto);
            return CreateResponse(result);
        }

        [HttpGet("{followerId:int}")]
        public ActionResult<List<FollowerMessageDto>> GetByFollowerId(int followerId)
        {
            var result = _messageService.GetByFollowerId(followerId);
            return CreateResponse(result);
        }

    }
}
