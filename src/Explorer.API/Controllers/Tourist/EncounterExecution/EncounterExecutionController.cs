using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.UseCases;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.EncounterExecution
{
    //[Authorize(Policy = "touristPolicy")]
    [Route("api/encounterExecution")]
    public class EncounterExecutionController:BaseApiController
    {
        private readonly IEncounterExecutionService _encounterExecutionService;
        private readonly ISocialEncounterService _socialEncounterService;
        private readonly IHiddenLocationEncounterService _hiddenLocationEncounterService;

        public EncounterExecutionController(IEncounterExecutionService service, ISocialEncounterService socialEncounterService, IHiddenLocationEncounterService hiddenLocationEncounterService)
        {
            _encounterExecutionService = service;
            _socialEncounterService = socialEncounterService;
            _hiddenLocationEncounterService = hiddenLocationEncounterService;
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

        [HttpGet("checkSocialEncounter/{encounterId:int}")]
        public PagedResult<EncounterExecutionDto> CheckSocialEncounter(int encounterId)
        {
            _socialEncounterService.CheckSocialEncounter(encounterId);

            List<EncounterExecutionDto> result = new List<EncounterExecutionDto>();
            result = _encounterExecutionService.GetAllExecutionsByEncounter(encounterId);

            return new PagedResult<EncounterExecutionDto>(result, result.Count);
        }

        [HttpGet("getActive/{userId:int}")]
        public ActionResult<PagedResult<EncounterExecutionDto>> GetActiveByUser(int userId)
        {
            var execution = _encounterExecutionService.GetExecutionByUser(userId);
            return CreateResponse(execution);
        }

        [HttpGet("checkHidden/{executionId:int}/{encounterId:int}")]
        public ActionResult<bool> GetBooleanValue(int executionId, int encounterId)
        {
            var result = _hiddenLocationEncounterService.CheckHiddenLocationEncounter(executionId, encounterId);

            return Ok(result);
        }

        [HttpGet("completeExecution/{userId:int}")]
        public ActionResult<EncounterExecutionDto> CompleteExecution(int userId)
        {
            Result<EncounterExecutionDto> execution = _encounterExecutionService.CompleteEncounter(userId);
            return CreateResponse(execution);
        }


    }
}
