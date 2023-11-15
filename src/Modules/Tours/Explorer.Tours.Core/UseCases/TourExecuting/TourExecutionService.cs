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
using System.Security.Cryptography.X509Certificates;
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
        
        public Result<TourExecutionDto> GetByUser(int userId)
        {
            var execution = _repository.GetByUser(userId);
            
            var executionDto = MapToDto(execution);
            LoadTour(executionDto);
            return executionDto;
        }

        
        public void UpdatePosition(int tourExecutionId, double longitude, double latitude)
        {
            var execution = _repository.GetById(tourExecutionId);

            TourExecutionDto executionDto = MapToDto(execution);

            LoadTour(executionDto);
            executionDto.Position.Longitude = longitude;
            executionDto.Position.Latitude = latitude;
            executionDto.Position.LastActivity = DateTime.UtcNow;

            CheckTourPoints(executionDto);
            
            _repository.Update(MapToDomain(executionDto));
        }

        public void UpdateStatus(int tourExecutionId, string status )
        {
            var execution = _repository.GetById(tourExecutionId);

            TourExecutionDto executionDto = MapToDto(execution);

            executionDto.Status = status;
            executionDto.Position.LastActivity= DateTime.UtcNow;

            _repository.Update(MapToDomain(executionDto));
        }

        public void CompleteTourPoint(int tourExecutionId, int tourPointId)
        {
            var execution = _repository.GetById(tourExecutionId);

            TourExecutionDto executionDto = MapToDto(execution);

            executionDto.TourPoints.FirstOrDefault(tp => tp.Id == tourPointId).Completed = true;
            executionDto.TourPoints.FirstOrDefault(tp => tp.TourPointId == tourPointId).CompletionTime = DateTime.UtcNow;

            _repository.Update(MapToDomain(executionDto));
        }

        private void LoadTour(TourExecutionDto execution)
        {
            var tour = _tourRepository.GetById(execution.TourId);
            execution.Tour = _mapper.Map<TourDTO>(tour);
        }

        public void CheckTourPoints(TourExecutionDto te)
        {
            foreach(TourPointDto tp in te.Tour.TourPoints)
            {
                if(CalculateDistance(te.Position.Latitude, te.Position.Longitude, tp.Latitude, tp.Longitude) < 50.0)
                {
                    int tourPointForChangeId = tp.Id;
                    te.TourPoints.FirstOrDefault(tep => tep.TourPointId == tourPointForChangeId).Completed = true;
                    te.TourPoints.FirstOrDefault(tep => tep.TourPointId == tourPointForChangeId).CompletionTime = DateTime.UtcNow;
                }
            }
        }

        public bool IsFinished(int tourExecutionId)
        {
            var execution = _repository.GetById(tourExecutionId);

            TourExecutionDto executionDto = MapToDto(execution);

            foreach (TourPointExecutionDto point in executionDto.TourPoints)
            {
                if (!point.Completed)
                {
                    return false;
                }
            }

            UpdateStatus(tourExecutionId, "Completed");

            return true;
        }

        public double CalculateDistance(double userLat, double userLon, double pointLat, double pointLon)
        {
            double EarthRadiusKm = 6371.0;
            userLat = DegreesToRadians(userLat);
            userLon = DegreesToRadians(userLon);
            pointLat = DegreesToRadians(pointLat);
            pointLon = DegreesToRadians(pointLon);

            double dLat = pointLat - userLat;
            double dLon = pointLon - userLon;

            double a = Math.Sin(dLat / 2.0) * Math.Sin(dLat / 2.0) +
                       Math.Cos(userLat) * Math.Cos(pointLat) *
                       Math.Sin(dLon / 2.0) * Math.Sin(dLon / 2.0);

            double c = 2.0 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distance = EarthRadiusKm * c*1000;

            return distance;
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180.0;
        }

        public void Create(int userId, int tourId, double longitude, double latitude)
        {
            var tour = _tourRepository.GetById(tourId);
            var execution = new TourExecution(userId, tourId, TourExecutionStatus.InProgress);

            int executionId = _repository.Create(userId, tourId);

            foreach(TourPoint tp in tour.TourPoints)
            {
                _repository.CreatePoint(executionId, Convert.ToInt32(tp.Id));
            }

            _repository.CreatePosition(longitude, latitude, executionId);
            
        }

    }
}

