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
    public class TourObjectRequestService : CrudService<TourObjectRequestDto, TourObjectRequest>, ITourObjectRequestService, IInternalTourObjectRequestService
    {
        private readonly ITourObjectRequestRepository _tourObjectRequestRepository;

        public TourObjectRequestService(ICrudRepository<TourObjectRequest> repository, IMapper mapper, ITourObjectRequestRepository requestRepository) : base(repository, mapper)
        {
            _tourObjectRequestRepository = requestRepository;
        }

        public Result<TourObjectRequestDto> Create(int tourObjectId, int authorId)
        {
            TourObjectRequest request = new TourObjectRequest(authorId, tourObjectId);
            var result = CrudRepository.Create(request);
            return MapToDto(result);
        }
        public Result<TourObjectRequestDto> AcceptRequest(int id, string comment)
        {
            var result = _tourObjectRequestRepository.AcceptRequest(id, comment);
            return MapToDto(result);
        }
        public Result<TourObjectRequestDto> RejectRequest(int id, string comment)
        {
            var result = _tourObjectRequestRepository.RejectRequest(id, comment);
            return MapToDto(result);
        }
    }
}
