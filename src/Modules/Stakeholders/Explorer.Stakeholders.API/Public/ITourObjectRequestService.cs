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
    public interface ITourObjectRequestService
    {
        Result<PagedResult<TourObjectRequestDto>> GetPaged(int page, int pageSize);
        Result<TourObjectRequestDto> Create(int tourObjectId, int authorId);
        Result Delete(int id);
        public Result<TourObjectRequestDto> RejectRequest(int id, string comment);
        public Result<TourObjectRequestDto> AcceptRequest(int id, string comment);
    }
}
