using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/administration/tourPoint")] 
    public class TourPointController : BaseApiController
    {
        private readonly ITourPointService _tourPointService;

        public TourPointController(ITourPointService tourPointService)
        {
            _tourPointService = tourPointService;
        }

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


        [HttpPut("{id:int}")]
        public ActionResult<TourPointDto> Update([FromBody] TourPointDto tourPoint)
        {
            var result = _tourPointService.Update(tourPoint);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourPointService.Delete(id);
            return CreateResponse(result);
        }


		[HttpGet("{tourId:int}")]

		public ActionResult<List<TourPointDto>> GetTourPointsByTourId(int tourId)
		{
			var result = _tourPointService.GetTourPointsByTourId(tourId);
			return CreateResponse(result);
		}
	}
}
