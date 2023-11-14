using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
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

        //[Authorize(Policy = "touristPolicy")]
        [HttpGet("byTourist/{userId:int}")]
        public ActionResult<List<ProblemDto>> GetByTouristId(int userId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _problemService.GetByTouristId(userId, page, pageSize);
            return CreateResponse(result);
        }

        //[Authorize(Policy = "authorPolicy")]
        [HttpGet("byGuide/{guideId:int}")]
        public ActionResult<List<ProblemDto>> GetByGuideId(int guideId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _problemService.GetByGuideId(guideId, page, pageSize);
            return CreateResponse(result);
        }

        [HttpGet("byUnreadMessages/{idUser:int}")]
        public int IsThereUnreadMessages(int idUser, [FromQuery] int page, [FromQuery] int pageSize)
        {
            int id = _problemService.IsThereUnreadMessages(idUser, page, pageSize);
            return id;
        }

        //[Authorize(Policy = "administratorPolicy")]
        [HttpGet("unsolved")]
        public ActionResult<PagedResult<ProblemDto>> GetUnsolvedProblems([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _problemService.GetUnsolvedProblems(page, pageSize);
            return CreateResponse(result);
        }

        //[Authorize(Policy = "administratorPolicy")]
        [HttpGet]
        public ActionResult<PagedResult<ProblemDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _problemService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }


        //[Authorize(Policy = "touristPolicy")]
        [HttpPost]
        public ActionResult<ProblemDto> Create([FromBody] ProblemDto problem)
        {
            var result = _problemService.Create(problem);
            return CreateResponse(result);
        }

        [HttpPut("{id:int}")]
        public ActionResult<ProblemDto> Update([FromBody] ProblemDto problem)
        {
            var result = _problemService.Update(problem);
            return CreateResponse(result);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _problemService.Delete(id);
            return CreateResponse(result);
        }

        [HttpGet("closestDeadline/{id:int}")]
        public ActionResult<ProblemDto> getGuideProblemWithClosestDeadline(int id, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _problemService.getGuideProblemWithClosestDeadline(id, page, pageSize);
            return CreateResponse(result);
        }

    }
}
