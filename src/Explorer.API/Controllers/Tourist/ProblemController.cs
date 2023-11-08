using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist
{

    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/problem")]

    public class ProblemController : BaseApiController
    {
        private readonly IProblemService _problemService;
     

       public ProblemController(IProblemService problemService)
        {
            _problemService = problemService;
        }


        [HttpGet("byTourist/{userId:int}")]
        public ActionResult<List<ProblemDto>> GetByTouristId(int userId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _problemService.GetByTouristId(userId, page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("byTour/{tourId:int}")]
        public ActionResult<List<ProblemDto>> GetByTourtId(int tourId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _problemService.GetByTourId(tourId, page, pageSize);
            return CreateResponse(result);
        }
    
        [HttpGet]
        public ActionResult<PagedResult<ProblemDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _problemService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        [HttpPost]
        public ActionResult<ProblemDto> Create([FromBody] ProblemDto problem)
        {
            var result = _problemService.Create(problem);
            return CreateResponse(result);
        }
        [HttpPut]
        public ActionResult<ProblemDto> Update([FromBody] ProblemDto problem)
        {
            var result = _problemService.Update(problem);
            return CreateResponse(result);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var result = _problemService.Delete(id);
            return CreateResponse(result);
        }

    }
}
