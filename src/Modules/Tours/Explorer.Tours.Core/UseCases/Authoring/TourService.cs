using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Public;
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
		private readonly IUserService _userService;
		public TourService(ICrudRepository<Tour> repository, ITourRepository tourRepository, IMapper mapper, IUserService userService) : base(repository, mapper)
        {
            _mapper = mapper;
            _repository = tourRepository;
            _userService = userService;
        }


        //celi agregat se dobavlja
        public Result<PagedResult<TourDTO>> GetAll( int page, int pageSize)

        {
            var tours = _repository.GetAll(page, pageSize);
            return MapToDto(tours);

        }

		public Result<PagedResult<TourDTO>> GetByUserId(int userId, int page, int pageSize)
        {
            var tours = _repository.GetByUserId(userId, page, pageSize);
            return MapToDto(tours);

        }

        public Result<TourDTO> GetTourByTourId(int id)
        {
            var tours = _repository.GetById(id);
            return MapToDto(tours);
        }

        public TourDTO GetTourById(int id)
        {
            var tour = _repository.GetById(id);
            return MapToDto(tour);
        }

        public Result<PagedResult<TourDTO>> GetByLevelAndPrice(string level, int price, int page, int pageSize)
        {
            int firstPrice = 0;
            if (price == 100)
                firstPrice = 0;
            else if (price == 200)
                firstPrice = 101;
            else
                firstPrice = 201;

           var tours = _repository.GetAllPublished(page, pageSize);
           var filteredTours = tours.Results.Where(tour =>
                                            (level.Equals("None") || tour.DifficultyLevel.ToString() == level) &&
                                             (price == 0 || (tour.Price >= firstPrice && tour.Price <= price && price != 0)))
                                             .ToList();

            var totalCount = filteredTours.Count;
            var pagedResult = new PagedResult<Tour>(filteredTours, totalCount);
            return MapToDto(pagedResult);

        }
        public Result<PagedResult<TourDTO>> GetByRange(double lat, double lon, int range, int type, int page, int pageSize)
        {
            var tours = _repository.GetAllPublished(page, pageSize);

            var filteredTours = tours.Results
                .Where(tour =>
                {
                    if (type == 1 && tour.TourPoints.Count > 0)
                    {
                        var firstCheckpoint = tour.TourPoints.First();
                        return IsWithinRange(lat, lon, firstCheckpoint.Latitude, firstCheckpoint.Longitude, range * 1000);
                    }
                    else if (type == 2 && tour.TourPoints.Count > 0)
                    {
                        var lastCheckpoint = tour.TourPoints.Last();
                        return IsWithinRange(lat, lon, lastCheckpoint.Latitude, lastCheckpoint.Longitude, range * 1000);
                    }
                    else
                    {
                        return tour.TourPoints.Any(checkpoint => IsWithinRange(lat, lon, checkpoint.Latitude, checkpoint.Longitude, range * 1000));
                    }
                })
                .ToList();
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



        public Result<PagedResult<TourDTO>> GetPublished()
        {
            var tours = GetPaged(1, int.MaxValue).Value;
            var publishedTours = tours.Results.Where(tour => tour.Status == TourStatus.Published.ToString()).ToList();
            var filteredPagedResult = new PagedResult<TourDTO>(publishedTours, publishedTours.Count());
            return Result.Ok(filteredPagedResult);
        }

        public Result SetTourCharacteristic(int tourId, double distance, double duration, string transportType)

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

       

        public Result Publish(int tourId)
        {
            try
            {
                var tour = _repository.GetByTourId(tourId);

                
                if (string.IsNullOrWhiteSpace(tour.Name) || string.IsNullOrWhiteSpace(tour.Description) || tour.Tags == null || tour.Tags.Count == 0)
                {
                    return Result.Fail("Tour must have all basic data set.");
                }

                if (tour.TourPoints.Count < 2)
                {
                    return Result.Fail("Tour must have at least two key points.But it has "+tour.TourPoints);
                  
                }

                bool validTimeDefined = tour.TourCharacteristics.Any(tc => tc.Duration > 0);
                if (!validTimeDefined)
                {
                    return Result.Fail("At least one valid tour time must be defined.");
                }

                tour.Publish(tour);
                CrudRepository.Update(tour);

                return Result.Ok();
            }
            catch (Exception e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }
        }

      
    

        public Result ArchiveTour(int tourId)
        {
            try
            {
                Tour tour = CrudRepository.Get(tourId);
                tour.Archive(tour);
                CrudRepository.Update(tour);
                return Result.Ok();
            }
			catch (Exception e)
			{
				return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
			}
		}

        public Result DeleteAggregate(int id)
        {
            try
            {
                _repository.DeleteAgreggate(id);
                 return Result.Ok();
            }
			catch (Exception e)
			{
				return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
			}
		}


        public Result<PagedResult<TourDTO>> GetAllPublishedByAuthor(int id, int page, int pageSize)
        {
            var tours = _repository.GetByUserId(id, page, pageSize);
            var t = MapToDto(tours).Value;
            var publishedTours = t.Results.Where(tour => tour.Status == TourStatus.Published.ToString()).ToList();
            var filteredPagedResult = new PagedResult<TourDTO>(publishedTours, publishedTours.Count());
            return Result.Ok(filteredPagedResult);
        }

		public Result<PagedResult<TourDTO>> FilterToursByPublicTourPoints(PublicTourPointDto[] publicTourPoints, int page, int pageSize)
		{
			var tours = _repository.GetAll(page, pageSize).Results;
			var helpingTours = new List<Tour>();

			foreach (var tour in tours)
			{

				var role = _userService.GetUserRole(tour.UserId);


				if (role == Stakeholders.API.Dtos.UserRole.Author)
				{
					helpingTours.Add(tour);
				}
			}


			Console.WriteLine($"Number of Public Tour Points: {publicTourPoints.Length}");

			var filteredTours = helpingTours
				.Where(tour => tour.TourPoints.Any(tourPoint =>
					publicTourPoints.Any(publicTp =>
						publicTp.Latitude == tourPoint.Latitude && publicTp.Longitude == tourPoint.Longitude)))
				.ToList();

			var filteredPagedResult = new PagedResult<Tour>(filteredTours.DistinctBy(t => t.Id).ToList(), filteredTours.Count());
			return MapToDto(filteredPagedResult);
		}

		public long GetLastTourId(int page, int pageSize)
        {
			var tours = _repository.GetAll(page, pageSize).Results;

			var lastTourId = tours.OrderByDescending(tour => tour.Id).First().Id;

			return lastTourId;

		}
    }
}
