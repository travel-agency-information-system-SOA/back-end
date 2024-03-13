using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{
    //[Authorize(Policy = "administratorPolicy")]
    [Route("api/encounters/tourKeyPointEncounter")]
    public class TourKeyPointEncounterController : BaseApiController
    {
        private readonly ITourKeyPointEncounterService _tourKeyPointEncounterService;
        public TourKeyPointEncounterController(ITourKeyPointEncounterService tourKeyPointEncounterService)
        {
            _tourKeyPointEncounterService = tourKeyPointEncounterService;
        }

        [HttpGet]
        public ActionResult<PagedResult<EncounterDto>> GePaged([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourKeyPointEncounterService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
        [HttpPost]
        public ActionResult<TourKeyPointEncounterDto> Create([FromBody] TourKeyPointEncounterDto tourKeyPointEncounter)
        {
            var result = _tourKeyPointEncounterService.Create(tourKeyPointEncounter);
            return CreateResponse(result);
        }
        [HttpPut("{id:int}")]
        public ActionResult<TourKeyPointEncounterDto> Update([FromBody] TourKeyPointEncounterDto tourKeyPointEncounter)
        {
            var result = _tourKeyPointEncounterService.Update(tourKeyPointEncounter);
            return CreateResponse(result);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourKeyPointEncounterService.Delete(id);
            return CreateResponse(result);
        }
    }
}
