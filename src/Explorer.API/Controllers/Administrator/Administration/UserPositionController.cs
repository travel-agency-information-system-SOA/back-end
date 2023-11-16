using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourExecutionsDTO;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Administrator.Administration
{


    [Route("api/administration/userPosition")]

    public class UserPositionController : BaseApiController
    {
        private readonly IUserPositionService _userPositionService;

        public UserPositionController(IUserPositionService userPositionService)
        {
            _userPositionService = userPositionService;
        }

        [HttpGet]
        public ActionResult<PagedResult<UserPositionDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _userPositionService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<UserPositionDto> Create([FromBody] UserPositionDto userPosition)
        {
            var result = _userPositionService.Create(userPosition);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<UserPositionDto> Update([FromBody] UserPositionDto userPosition)
        {
            var result = _userPositionService.Update(userPosition);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _userPositionService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("{userId:int}")]
        public ActionResult<PagedResult<UserPositionDto>> GetByUserId(int userId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _userPositionService.GetByUserId(userId, page, pageSize);
            return CreateResponse(result);
        }
    }
}
