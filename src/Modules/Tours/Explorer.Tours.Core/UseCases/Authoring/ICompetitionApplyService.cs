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
	public interface ICompetitionApplyService
	{
		Result<CompetitionApplyDto> Create(CompetitionApplyDto competitionApplyDto);

        Result<PagedResult<CompetitionApplyDto>> GetAppliesByCompId(int tourId);

        Result<PagedResult<CompetitionApplyDto>> GetWinnerByCompId(int comId);

        Result<CompetitionApplyDto> Update(CompetitionApplyDto apply);
    }
}
