using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Statistics
{
    [Route("api/tourMileage")]
    public class UserTourMileageController : BaseApiController
    {
        private readonly IUserTourMileageService _userTourMileageService;

        public UserTourMileageController(IUserTourMileageService userTourMileageService)
        {
            _userTourMileageService = userTourMileageService;
        }

        [HttpPost]
        public ActionResult<UserTourMileageDto> Create([FromBody] UserTourMileageDto userTourMileage)
        {
            var result = _userTourMileageService.Create(userTourMileage);
            return CreateResponse(result);
        }

        [HttpGet("getByUser/{userId:int}")]
        public ActionResult<PagedResult<UserTourMileageDto>> GetRecentMileageByUser(int userId)
        {
            var mileages = _userTourMileageService.GetRecentMileageByUser(userId);
            return mileages;
        }

        


    }
}
