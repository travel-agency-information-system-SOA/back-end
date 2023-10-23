using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Marketplace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Marketplace
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/marketplace/preferences")]
    public class PreferencesController : BaseApiController
    {
        private readonly IPreferencesService _preferencesService;

        public PreferencesController(IPreferencesService preferencesService)
        {
            _preferencesService = preferencesService;
        }

        [HttpGet]
        public ActionResult<PagedResult<PreferencesDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _preferencesService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<PreferencesDto> Create([FromBody] PreferencesDto preferences)
        {
            var result = _preferencesService.Create(preferences);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<PreferencesDto> Update([FromBody] PreferencesDto preferences)
        {
            var result = _preferencesService.Update(preferences);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<PreferencesDto> Delete(int id)
        {
            var result = _preferencesService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<PreferencesDto> GetByUserId([FromQuery] int page, [FromQuery] int pageSize, int id)
        {
            var result = _preferencesService.GetByUserId(page, pageSize, id);
            return CreateResponse(result);
        }
    }
}
