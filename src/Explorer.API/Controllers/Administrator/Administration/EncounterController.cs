using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration;

[Authorize(Policy = "administratorPolicy")]
[Route("api/encounters")]
public class EncounterController : BaseApiController
{
    private readonly IEncounterService _encounterService;
    public EncounterController(IEncounterService encounterService)
    {
        _encounterService = encounterService;
    }

    [HttpGet]
    public ActionResult<PagedResult<EncounterDto>> GePaged([FromQuery] int page, [FromQuery] int pageSize)
    {
        var result = _encounterService.GetPaged(page, pageSize);
        return CreateResponse(result);
    }
    [HttpPost]
    public ActionResult<EncounterDto> Create([FromBody] EncounterDto encounter)
    {
        var result = _encounterService.Create(encounter);
        return CreateResponse(result);
    }
    [HttpPut("{id:int}")]
    public ActionResult<EncounterDto> Update([FromBody] EncounterDto encounter)
    {
        var result = _encounterService.Update(encounter);
        return CreateResponse(result);
    }
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var result = _encounterService.Delete(id);
        return CreateResponse(result);
    }
}
