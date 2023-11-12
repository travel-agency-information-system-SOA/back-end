using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourExecutionsDTO;
using Explorer.Tours.API.Public.TourExecuting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.TourExecution
{
    //[Authorize(Policy = "touristPolicy")]
    [Route("api/tourExecution")]
    public class TourExecutionController : BaseApiController
    {
        private readonly ITourExecutionService _tourExecutionService;

        public TourExecutionController(ITourExecutionService service)
        {
            _tourExecutionService = service;
        }

        [HttpGet("{tourExecutionId:int}")]
        public ActionResult<PagedResult<TourExecutionDto>> GetById(int tourExecutionId)
        {
            var result = _tourExecutionService.GetById(tourExecutionId);
            return CreateResponse(result);
        }

        [HttpPut("{tourExecutionId}/update-position/{longitude}/{latitude}")]
        public IActionResult UpdatePosition(int tourExecutionId, int longitude, int latitude)
        {
            try
            {
                _tourExecutionService.UpdatePosition(tourExecutionId, longitude, latitude);
                return Ok("Position updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error updating position: {ex.Message}");
            }
        }

        [HttpPost("{tourExecutionId}/complete/{tourPointId}")]
        public IActionResult CompleteTourPoint(int tourExecutionId, int tourPointId)
        {
            try
            {
                // Call the service method to complete the tour point
                _tourExecutionService.CompleteTourPoint(tourExecutionId, tourPointId);

                // Return a successful response
                return Ok("Tour point completed successfully.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
                return StatusCode(500, "An error occurred while completing the tour point.");
            }
        }

    }
}
