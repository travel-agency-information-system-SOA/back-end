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
    public interface ITourObjectService
    {
        Result<PagedResult<TourObjectDto>> GetPaged(int page, int pageSize);
        Result<TourObjectDto> Create(TourObjectDto tourObject);
        Result<TourObjectDto> Update(TourObjectDto tourObject);
        Result Delete(int id);

        Result<TourObjectDto> Get(int id);
        TourObjectDto GetTour(int id);

	}
}
