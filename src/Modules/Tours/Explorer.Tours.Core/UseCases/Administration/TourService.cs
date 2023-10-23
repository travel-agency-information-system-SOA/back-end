using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using FluentResults;


namespace Explorer.Tours.Core.UseCases.Administration
{
	public class TourService : CrudService<TourDTO, Tour>, ITourService
	{
		
		public TourService(ICrudRepository<Tour> repository, IMapper mapper) : base(repository, mapper)
		{
			
		}

		public Result<List<TourDTO>> GetByUserId(int userId, int page, int pageSize)
		{
		  var allTours = CrudRepository.GetPaged(page, pageSize);
		 List<Tour> filteredTours = allTours.Results.Where(tour => tour.GuideId==userId).ToList();
		  return MapToDto(filteredTours);
		}
	}







}
