using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain.Tours;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.UseCases.Administration
{
    public class TourPointService : CrudService<TourPointDto, TourPoint>, ITourPointService
    {
        public TourPointService(ICrudRepository<TourPoint> repository, IMapper mapper) : base(repository, mapper) { }

		public Result<PagedResult<TourPointDto>> GetTourPointsByTourId(int tourId)
		{
			var allTourPoints = CrudRepository.GetPaged(1, int.MaxValue);

			var filteredTourPoints = allTourPoints.Results.Where(tourPoint => tourPoint.TourId == tourId);

			var filteredPagedResult = new PagedResult<TourPoint>(filteredTourPoints.ToList(), filteredTourPoints.Count());
			return MapToDto(filteredPagedResult);
		}


        public Result<TourPointDto> GetFirstTourPoint(int tourId)
        {
            var allTourPoints = CrudRepository.GetPaged(1, int.MaxValue);

            var firstTourPoint = allTourPoints.Results
                .Where(tp => tp.TourId == tourId)
                .First();


            if (firstTourPoint == null)
            {
                return Result.Fail("Nije pronađen prvi TourPoint za datu turu.");
            }

            var firstTourPointDto = MapToDto(firstTourPoint);

            return Result.Ok(firstTourPointDto);
        }

        TourPointDto ITourPointService.Get(int id)
        {
            var result = CrudRepository.Get(id);
            return MapToDto(result);

        }
    }
}
