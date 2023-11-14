using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{

    //[Authorize(Policy = "authorPolicy")]
    [Route("api/administration/message")]
    public class ProblemMessageController : BaseApiController
    {
            private readonly IProblemMessageService _problemService;

            public ProblemMessageController(IProblemMessageService objectService)
            {
                _problemService = objectService;
            }

            [HttpGet]
            public ActionResult<PagedResult<ProblemMessageDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
            {
                var result = _problemService.GetPaged(page, pageSize);
                return CreateResponse(result);
            }

            [HttpPost]
            public ActionResult<ProblemMessageDto> Create([FromBody] ProblemMessageDto message)
            {
                var result = _problemService.Create(message);
                return CreateResponse(result);
            }

            [HttpPut("{id:int}")]
            public ActionResult<ProblemMessageDto> Update([FromBody] ProblemMessageDto message)
            {
                var result = _problemService.Update(message);
                return CreateResponse(result);
            }

            [HttpDelete("{id:int}")]
            public ActionResult Delete(int id)
            {
                var result = _problemService.Delete(id);
                return CreateResponse(result);
            }
            
            [HttpGet("{id:int}")]

            public ActionResult<PagedResult<ProblemMessageDto>> GetAllByProblemId(int id, [FromQuery] int page, [FromQuery] int pageSize)
            {
                var result = _problemService.GetAllByProblemId(id, page, pageSize);
                return CreateResponse(result);
            }

    }
}
