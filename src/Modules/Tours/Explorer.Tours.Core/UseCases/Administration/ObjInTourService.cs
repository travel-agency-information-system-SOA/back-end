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
    public class ObjInTourService : CrudService<ObjInTourDto, ObjInTour>, IObjInTourService
    {
		
        public ObjInTourService(ICrudRepository<ObjInTour> repository, IMapper mapper) : base(repository, mapper)
		{
		   
		}

		public Result<List<int>> GetObjectsByTourId(int tourId)
		{
			var allObjects = CrudRepository.GetPaged(1, int.MaxValue).Results;

			var tourObjects = allObjects.Where(te => te.IdTour == tourId);

			var objectIds = tourObjects.Select(te => te.IdObject).ToList();

			return objectIds;
			

			


		}

		
	}
}
