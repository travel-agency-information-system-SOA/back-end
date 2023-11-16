using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
   
        [Route("api/administration/publicTourPoint")]
        public class PublicTourPointController : BaseApiController
        {
            private readonly IPublicTourPointService _tourPointService;

            public PublicTourPointController(IPublicTourPointService tourPointService)
            {
                _tourPointService = tourPointService;
            }
        [HttpGet]
            public ActionResult<PagedResult<TourPointDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
            {
                var result = _tourPointService.GetPaged(page, pageSize);
                return CreateResponse(result);
            }

        [HttpPost("createPublicTourPoint/{requestId:int}/{tourPointId:int}/{comment}")]
        public ActionResult<TourPointDto> CreatePublicTourPointAndAcceptRequest(int requestId, int tourPointId,string comment)
        {
            var result = _tourPointService.CreatePublicTourPointAndAcceptRequest(requestId, tourPointId,comment);
            return CreateResponse(result);
        }








    }

    }

