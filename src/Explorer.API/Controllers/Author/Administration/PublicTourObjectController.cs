using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



namespace Explorer.API.Controllers.Author.Administration
{
    [Route("api/administration/publicTourObject")]
    public class PublicTourObjectController : BaseApiController
    {

        private readonly IPublicTourObjectService _tourObjectService;

        public PublicTourObjectController(IPublicTourObjectService tourObjectService)
        {
            _tourObjectService = tourObjectService;
        }
        [HttpGet]
        public ActionResult<PagedResult<TourObjectDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourObjectService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost("createPublicTourObject/{requestId:int}/{tourPointId:int}/{comment}")]
        public ActionResult<TourObjectDto> CreatePublicTourObjectAndAcceptRequest(int requestId, int tourObjectId, string comment)
        {
            var result = _tourObjectService.CreatePublicTourObjectAndAcceptRequest(requestId, tourObjectId, comment);
            return CreateResponse(result);
        }
    }
}
