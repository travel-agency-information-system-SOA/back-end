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
using System.Xml.Linq;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class PublicTourPointService: CrudService<PublicTourPointDto, PublicTourPoint>, IPublicTourPointService
    {
        private readonly IInternalTourPointRequestService _internalTourPointRequestInterface;
        private readonly ITourPointService _tourPointService;
        public PublicTourPointService(ICrudRepository<PublicTourPoint> repository, IMapper mapper, IInternalTourPointRequestService internalService,ITourPointService tourPointService) : base(repository, mapper) {
            _internalTourPointRequestInterface = internalService;
            _tourPointService = tourPointService;   
        }

        public Result<PagedResult<PublicTourPointDto>> GetTourPointsByTourId(int tourId)
        {
            var allTourPoints = CrudRepository.GetPaged(1, int.MaxValue);

            var filteredTourPoints = allTourPoints.Results.Where(tourPoint => tourPoint.IdTour == tourId);

            var filteredPagedResult = new PagedResult<PublicTourPoint>(filteredTourPoints.ToList(), filteredTourPoints.Count());
            return MapToDto(filteredPagedResult);

        }

        

        public Result<PublicTourPointDto> CreatePublicTourPointAndAcceptRequest(int requestId, int tourPointId,string comment)
        {
            TourPointDto tourPoint = new TourPointDto();
            tourPoint =_tourPointService.Get(tourPointId);
            PublicTourPoint publicTourPoint = new PublicTourPoint(tourPoint.TourId, tourPoint.Name, tourPoint.Description, tourPoint.Latitude, tourPoint.Longitude, tourPoint.ImageUrl);
            var result = CrudRepository.Create(publicTourPoint);
            _internalTourPointRequestInterface.AcceptRequest(requestId,comment);
            return MapToDto(result);
        }
    }
}
