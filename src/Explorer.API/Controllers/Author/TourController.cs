using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
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
			var result = _tourService.Create(tour);
			return CreateResponse(result);
		}

		[HttpGet("{id:int}")]

		public ActionResult<List<TourDTO>> GetByUserId(int userId)
		{
			var result = _tourService.GetByUserId(userId);
			return CreateResponse(result);
		}
	}
}
