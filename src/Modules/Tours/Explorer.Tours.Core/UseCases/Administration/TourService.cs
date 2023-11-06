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


namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourService : CrudService<TourDTO, Tour>, ITourService
	{
		private readonly IMapper _mapper;
		public TourService(ICrudRepository<Tour> repository, IMapper mapper) : base(repository, mapper)
		{
			_mapper = mapper;
		}

		public Result<PagedResult<TourDTO>> GetByUserId(int userId, int page, int pageSize)
		{
		  var allTours = CrudRepository.GetPaged(page, pageSize);
			var filteredTours = allTours.Results.Where(tour => tour.GuideId == userId);
			var filteredPagedResult = new PagedResult<Tour>(filteredTours.ToList(), filteredTours.Count());
			return MapToDto(filteredPagedResult);
			
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
