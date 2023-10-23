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
using FluentResults;

namespace Explorer.Tours.Core.UseCases.Administration
{
	public class TourService : CrudService<TourDTO, Tour>, ITourService
	{
		private readonly ICrudRepository<Tour> _repository;
		private readonly IMapper _mapper;
		public TourService(ICrudRepository<Tour> repository, IMapper mapper) : base(repository, mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public Result<List<TourDTO>> GetByUserId(int userId)
		{
			
			throw new NotImplementedException();


		}
	}
}
