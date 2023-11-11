using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class PublicTourPointService: CrudService<PublicTourPointDto, PublicTourPoint>, IPublicTourPointService
    {
        private readonly IInternalTourPointRequestService _internalTourPointRequestInterface;
        public PublicTourPointService(ICrudRepository<PublicTourPoint> repository, IMapper mapper, IInternalTourPointRequestService internalService) : base(repository, mapper) {
            _internalTourPointRequestInterface = internalService;   
        }

        public Result<PagedResult<PublicTourPointDto>> GetTourPointsByTourId(int tourId)
        {
            var allTourPoints = CrudRepository.GetPaged(1, int.MaxValue);

            var filteredTourPoints = allTourPoints.Results.Where(tourPoint => tourPoint.IdTour == tourId);

            var filteredPagedResult = new PagedResult<PublicTourPoint>(filteredTourPoints.ToList(), filteredTourPoints.Count());
            return MapToDto(filteredPagedResult);

        }

        public Result<PublicTourPointDto> Create(PublicTourPointDto publicTourPoint)
        {

            var result = CrudRepository.Create(MapToDomain(publicTourPoint));
            //_internalTourPointRequestInterface.Create();  ovdje cu samo da ga kreiram
            //na lijevom mjestu cemo pozivati accept
            return MapToDto(result);
        }
    }
}
