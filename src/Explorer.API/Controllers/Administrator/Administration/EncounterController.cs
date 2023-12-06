using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration;

[Authorize(Policy = "administratorPolicy")]
[Route("api/encounters")]
public class EncounterController : BaseApiController
{
    private readonly IEncounterService _encounterService;
    private readonly ISocialEncounterService _socialEncounterService;
    public EncounterController(IEncounterService encounterService, ISocialEncounterService socialEncounterService)
    {
        _encounterService = encounterService;
        _socialEncounterService = socialEncounterService;
    }
     
    [HttpGet]
    public ActionResult<PagedResult<EncounterDto>> GetAllEncounters([FromQuery] int page, [FromQuery] int pageSize)
    {
        var result = _encounterService.GetPaged(page, pageSize);
        return CreateResponse(result);
    }

    [HttpGet("social")]
    public ActionResult<PagedResult<SocialEncounterDto>> GetAllSocialEncounters([FromQuery] int page, [FromQuery] int pageSize)
    {
        var result = _socialEncounterService.GetPaged(page, pageSize);
        return CreateResponse(result);
    }

    [HttpPost]
    public ActionResult<EncounterDto> Create([FromBody] EncounterDto encounter)
    {
        var result = _encounterService.Create(encounter);
        return CreateResponse(result);
    }
    [HttpPost("social")]
    public ActionResult<SocialEncounterDto> Create([FromBody] WholeSocialEncounterDto socialEncounter)
    {
        EncounterDto encounterDto = new EncounterDto();
        encounterDto.Name = socialEncounter.Name;
        encounterDto.Description = socialEncounter.Description;
        encounterDto.XpPoints = socialEncounter.XpPoints;
        encounterDto.Status = socialEncounter.Status;
        encounterDto.Type = socialEncounter.Type;
        encounterDto.Longitude = socialEncounter.Longitude;
        encounterDto.Latitude = socialEncounter.Latitude;
        var baseEncounter = _encounterService.Create(encounterDto);
        SocialEncounterDto socialEncounterDto = new SocialEncounterDto();
        socialEncounterDto.EncounterId = baseEncounter.Value.Id;
        socialEncounterDto.TouristsRequiredForCompletion = socialEncounter.TouristsRequiredForCompletion;
        socialEncounterDto.DistanceTreshold = socialEncounter.DistanceTreshold;
        socialEncounterDto.TouristIDs = socialEncounter.TouristIDs;
        var result = _socialEncounterService.Create(socialEncounterDto);
        return CreateResponse(result);
    }
    [HttpPut]
    public ActionResult<EncounterDto> Update([FromBody] EncounterDto encounter)
    {
        var result = _encounterService.Update(encounter);
        return CreateResponse(result);
    }
    [HttpPut("social")]
    public ActionResult<SocialEncounterDto> Update([FromBody] WholeSocialEncounterDto socialEncounter)
    {
        EncounterDto encounterDto = new EncounterDto();
        encounterDto.Id = socialEncounter.EncounterId;
        encounterDto.Name = socialEncounter.Name;
        encounterDto.Description = socialEncounter.Description;
        encounterDto.XpPoints = socialEncounter.XpPoints;
        encounterDto.Status = socialEncounter.Status;
        encounterDto.Type = socialEncounter.Type;
        encounterDto.Longitude = socialEncounter.Longitude;
        encounterDto.Latitude = socialEncounter.Latitude;
        var baseEncounter = _encounterService.Update(encounterDto);
        SocialEncounterDto socialEncounterDto = new SocialEncounterDto();
        socialEncounterDto.Id = socialEncounter.Id;
        socialEncounterDto.EncounterId = baseEncounter.Value.Id;
        socialEncounterDto.TouristsRequiredForCompletion = socialEncounter.TouristsRequiredForCompletion;
        socialEncounterDto.DistanceTreshold = socialEncounter.DistanceTreshold;
        socialEncounterDto.TouristIDs = socialEncounter.TouristIDs;
        var result = _socialEncounterService.Update(socialEncounterDto);
        return CreateResponse(result);
    }
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var result = _encounterService.Delete(id);
        return CreateResponse(result);
    }
    [HttpDelete("social/{baseEncounterId:int}/{socialEncounterId:int}")]
    public ActionResult Delete(int baseEncounterId, int socialEncounterId)
    {
        var baseEncounter = _encounterService.Delete(baseEncounterId);
        var result = _socialEncounterService.Delete(socialEncounterId);
        return CreateResponse(result);
    }
}
