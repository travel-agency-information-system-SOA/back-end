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
        Result<TourPointRequestDto> Create(int tourPointId, int authorId);
        Result Delete(int id);
        public Result<TourPointRequestDto> RejectRequest(int id,string comment);
        public Result<TourPointRequestDto> AcceptRequest(int id,string comment);
    }
}
