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
