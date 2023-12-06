using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{

    [Route("api/administration/object")]
    public class ObjectController : BaseApiController
    {
        private readonly ITourObjectService _objectService;

        public ObjectController(ITourObjectService objectService)
        {
            _objectService = objectService;
        }

        [HttpGet]
        public ActionResult<PagedResult<TourObjectDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _objectService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<TourObjectDto> Create([FromBody] TourObjectDto tourObject)
        {
            var result = _objectService.Create(tourObject);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<TourObjectDto> Update([FromBody] TourObjectDto tourObject)
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

		[HttpGet("{id:int}")]

		public ActionResult<TourObjectDto> GetById(int id)
		{
			var result = _objectService.Get(id);
			return CreateResponse(result);
		}
	}
}
