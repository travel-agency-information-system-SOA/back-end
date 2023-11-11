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
        public ActionResult<PagedResult<TourExecutionDto>> GetById(int tourExecutionId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourExecutionService.GetById(tourExecutionId, page, pageSize);
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

    }
}
