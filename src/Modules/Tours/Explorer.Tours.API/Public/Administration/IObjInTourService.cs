using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Administration
{
    public interface IObjInTourService
    {
        Result<PagedResult<ObjInTourDto>> GetPaged(int page, int pageSize);
        Result<ObjInTourDto> Create(ObjInTourDto objInTour);
        Result<ObjInTourDto> Update(ObjInTourDto objInTour);
        Result Delete(int id);

		Result<List<TourObjectDto>> GetObjectsByTourId(int tourId);

	}
}
