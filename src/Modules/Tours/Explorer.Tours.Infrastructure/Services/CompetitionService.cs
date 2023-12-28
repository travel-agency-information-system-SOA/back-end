using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.ShoppingCartDtos;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Payments.Core.UseCases.ShoppingCarts;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
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
		private readonly TourPurchaseTokenService _tourPurchaseTokeService;
		private readonly ITourService _tourService;
		public CompetitionService(ICrudRepository<Competition> crudRepository, ICompetitionRepository competitionRepository, IMapper mapper, ITourService tourService) : base(crudRepository, mapper)
		{
			_mapper = mapper;
			_repository = competitionRepository;
			_tourService = tourService;

        }

		public Result<PagedResult<CompetitionDto>> GetAll(int page, int pageSize)

		{
			var competitions = _repository.GetAll(page, pageSize);
			return MapToDto(competitions);

		}

        public Result<PagedResult<CompetitionDto>> GetAllCompetitionAuthorId(int page, int pageSize, int id)
        {
            var competitions = _repository.GetAll(page, pageSize);
			List<Competition> result = new List<Competition>();
			foreach ( var competition in competitions.Results)
			{
                int tourIdInt = (int)competition.TourId;
                TourDTO tour = _tourService.GetTourById(tourIdInt);

				if(tour.UserId == id)
				{
                    result.Add(competition);
				}
				
			}
            var pgResult = new PagedResult<Competition>(result, result.Count);
            return MapToDto(pgResult);
        }


    }
}
