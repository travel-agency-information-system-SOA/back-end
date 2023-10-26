using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers;

//[Authorize(Policy = "touristPolicy")]
//[Authorize(Policy = "authorPolicy")]
[Route("api/profile")]
public class ProfileController : BaseApiController
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet("{userId}")]
    public ActionResult<UserProfileDto> Get([FromRoute] int userId)
    {
        var result = _profileService.Get(userId);
        return CreateResponse(result);
    }

    [HttpPut("{id:int}")]
    public ActionResult<UserProfileDto> Update([FromBody] UserProfileDto profile)
    {
        var result = _profileService.Update(profile);
        return CreateResponse(result);
    }
}
