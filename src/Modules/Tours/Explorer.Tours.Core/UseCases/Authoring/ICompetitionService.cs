using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Authoring
{
	public interface ICompetitionService 
	{
		Result<CompetitionDto> Create(CompetitionDto competitionDto);

		Result<PagedResult<CompetitionDto>> GetAll(int page, int pageSize);

        Result<PagedResult<CompetitionDto>> GetAllCompetitionAuthorId(int page, int pageSize, int id);
    }
}
