using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Stakeholders.API.Dtos;

namespace Explorer.API.Controllers.Tourist
{
    [Route("api/tourist/publicTourPointRequest")]
    public class PublicTourPointRequestController :BaseApiController
    {
        private readonly ITourPointRequestService _requestService;
        public PublicTourPointRequestController(ITourPointRequestService tourPointRequestService)
        {
            _requestService = tourPointRequestService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourPointRequestDto>> GetAll()
        {
            var result = _requestService.GetPaged(0, 0);
            return CreateResponse(result);
        }


        [HttpPost("createRequest/{tourPointId:int}/{authorId:int}")]  //na klikk dugmeta proglasi javnom
        public ActionResult<TourPointRequestDto> Create( int tourPointId, int authorId)
        {
            var result = _requestService.Create(tourPointId,authorId);
            return CreateResponse(result);
        }

        [HttpPut("rejectRequest/{id:int}/{comment}")] 
        public ActionResult<TourPointRequestDto> RejectRequest(int id,string comment)
        {
            var result = _requestService.RejectRequest(id,comment);
            return CreateResponse(result);
        }



    }
}
