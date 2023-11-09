using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Tours.API;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;


namespace Explorer.Tours.Core.UseCases.Authoring
{
    public class TourService : CrudService<TourDTO, Tour>, ITourService
    {
        private readonly IMapper _mapper;
        private readonly ITourRepository _repository;
        public TourService(ICrudRepository<Tour> repository, ITourRepository tourRepository, IMapper mapper) : base(repository, mapper)
        {
            _mapper = mapper;
            _repository = tourRepository;
        }

        public Result<PagedResult<TourDTO>> GetByUserId(int userId, int page, int pageSize)
        {
            var tours = _repository.GetByUserId(userId, page, pageSize);
            return MapToDto(tours);

        }

        public Result<PagedResult<TourDTO>> GetByRange(double lat, double lon, int range, int page, int pageSize)
        {
            var tours = _repository.GetAllPublished(page, pageSize);

            var filteredTours = tours.Results.Where(tour => tour.TourPoints.Any(checkpoint => IsWithinRange(lat, lon, checkpoint.Latitude, checkpoint.Longitude, range * 1000))).ToList();
            var totalCount = filteredTours.Count;

            foreach (var tour in filteredTours)
            {
                if (tour.TourPoints.Count > 1)
                {
                    var firstTourPoint = tour.TourPoints.First();
                    tour.TourPoints.Clear();
                    tour.TourPoints.Add(firstTourPoint);
                }
            }
            var pagedResult = new PagedResult<Tour>(filteredTours, totalCount);
            return MapToDto(pagedResult);
        }

        private bool IsWithinRange(double lat1, double lon1, double lat2, double lon2, double rangeMeters)
        {
            const double EarthRadius = 6371000;
            lat1 *= Math.PI / 180;
            lon1 *= Math.PI / 180;
            lat2 *= Math.PI / 180;
            lon2 *= Math.PI / 180;

            double dLat = lat2 - lat1;
            double dLon = lon2 - lon1;
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = EarthRadius * c;

            return distance <= rangeMeters;
        }

        public Result SetTourCharacteristic(int tourId, int distance, TimeSpan duration, string transportType)
        {
            try
            {

                var Tour = CrudRepository.Get(tourId);
                Tour.setCharacteristic(distance, duration, (TransportType)Enum.Parse(typeof(TransportType), transportType));
                CrudRepository.Update(Tour);
                return Result.Ok();

            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }
    }







}
