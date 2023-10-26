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
    public interface ITourPointService
    {
        Result<PagedResult<TourPointDto>> GetPaged(int page, int pageSize);
        Result<TourPointDto> Create(TourPointDto tourPoint);
        Result<TourPointDto> Update(TourPointDto tourPoint);
        Result Delete(int id);

        Result<PagedResult<TourPointDto>> GetTourPointsByTourId(int tourId);


    }
}
