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

        [HttpGet("getAllSortedByMonth")]
        public ActionResult<PagedResult<UserMileageDto>> GetAllSortedByMonth()
        {
            var mileages = _userMileageService.GetAllSortedByMonth();
            return mileages;
        }

        [HttpPut("updateUserMileageByMonth/{userId}")]
        public IActionResult UpdateMileageByMonth(int userId)
        {
            try
            {
                _userMileageService.UpdateMileageByMonth(userId);
                return Ok("Mileage updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPut("updateAllMileagesByMonth")]
        public IActionResult UpdateAllUserMileagesByMonth(int userId)
        {
            try
            {
                _userMileageService.UpdateAllUserMileages();
                return Ok("Mileage updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


    }
}
