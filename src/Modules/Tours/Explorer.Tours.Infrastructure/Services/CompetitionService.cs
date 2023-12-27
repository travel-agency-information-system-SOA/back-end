using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain.Competitions;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.UseCases.Authoring;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Infrastructure.Services
{
	public class CompetitionService : CrudService<CompetitionDto, Competition>, ICompetitionService
	{

		private readonly IMapper _mapper;
		private readonly ICompetitionRepository _repository;
		public CompetitionService(ICrudRepository<Competition> crudRepository, ICompetitionRepository competitionRepository, IMapper mapper) : base(crudRepository, mapper)
		{
			_mapper = mapper;
			_repository = competitionRepository;
		}

		public Result<PagedResult<CompetitionDto>> GetAll(int page, int pageSize)

		{
			var competitions = _repository.GetAll(page, pageSize);
			return MapToDto(competitions);

		}

		
	}
}
