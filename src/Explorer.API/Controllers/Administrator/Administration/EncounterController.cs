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
    private readonly IHiddenLocationEncounterService _hiddenLocationEncounterService;

    public EncounterController(IEncounterService encounterService, IHiddenLocationEncounterService hiddenLocationEncounterService)
    {
        _encounterService = encounterService;
        _hiddenLocationEncounterService = hiddenLocationEncounterService;
    }

    [HttpGet]
    public ActionResult<PagedResult<EncounterDto>> GetAllEncounters([FromQuery] int page, [FromQuery] int pageSize)
    {
        var result = _encounterService.GetPaged(page, pageSize);
        return CreateResponse(result);
    }
    [HttpGet("hiddenLocation")]
    public ActionResult<PagedResult<HiddenLocationEncounterDto>> GetAllHiddenLocationEncounters([FromQuery] int page, [FromQuery] int pageSize)
    {
        var result = _hiddenLocationEncounterService.GetPaged(page, pageSize);
        return CreateResponse(result);
    }
    [HttpPost]
    public ActionResult<EncounterDto> Create([FromBody] EncounterDto encounter)
    {
        var result = _encounterService.Create(encounter);
        return CreateResponse(result);
    }
    [HttpPost("hiddenLocation")]
    public ActionResult<HiddenLocationEncounterDto> Create([FromBody] WholeHiddenLocationEncounterDto wholeEncounter)
    {
        EncounterDto encounterDto = new EncounterDto();
        encounterDto.Name = wholeEncounter.Name;
        encounterDto.Description = wholeEncounter.Description;
        encounterDto.XpPoints = wholeEncounter.XpPoints;
        encounterDto.Status = wholeEncounter.Status;
        encounterDto.Type = wholeEncounter.Type;
        encounterDto.Longitude = wholeEncounter.Longitude;
        encounterDto.Latitude = wholeEncounter.Latitude;
        var baseEncounter = _encounterService.Create(encounterDto);

        HiddenLocationEncounterDto hiddenLocationEncounterDto = new HiddenLocationEncounterDto();
        hiddenLocationEncounterDto.EncounterId = baseEncounter.Value.Id;
        hiddenLocationEncounterDto.ImageLatitude = wholeEncounter.ImageLatitude;
        hiddenLocationEncounterDto.ImageLongitude = wholeEncounter.ImageLongitude;
        hiddenLocationEncounterDto.ImageURL = wholeEncounter.ImageURL;
        hiddenLocationEncounterDto.DistanceTreshold = wholeEncounter.DistanceTreshold;

        var result = _hiddenLocationEncounterService.Create(hiddenLocationEncounterDto);
        return CreateResponse(result);
    }
    [HttpPut]
    public ActionResult<EncounterDto> Update([FromBody] EncounterDto encounter)
    {

        var result = _encounterService.Update(encounter);
        return CreateResponse(result);
    }
    [HttpPut("hiddenLocation")]
    public ActionResult<HiddenLocationEncounterDto> Update([FromBody] WholeHiddenLocationEncounterDto wholeEncounter)
    {
        EncounterDto encounterDto = new EncounterDto();
        encounterDto.Id = wholeEncounter.EncounterId;
        encounterDto.Name = wholeEncounter.Name;
        encounterDto.Description = wholeEncounter.Description;
        encounterDto.XpPoints = wholeEncounter.XpPoints;
        encounterDto.Status = wholeEncounter.Status;
        encounterDto.Type = wholeEncounter.Type;
        encounterDto.Longitude = wholeEncounter.Longitude;
        encounterDto.Latitude = wholeEncounter.Latitude;
        var baseEncounter = _encounterService.Update(encounterDto);

        HiddenLocationEncounterDto hiddenLocationEncounterDto = new HiddenLocationEncounterDto();
        hiddenLocationEncounterDto.Id = hiddenLocationEncounterDto.Id;
        hiddenLocationEncounterDto.EncounterId = baseEncounter.Value.Id;
        hiddenLocationEncounterDto.ImageLatitude = wholeEncounter.ImageLatitude;
        hiddenLocationEncounterDto.ImageLongitude = wholeEncounter.ImageLongitude;
        hiddenLocationEncounterDto.ImageURL = wholeEncounter.ImageURL;
        hiddenLocationEncounterDto.DistanceTreshold = wholeEncounter.DistanceTreshold;

        var result = _hiddenLocationEncounterService.Update(hiddenLocationEncounterDto);
        return CreateResponse(result);  
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var result = _encounterService.Delete(id);
        return CreateResponse(result);
    }
    [HttpDelete("hiddenLocation/{baseEncounterId:int}/{hiddenLocationEncounterId:int}")]
    public ActionResult DeleteHiddenLocationEncounter(int baseEncounterId, int hiddenLocationEncounterId)
    {
        var baseEncounter = _encounterService.Delete(baseEncounterId);
        var result = _hiddenLocationEncounterService.Delete(hiddenLocationEncounterId);
        return CreateResponse(result);
    }


}
