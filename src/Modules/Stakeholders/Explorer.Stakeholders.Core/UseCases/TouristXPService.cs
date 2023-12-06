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
        private ITouristXPRepository _touristXPRepository;
        public TouristXPService(ITouristXPRepository touristXPRepository, IMapper mapper) : base(mapper)
        {

            _touristXPRepository = touristXPRepository;
        }

        public Result<TouristXPDto> AddExperience(int touristId, int ammount)
        {
            var result = _touristXPRepository.AddExperience(touristId, ammount);
            return MapToDto(result);
        }

    }
}
