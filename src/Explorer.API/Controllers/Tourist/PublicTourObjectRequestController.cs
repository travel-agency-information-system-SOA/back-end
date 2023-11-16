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
    [Route("api/tourist/publicTourObjectRequest")]
    public class PublicTourObjectRequestController: BaseApiController
    {
        private readonly ITourObjectRequestService _requestService;
        public PublicTourObjectRequestController(ITourObjectRequestService tourObjectRequestService)
        {
            _requestService = tourObjectRequestService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourObjectRequestDto>> GetAll()
        {
            var result = _requestService.GetPaged(0, 0);
            return CreateResponse(result);
        }


        [HttpPost("createRequest/{tourObjectId:int}/{authorId:int}")]  //na klikk dugmeta proglasi javnom
        public ActionResult<TourObjectRequestDto> Create(int tourObjectId, int authorId)
        {
            var result = _requestService.Create(tourObjectId, authorId);
            return CreateResponse(result);
        }

        [HttpPut("rejectRequest/{id:int}/{comment}")]
        public ActionResult<TourObjectRequestDto> RejectRequest(int id, string comment)
        {
            var result = _requestService.RejectRequest(id, comment);
            return CreateResponse(result);
        }
    }
}
