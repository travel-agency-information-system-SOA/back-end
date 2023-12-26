using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
    [Route("api/user")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("getById/{userId}")]
        public ActionResult<UserDto> Get(int userId)
        {
            var result = _userService.Get(userId);
            return CreateResponse(result);
        }

        [HttpGet("confirm-account")]
        public ActionResult<UserDto> ConfirmRegistration(string token)
        {
            var result = _userService.ConfirmRegistration(token);
            return CreateResponse(result);
        }
    }
}
