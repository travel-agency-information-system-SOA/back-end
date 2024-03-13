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

		private readonly ITourObjectService _tourObjectService;

        public ObjInTourService(ICrudRepository<ObjInTour> repository, IMapper mapper, ITourObjectService tourObjectService) : base(repository, mapper)
		{
			this._tourObjectService = tourObjectService;
		}

		public Result<List<TourObjectDto>> GetObjectsByTourId(int tourId)
		{
			var allObjects = CrudRepository.GetPaged(1, int.MaxValue).Results;

			var tourObjects = allObjects.Where(te => te.IdTour == tourId);

			var objectIds = tourObjects.Select(te => te.IdObject).ToList();

			var objList = new List<TourObjectDto>();
			foreach (var objectId in objectIds)
			{
				objList.Add(_tourObjectService.Get(objectId).Value);
			}

			return objList;

		}

		
	}
}
