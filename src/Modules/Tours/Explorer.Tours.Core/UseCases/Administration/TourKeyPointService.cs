using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourKeyPointService : BaseService<TourKeyPointDto, TourEquipment>, ITourKeyPointService
    {
        private readonly ICrudRepository<TourKeyPoint> _tourKeyPointRepository;
        private readonly IMapper _mapper;

        public TourKeyPointService(ICrudRepository<TourKeyPoint> tourKeyPointRepository, IMapper mapper) : base(mapper)
        {
            _tourKeyPointRepository = tourKeyPointRepository;
            _mapper = mapper;
        }

        public async Task<Result> AddKeyPointToTourAsync(long tourId, long pointId)
        {
            var allTourKeyPoint = _tourKeyPointRepository.GetPaged(1, int.MaxValue);

            // Proverimo da li veza već postoji
            bool connectionExists = allTourKeyPoint.Results.Any(te => te.TourId == tourId && te.PointId == pointId);

            if (!connectionExists)
            {
                // Ako ne postoji veza, kreirajte novu vezu između ture i opreme
                var newTourKeyPoint = new TourKeyPoint
                {
                    TourId = tourId,
                    PointId = pointId
                };
                _tourKeyPointRepository.Create(newTourKeyPoint);
            }

            return Result.Ok();
        }

        public async Task<Result<List<TourKeyPointDto>>> GetTourKeyPointAsync(long tourId)
        {
            var allTourKeyPoint = _tourKeyPointRepository.GetPaged(1, int.MaxValue).Results;

            var tourKeyPointForTour = allTourKeyPoint.Where(te => te.TourId == tourId).ToList();

            var tourKeyPointDtos = _mapper.Map<List<TourKeyPointDto>>(tourKeyPointForTour);

            return Result.Ok(tourKeyPointDtos);
        }




        public async Task<Result> RemoveKeyPointFromTourAsync(long tourId, long pointId)
        {
            var existingTourKeyPoint = _tourKeyPointRepository.GetPaged(1, int.MaxValue);
            foreach (var tourKeyPoint in existingTourKeyPoint.Results)
            {
                if (tourKeyPoint.TourId == tourId && tourKeyPoint.PointId == pointId)
                {
                    _tourKeyPointRepository.Delete(tourKeyPoint.Id);
                    break;
                }

            }








            return Result.Ok();
        }


    }
}
