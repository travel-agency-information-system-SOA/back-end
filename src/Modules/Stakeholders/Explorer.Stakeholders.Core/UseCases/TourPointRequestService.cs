using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
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
    public class TourPointRequestService : CrudService<TourPointRequestDto, TourPointRequest>, ITourPointRequestService, IInternalTourPointRequestService
    {
        private readonly ITourPointRequestRepository _tourPointRequestRepository;
        public TourPointRequestService(ICrudRepository<TourPointRequest> repository, ITourPointRequestRepository tourPointRequestRepository, IMapper mapper) : base(repository, mapper) {
            _tourPointRequestRepository = tourPointRequestRepository;
        }

        public Result<TourPointRequestDto> AcceptRequest(int id)
        {
            //kreiraj notifikaciju ovdjeee
            var result = _tourPointRequestRepository.AcceptRequest(id);
            return MapToDto(result);
        }
    }
}
