using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Stakeholders.API.Internal;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
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
    public class PublicTourObjectService : CrudService<PublicTourObjectDto, PublicTourObject>, IPublicTourObjectService
    {
        private readonly IInternalTourObjectRequestService _internalTourObjectRequestInterface;
        private readonly ITourObjectService _tourObjectService;
        public PublicTourObjectService(ICrudRepository<PublicTourObject> repository, IMapper mapper, IInternalTourObjectRequestService internalService, ITourObjectService tourObjectService) : base(repository, mapper)
        {
            _internalTourObjectRequestInterface = internalService;
            _tourObjectService = tourObjectService;
        }


        public Result<PublicTourObjectDto> CreatePublicTourObjectAndAcceptRequest(int requestId, int tourObjectId, string comment)
        {
            TourObjectDto tourObject = new TourObjectDto();
            tourObject = _tourObjectService.GetTour(tourObjectId);
            PublicTourObject publicTourObject = new PublicTourObject(tourObject.Name, tourObject.Description, tourObject.ImageUrl, tourObject.Latitude, tourObject.Longitude);
            var result = CrudRepository.Create(publicTourObject);
            _internalTourObjectRequestInterface.AcceptRequest(requestId, comment);
            return MapToDto(result);
        }


        public Result<PagedResult<PublicTourObjectDto>> GetTourObjectByTourId(int tourId)
        {
            var allTourObjects = CrudRepository.GetPaged(1, int.MaxValue);

            var filteredTourObjects = allTourObjects.Results.Where(tourObject => tourObject.Id == tourId);

            var filteredPagedResult = new PagedResult<PublicTourObject>(filteredTourObjects.ToList(), filteredTourObjects.Count());
            return MapToDto(filteredPagedResult);
        }

    }
}
