using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.EncounterExecution
{
    //[Authorize(Policy = "touristPolicy")]
    [Route("api/encounterExecution")]
    public class EncounterExecutionController:BaseApiController
    {
        private readonly IEncounterExecutionService _encounterExecutionService;

        public EncounterExecutionController(IEncounterExecutionService service)
        {
            _encounterExecutionService = service;
        }

        [HttpGet]
        public ActionResult<PagedResult<EncounterExecutionDto>> GePaged([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _encounterExecutionService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
        [HttpPost]
        public ActionResult<EncounterExecutionDto> Create([FromBody] EncounterExecutionDto encounterExecution)
        {
            var result = _encounterExecutionService.Create(encounterExecution);
            return CreateResponse(result);
        }
        [HttpPut("{id:int}")]
        public ActionResult<EncounterExecutionDto> Update([FromBody] EncounterExecutionDto encounterExecution)
        {
            var result = _encounterExecutionService.Update(encounterExecution);
            return CreateResponse(result);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _encounterExecutionService.Delete(id);
            return CreateResponse(result);
        }
    }
}
