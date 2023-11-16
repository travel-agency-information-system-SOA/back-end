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

        public TourPointRequestService(ICrudRepository<TourPointRequest> repository, IMapper mapper, ITourPointRequestRepository requestRepository) : base(repository, mapper)
        {
            _tourPointRequestRepository = requestRepository;
        }

        public Result<TourPointRequestDto> Create(int tourPointId, int authorId)
        {
            TourPointRequest request = new TourPointRequest(authorId,tourPointId);
            var result = CrudRepository.Create(request); 
            return MapToDto(result);
        }
        public Result<TourPointRequestDto> AcceptRequest(int id,string comment)
        {
            var result = _tourPointRequestRepository.AcceptRequest(id,comment);
            return MapToDto(result);
        }
        public Result<TourPointRequestDto> RejectRequest(int id,string comment)
        {
             var result = _tourPointRequestRepository.RejectRequest(id,comment);
             return MapToDto(result);
        }

       
    }

}
