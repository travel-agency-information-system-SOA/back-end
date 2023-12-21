using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Statistics
{
    //[Authorize(Policy = "touristPolicy")]
    [Route("api/mileage")]
    public class UserMileageController : BaseApiController
    {
        private readonly IUserMileageService _userMileageService;

        public UserMileageController(IUserMileageService userMileageService)
        {
            _userMileageService = userMileageService;
        }

        [HttpGet("getByUser/{userId:int}")]
        public ActionResult<PagedResult<UserMileageDto>> GetActiveByUser(int userId)
        {
            var mileage = _userMileageService.GetMileageByUser(userId);
            return CreateResponse(mileage);
        }

        [HttpGet("getAllSorted")]
        public ActionResult<PagedResult<UserMileageDto>> GetAllSorted()
        {
            var mileages = _userMileageService.GetAllSorted();
            return mileages;
        }

        
    }
}
