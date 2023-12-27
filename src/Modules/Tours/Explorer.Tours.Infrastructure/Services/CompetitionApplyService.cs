using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.Competitions;
using Explorer.Tours.Core.UseCases.Authoring;
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
	}
}
