using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
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

        //metoda za dobavljanje tura i recenzija
        public Result<PagedResult<TourDTO>> GetAll( int page, int pageSize)
        {
            var tours = _repository.GetAll( page, pageSize);
            return MapToDto(tours);

        }
        public Result<PagedResult<TourDTO>> GetByUserId(int userId, int page, int pageSize)
        {
            var tours = _repository.GetByUserId(userId, page, pageSize);
            return MapToDto(tours);

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

        public Result SetTourCharacteristic(int tourId, int distance, TimeSpan duration, string transposrtType)
        {
            throw new NotImplementedException();
        }
    

        public Result ArchiveTour(int tourId)
        {
            try
            {
                Tour tour = CrudRepository.Get(tourId);
                tour.Status = TourStatus.Archived;
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

		
	}







}
