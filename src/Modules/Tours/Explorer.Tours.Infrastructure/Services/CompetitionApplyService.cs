using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.Competitions;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.UseCases.Authoring;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Services
{
	public class CompetitionApplyService : CrudService<CompetitionApplyDto, CompetitionApply>, ICompetitionApplyService
	{
		public CompetitionApplyService(ICrudRepository<CompetitionApply> crudRepository, IMapper mapper) : base(crudRepository, mapper)
		{
		}


        public Result<PagedResult<CompetitionApplyDto>> GetAppliesByCompId(int comId)
        {
            var allApplies = CrudRepository.GetPaged(1, int.MaxValue);

            var filteredApplies = allApplies.Results.Where(apply => apply.CompetitionId == comId);

            var filteredPagedResult = new PagedResult<CompetitionApply>(filteredApplies.ToList(), filteredApplies.Count());
            return MapToDto(filteredPagedResult);
        }
    }
}
