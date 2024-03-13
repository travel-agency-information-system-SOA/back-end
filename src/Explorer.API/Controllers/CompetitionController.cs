using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Authoring;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers
{
	[Route("api/competition")]
	public class CompetitionController : BaseApiController
	{
		private readonly ICompetitionService _competitionService;

		public CompetitionController(ICompetitionService competitionService)
		{
			_competitionService = competitionService;
		}

		[HttpPost]
		public ActionResult<CompetitionDto> Create([FromBody] CompetitionDto competition)
		{
			

			var result = _competitionService.Create(competition);

			return CreateResponse(result);
		}

		[HttpGet("allCompetitions")]
		public ActionResult<PagedResult<CompetitionDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
		{
			var result = _competitionService.GetAll(page, pageSize);
			return CreateResponse(result);
		}

        [HttpGet("getAllCompetitionAuthorId/{id}")]
        public ActionResult<PagedResult<CompetitionDto>> GetAllCompetitionAuthorId(int id, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _competitionService.GetAllCompetitionAuthorId(page, pageSize, id);
            return CreateResponse(result);
        }
    }
}
