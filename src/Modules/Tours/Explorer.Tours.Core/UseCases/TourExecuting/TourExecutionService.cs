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
        private readonly ITourRepository _tourRepository;
        public TourExecutionService(ICrudRepository<TourExecution> crudRepository, IMapper mapper, ITourExecutionRepository tourExecutionRepository, ITourRepository tourRepository) : base(crudRepository, mapper)
        {
            _mapper = mapper;
            _repository = tourExecutionRepository;
            _tourRepository = tourRepository;
        }

        public Result<TourExecutionDto> GetById(int tourExecutionId)
        {
            var execution = _repository.GetById(tourExecutionId);
            

            var executionDto = MapToDto(execution);
            LoadTour(executionDto);
            return executionDto;
        }

        // TODO: lastActivity
        public void UpdatePosition(int tourExecutionId, int longitude, int latitude)
        {
            var execution = _repository.GetById(tourExecutionId);

            TourExecutionDto executionDto = MapToDto(execution);

            executionDto.Position.Longitude = longitude;
            executionDto.Position.Latitude = latitude;

            _repository.Update(MapToDomain(executionDto));
        }

        public void CompleteTourPoint(int tourExecutionId, int tourPointId)
        {
            var execution = _repository.GetById(tourExecutionId);

            TourExecutionDto executionDto = MapToDto(execution);

            executionDto.TourPoints.FirstOrDefault(tp => tp.Id == tourPointId).Completed = true;
            //executionDto.TourPoints.FirstOrDefault(tp => tp.TourPointId == tourPointId).CompletionTime = DateTime.Now;

            _repository.Update(MapToDomain(executionDto));
        }

        private void LoadTour(TourExecutionDto execution)
        {
            var tour = _tourRepository.GetById(execution.TourId);
            execution.Tour = _mapper.Map<TourDTO>(tour);
        }

        
    }
}

