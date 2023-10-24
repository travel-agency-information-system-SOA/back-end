using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author
{
	[Authorize(Policy = "authorPolicy")]
	[Route("api/administration/tour")]
	public class TourController : BaseApiController
	{
		private readonly ITourService _tourService;

		public TourController(ITourService tourService)
		{
			_tourService = tourService;
		}

		[HttpPost]
		public ActionResult<TourDTO> Create([FromBody] TourDTO tour) 
		{
		
				tour.Status = "Draft";
				tour.Price = 0;

				var result = _tourService.Create(tour);
				return CreateResponse(result);
			
		}

		[HttpGet("{userId:int}")]

		public ActionResult<List<TourDTO>> GetByUserId(int userId,[FromQuery] int page,[FromQuery] int pageSize)
		{
			var result = _tourService.GetByUserId(userId, page, pageSize);
			return CreateResponse(result);
		}

		[HttpDelete("{id:int}")]
		public ActionResult Delete(int id)
		{
			var result = _tourService.Delete(id);
			return CreateResponse(result);
		}
	}
}
