using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    [Authorize(Policy = "authorPolicy")]
    [Route("api/administration/objInTour")]
    public class ObjInTourController : BaseApiController
    {
        private readonly IObjInTourService _objectService;

        public ObjInTourController(IObjInTourService objectService)
        {
            _objectService = objectService;
        }

        [HttpGet]
        public ActionResult<PagedResult<ObjInTourDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _objectService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ObjInTourDto> Create([FromBody] ObjInTourDto tourObject)
        {
            var result = _objectService.Create(tourObject);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ObjInTourDto> Update([FromBody] ObjInTourDto tourObject)
        {
            var result = _objectService.Update(tourObject);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _objectService.Delete(id);
            return CreateResponse(result);
        }

		[HttpGet("{tourId:int}")]

		public ActionResult<List<TourObjectDto>> GetObjectsByTourId(int tourId)
		{
			var result = _objectService.GetObjectsByTourId(tourId);
			return CreateResponse(result);
		}
	}
}
