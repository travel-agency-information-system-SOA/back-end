using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{

    [Route("api/problem")]
    public class ProblemController : BaseApiController
    {
        private readonly IProblemService _problemService;


        public ProblemController(IProblemService problemService)
        {
            _problemService = problemService;
        }

        //[Authorize(Policy = "touristPolicy, administratorPolicy")]
        [HttpGet("byTourist/{userId:int}")]
        public ActionResult<List<ProblemDto>> GetByTouristId(int userId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _problemService.GetByTouristId(userId, page, pageSize);
            return CreateResponse(result);
        }

       // [Authorize(Policy = "authorPolicy")]
       // [Authorize(Policy = "administratorPolicy")]
        [HttpGet("byGuide/{guideId:int}")]
        public ActionResult<List<ProblemDto>> GetByGuideId(int guideId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _problemService.GetByGuideId(guideId, page, pageSize);
            return CreateResponse(result);
        }


        [Authorize(Policy = "administratorPolicy")]
        [HttpGet]
        public ActionResult<PagedResult<ProblemDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _problemService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }


        [Authorize(Policy = "touristPolicy")]
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
