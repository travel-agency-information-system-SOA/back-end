using Explorer.Blog.API.Dtos;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Encounters.API;
using Explorer.Encounters.API.Dtos;
using Explorer.Encounters.API.Public;
using Explorer.Encounters.Core.Domain;
using Explorer.Encounters.Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Explorer.API.Controllers.Administrator.Administration;

//[Authorize(Policy = "administratorPolicy")]
[Route("api/encounters")]
public class EncounterController : BaseApiController
{
    private readonly IEncounterService _encounterService;
    private readonly IHiddenLocationEncounterService _hiddenLocationEncounterService;

    private readonly ISocialEncounterService _socialEncounterService;
    public EncounterController(IEncounterService encounterService, ISocialEncounterService socialEncounterService, IHiddenLocationEncounterService hiddenLocationEncounterService)
    {
        _encounterService = encounterService;
        _socialEncounterService = socialEncounterService;
        _hiddenLocationEncounterService = hiddenLocationEncounterService;
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
        encounterDto.ShouldBeApproved = wholeEncounter.ShouldBeApproved;
        var baseEncounter = _encounterService.Create(encounterDto);
        if (!baseEncounter.IsSuccess)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, baseEncounter);
        }

        HiddenLocationEncounterDto hiddenLocationEncounterDto = new HiddenLocationEncounterDto();
        hiddenLocationEncounterDto.EncounterId = baseEncounter.Value.Id;
        hiddenLocationEncounterDto.ImageLatitude = wholeEncounter.ImageLatitude;
        hiddenLocationEncounterDto.ImageLongitude = wholeEncounter.ImageLongitude;
        hiddenLocationEncounterDto.ImageURL = wholeEncounter.ImageURL;
        hiddenLocationEncounterDto.DistanceTreshold = wholeEncounter.DistanceTreshold;

        var result = _hiddenLocationEncounterService.Create(hiddenLocationEncounterDto);
        if (!result.IsSuccess)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, result);
        }

        var wholeHiddenLocationEncounterDto = new WholeHiddenLocationEncounterDto();
        wholeHiddenLocationEncounterDto.EncounterId = baseEncounter.Value.Id;
        wholeHiddenLocationEncounterDto.Name = wholeEncounter.Name;
        wholeHiddenLocationEncounterDto.Description = wholeEncounter.Description;
        wholeHiddenLocationEncounterDto.XpPoints = wholeEncounter.XpPoints;
        wholeHiddenLocationEncounterDto.Status = wholeEncounter.Status;
        wholeHiddenLocationEncounterDto.Type = wholeEncounter.Type;
        wholeHiddenLocationEncounterDto.Latitude = wholeEncounter.Latitude;
        wholeHiddenLocationEncounterDto.Longitude = wholeEncounter.Longitude;
        wholeHiddenLocationEncounterDto.Id = result.Value.Id;
        wholeHiddenLocationEncounterDto.ImageURL = wholeEncounter.ImageURL;
        wholeHiddenLocationEncounterDto.ImageLatitude = wholeEncounter.ImageLatitude;
        wholeHiddenLocationEncounterDto.ImageLongitude = wholeEncounter.ImageLongitude;
        wholeHiddenLocationEncounterDto.DistanceTreshold = wholeEncounter.DistanceTreshold;
        wholeHiddenLocationEncounterDto.ShouldBeApproved = wholeEncounter.ShouldBeApproved;
        return StatusCode((int)HttpStatusCode.Created, wholeHiddenLocationEncounterDto);
    
    }
    [HttpPost("social")]
    public ActionResult<WholeSocialEncounterDto> Create([FromBody] WholeSocialEncounterDto socialEncounter)
    {
        EncounterDto encounterDto = new EncounterDto();
        encounterDto.Name = socialEncounter.Name;
        encounterDto.Description = socialEncounter.Description;
        encounterDto.XpPoints = socialEncounter.XpPoints;
        encounterDto.Status = socialEncounter.Status;
        encounterDto.Type = socialEncounter.Type;
        encounterDto.Longitude = socialEncounter.Longitude;
        encounterDto.Latitude = socialEncounter.Latitude;
        encounterDto.ShouldBeApproved = socialEncounter.ShouldBeApproved;
        var baseEncounter = _encounterService.Create(encounterDto);
        if (!baseEncounter.IsSuccess)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, baseEncounter);
        }
        SocialEncounterDto socialEncounterDto = new SocialEncounterDto();
        socialEncounterDto.EncounterId = baseEncounter.Value.Id;
        socialEncounterDto.TouristsRequiredForCompletion = socialEncounter.TouristsRequiredForCompletion;
        socialEncounterDto.DistanceTreshold = socialEncounter.DistanceTreshold;
        socialEncounterDto.TouristIDs = socialEncounter.TouristIDs;
        var result = _socialEncounterService.Create(socialEncounterDto);
        if (!result.IsSuccess)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, result);
        }

        var wholeSocialEncounterDto = new WholeSocialEncounterDto();
        wholeSocialEncounterDto.EncounterId = baseEncounter.Value.Id;
        wholeSocialEncounterDto.Name = socialEncounter.Name;
        wholeSocialEncounterDto.Description = socialEncounter.Description;
        wholeSocialEncounterDto.XpPoints = socialEncounter.XpPoints;
        wholeSocialEncounterDto.Status = socialEncounter.Status;
        wholeSocialEncounterDto.Type = socialEncounter.Type;
        wholeSocialEncounterDto.Latitude = socialEncounter.Latitude;
        wholeSocialEncounterDto.Longitude = socialEncounter.Longitude;
        wholeSocialEncounterDto.Id = result.Value.Id;
        wholeSocialEncounterDto.TouristsRequiredForCompletion = socialEncounter.TouristsRequiredForCompletion;
        wholeSocialEncounterDto.DistanceTreshold = socialEncounter.DistanceTreshold;
        wholeSocialEncounterDto.TouristIDs = socialEncounter.TouristIDs;
        wholeSocialEncounterDto.ShouldBeApproved = socialEncounter.ShouldBeApproved;

        return StatusCode((int)HttpStatusCode.Created, wholeSocialEncounterDto);
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
        encounterDto.ShouldBeApproved = wholeEncounter.ShouldBeApproved;
        var baseEncounter = _encounterService.Update(encounterDto);
        if (!baseEncounter.IsSuccess)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, baseEncounter);
        }

        HiddenLocationEncounterDto hiddenLocationEncounterDto = new HiddenLocationEncounterDto();
        hiddenLocationEncounterDto.Id = wholeEncounter.Id;
        hiddenLocationEncounterDto.EncounterId = baseEncounter.Value.Id;
        hiddenLocationEncounterDto.ImageLatitude = wholeEncounter.ImageLatitude;
        hiddenLocationEncounterDto.ImageLongitude = wholeEncounter.ImageLongitude;
        hiddenLocationEncounterDto.ImageURL = wholeEncounter.ImageURL;
        hiddenLocationEncounterDto.DistanceTreshold = wholeEncounter.DistanceTreshold;

        var result = _hiddenLocationEncounterService.Update(hiddenLocationEncounterDto);
        if (!result.IsSuccess)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, result);
        }
        var wholeHiddenLocationEncounterDto = new WholeHiddenLocationEncounterDto();
        wholeHiddenLocationEncounterDto.EncounterId = baseEncounter.Value.Id;
        wholeHiddenLocationEncounterDto.Name = wholeEncounter.Name;
        wholeHiddenLocationEncounterDto.Description = wholeEncounter.Description;
        wholeHiddenLocationEncounterDto.XpPoints = wholeEncounter.XpPoints;
        wholeHiddenLocationEncounterDto.Status = wholeEncounter.Status;
        wholeHiddenLocationEncounterDto.Type = wholeEncounter.Type;
        wholeHiddenLocationEncounterDto.Latitude = wholeEncounter.Latitude;
        wholeHiddenLocationEncounterDto.Longitude = wholeEncounter.Longitude;
        wholeHiddenLocationEncounterDto.Id = result.Value.Id;
        wholeHiddenLocationEncounterDto.ImageURL = wholeEncounter.ImageURL;
        wholeHiddenLocationEncounterDto.ImageLatitude = wholeEncounter.ImageLatitude;
        wholeHiddenLocationEncounterDto.ImageLongitude = wholeEncounter.ImageLongitude;
        wholeHiddenLocationEncounterDto.DistanceTreshold = wholeEncounter.DistanceTreshold;
        wholeHiddenLocationEncounterDto.ShouldBeApproved = wholeEncounter.ShouldBeApproved;

        return StatusCode((int)HttpStatusCode.NoContent, wholeHiddenLocationEncounterDto);
    }

    [HttpPut("social")]
    public ActionResult<WholeSocialEncounterDto> Update([FromBody] WholeSocialEncounterDto socialEncounter)
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
        encounterDto.ShouldBeApproved = socialEncounter.ShouldBeApproved;
        var baseEncounter = _encounterService.Update(encounterDto);
        if (!baseEncounter.IsSuccess)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, baseEncounter);
        }
        SocialEncounterDto socialEncounterDto = new SocialEncounterDto();
        socialEncounterDto.Id = socialEncounter.Id;
        socialEncounterDto.EncounterId = baseEncounter.Value.Id;
        socialEncounterDto.TouristsRequiredForCompletion = socialEncounter.TouristsRequiredForCompletion;
        socialEncounterDto.DistanceTreshold = socialEncounter.DistanceTreshold;
        socialEncounterDto.TouristIDs = socialEncounter.TouristIDs;
        var result = _socialEncounterService.Update(socialEncounterDto);
        if (!result.IsSuccess)
        {
            return StatusCode((int)HttpStatusCode.BadRequest, result);
        }

        var wholeSocialEncounterDto = new WholeSocialEncounterDto();
        wholeSocialEncounterDto.EncounterId = baseEncounter.Value.Id;
        wholeSocialEncounterDto.Name = socialEncounter.Name;
        wholeSocialEncounterDto.Description = socialEncounter.Description;
        wholeSocialEncounterDto.XpPoints = socialEncounter.XpPoints;
        wholeSocialEncounterDto.Status = socialEncounter.Status;
        wholeSocialEncounterDto.Type = socialEncounter.Type;
        wholeSocialEncounterDto.Latitude = socialEncounter.Latitude;
        wholeSocialEncounterDto.Longitude = socialEncounter.Longitude;
        wholeSocialEncounterDto.Id = result.Value.Id;
        wholeSocialEncounterDto.TouristsRequiredForCompletion = socialEncounter.TouristsRequiredForCompletion;
        wholeSocialEncounterDto.DistanceTreshold = socialEncounter.DistanceTreshold;
        wholeSocialEncounterDto.TouristIDs = socialEncounter.TouristIDs;
        wholeSocialEncounterDto.ShouldBeApproved = socialEncounter.ShouldBeApproved;

        return StatusCode((int)HttpStatusCode.NoContent, wholeSocialEncounterDto);
    }
    /*
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


    [HttpDelete("social/{baseEncounterId:int}/{socialEncounterId:int}")]
    public ActionResult Delete(int baseEncounterId, int socialEncounterId)
    {
        var baseEncounter = _encounterService.Delete(baseEncounterId);
        var result = _socialEncounterService.Delete(socialEncounterId);
        return CreateResponse(result);
    }
    */
    [HttpGet("getEncounter/{encounterId:int}")]
    public ActionResult<PagedResult<EncounterDto>> GetEncounter(int encounterId)
    {
        var encounter = _encounterService.GetEncounterById(encounterId);
        return CreateResponse(encounter);
    }
    
    [HttpDelete("{baseEncounterId:int}")]
    public ActionResult DeleteEncounter(int baseEncounterId)
    {
        var baseEncounter = _encounterService.Delete(baseEncounterId);

        if (baseEncounter.IsSuccess)
        {
            long socialEncounterId = _socialEncounterService.GetId(baseEncounterId);
            long hiddenLocationEncounterId = _hiddenLocationEncounterService.GetId(baseEncounterId);

            if (socialEncounterId != -1)
            {
                var result = _socialEncounterService.Delete((int)socialEncounterId);
                return CreateResponse(result);
            }
            else if (hiddenLocationEncounterId != -1)
            {
                var hiddenLocationResult = _hiddenLocationEncounterService.Delete((int)hiddenLocationEncounterId);
                return CreateResponse(hiddenLocationResult);
            }
        }

        // Handle the case when baseEncounterId doesn't match any specific type
        return CreateResponse(baseEncounter);
    }

    [HttpGet("hiddenLocation/{encounterId:int}")]
    public ActionResult<PagedResult<HiddenLocationEncounterDto>> GetHiddenLocationEncounterByEncounterId(int encounterId)
    {
        var hiddenLocationEncounter = _hiddenLocationEncounterService.GetHiddenLocationEncounterByEncounterId(encounterId);
        return CreateResponse(hiddenLocationEncounter);
    }

}
