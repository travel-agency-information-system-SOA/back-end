using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class TouristXPService : BaseService<TouristXPDto, TouristXP>, ITouristXPService
    {
        private readonly ITouristXPRepository _touristXPRepository;
        private readonly ICrudRepository<TouristXP> _repository;
        public TouristXPService(ICrudRepository<TouristXP> repository,ITouristXPRepository touristXPRepository, IMapper mapper) : base(mapper)
        {

            _touristXPRepository = touristXPRepository;
            _repository = repository;
        }

        public Result<TouristXPDto> AddExperience(int touristId, int ammount)
        {
            var result = _touristXPRepository.AddExperience(touristId, ammount);
            return MapToDto(result);
        }

        public Result<PagedResult<TouristXPDto>> GetByUserId(int id, int page, int pageSize)
        {
            var allTouristXPs = _repository.GetPaged(page, pageSize);
            var filteredTouristXPs = allTouristXPs.Results.Where(t => t.TouristId == id);
            var filteredPagedResult = new PagedResult<TouristXP>(filteredTouristXPs.ToList(), filteredTouristXPs.Count());
            return MapToDto(filteredPagedResult);

        }

    }
}
