using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Dtos.TourExecutionsDTO;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.TourExecuting;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.TourExecuting
{
    public class TourExecutionService : CrudService<TourExecutionDto, TourExecution>, ITourExecutionService
    {
        private readonly IMapper _mapper;
        private readonly ITourExecutionRepository _repository;
        public TourExecutionService(ICrudRepository<TourExecution> crudRepository, IMapper mapper, ITourExecutionRepository tourExecutionRepository) : base(crudRepository, mapper)
        {
            _mapper = mapper;
            _repository = tourExecutionRepository;
        }

        public Result<PagedResult<TourExecutionDto>> GetById(int tourExecutionId, int page, int pageSize)
        {
            var execution = _repository.GetById(tourExecutionId, page, pageSize);
            return MapToDto(execution);
        }

        public void UpdatePosition(int tourExecutionId, int longitude, int latitude)
        {
            var execution = _repository.GetById(tourExecutionId);

            TourExecutionDto executionDto = MapToDto(execution);

            executionDto.Position.Longitude = longitude;
            executionDto.Position.Latitude = latitude;

            _repository.Update(MapToDomain(executionDto));
            
        }
    }
}

