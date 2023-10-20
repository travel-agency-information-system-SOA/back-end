using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Club
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/club/club")]
    public class ClubController : BaseApiController
    {
        private readonly IClubService _clubManagementService;
        public ClubController(IClubService clubManagementService)
        {
            _clubManagementService = clubManagementService;
        }
        [HttpGet]
        public ActionResult<PagedResult<ClubDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _clubManagementService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<EquipmentDto> Create([FromBody] ClubDto clubManagement)
        {
            var result = _clubManagementService.Create(clubManagement);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<EquipmentDto> Update([FromBody] ClubDto clubManagement)
        {
            var result = _clubManagementService.Update(clubManagement);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _clubManagementService.Delete(id);
            return CreateResponse(result);
        }
    }
}
