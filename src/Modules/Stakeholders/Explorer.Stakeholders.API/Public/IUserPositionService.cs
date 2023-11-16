using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IUserPositionService
    {
        Result<PagedResult<UserPositionDto>> GetPaged(int page, int pageSize);
        Result<UserPositionDto> Create(UserPositionDto userPosition);
        Result<UserPositionDto> Update(UserPositionDto userPosition);
        Result Delete(int id);
        public Result<UserPositionDto> GetByUserId(int tourExecutionId, int page, int pageSize);
    }
}
