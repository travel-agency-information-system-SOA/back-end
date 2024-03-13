using Explorer.Payments.API.Dtos;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Explorer.API.Controllers
{

    [Route("api/follower")]
    public class FollowerController : BaseApiController
    {
        private readonly IFollowerService _followerService;

        public FollowerController(IFollowerService followerService)
        {
            _followerService = followerService;
        }


        [HttpPost]
        public ActionResult<FollowerDto> Create([FromBody] FollowerDto follower)
        {
            var result = _followerService.Create(follower);

            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _followerService.Delete(id);
            return CreateResponse(result);
        }


        [HttpPut("{id:int}")]
        public ActionResult<FollowerDto> Update([FromBody] FollowerDto followerDto)
        {
            var result = _followerService.Update(followerDto);
            return CreateResponse(result);
        }

        [HttpGet("{userId:int}")]
        public ActionResult<List<FollowerDto>> GetByUserId(int userId)
        {
            var result = _followerService.GetByUserId(userId);
            return CreateResponse(result);
        }


    }
}
