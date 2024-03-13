using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{

   // [Authorize(Policy = "authorAndAdminPolicy")]

    [Route("api/administration/tourPoint")] 
    public class TourPointController : BaseApiController
    {
        private readonly ITourPointService _tourPointService;

        public TourPointController(ITourPointService tourPointService)
        {
            _tourPointService = tourPointService;
        }
        [Authorize(Policy = "authorPolicy")]
        [HttpGet]
        public ActionResult<PagedResult<TourPointDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _tourPointService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }
        
        [HttpPost]/*
        public (ActionResult<TourPointDto>, int) Create([FromBody] TourPointDto tourPoint)
        {
            var result = _tourPointService.Create(tourPoint);
            Console.WriteLine("rezultat id:" + result.Value.Id);

            return (CreateResponse(result), result.Value.Id);
        }*/
        
        public ActionResult<PagedResult<TourPointDto>> Create([FromBody] TourPointDto tourPoint)
        {
            var result = _tourPointService.Create(tourPoint);
            Console.WriteLine("rezultat id:" + result.Value.Id);
            
            return CreateResponse(result);
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpPut("{id:int}")]
        public ActionResult<TourPointDto> Update([FromBody] TourPointDto tourPoint)
        {
            var result = _tourPointService.Update(tourPoint);
            return CreateResponse(result);
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourPointService.Delete(id);
            return CreateResponse(result);
        }

        [Authorize(Policy = "touristAuthorPolicy")]
        [HttpGet("{tourId:int}")]

		public ActionResult<List<TourPointDto>> GetTourPointsByTourId(int tourId)
		{
			var result = _tourPointService.GetTourPointsByTourId(tourId);
			return CreateResponse(result);
		}

        [HttpGet("getById/{id:int}")]
        public ActionResult<TourPointDto> GetTourPointById(int id)
        {
            var result = _tourPointService.Get(id);
            return CreateResponse(Result.Ok(result));
        }

    }
}
