using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.EncounterExecution
{
    [Route("api/encounters")]

    public class ExecutionsController : BaseApiController
    {
        private readonly IEncounterExecutionService _encounterExecutionService;

        public ExecutionsController(IEncounterExecutionService encounterExecutionService) { }

        [HttpGet("complete/{userId:int}")]
        public ActionResult<PagedResult<EncounterExecutionDto>> Complete(int userId)
        {
            var execution = _encounterExecutionService.CompleteEncounter(userId);
            return CreateResponse(execution);
        }
    }
}
