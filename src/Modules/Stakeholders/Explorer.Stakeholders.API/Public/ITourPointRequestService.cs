using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface ITourPointRequestService
    {
        Result<PagedResult<TourPointRequestDto>> GetPaged(int page, int pageSize);
        Result<TourPointRequestDto> Create(TourPointRequestDto club);
        Result<TourPointRequestDto> Update(TourPointRequestDto club);
        Result Delete(int id);
    }
}
