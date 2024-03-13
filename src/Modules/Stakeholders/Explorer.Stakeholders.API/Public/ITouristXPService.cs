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
    public interface ITouristXPService
    {
        public Result<TouristXPDto> AddExperience(int TouristId, int ammount);
        Result<PagedResult<TouristXPDto>> GetByUserId(int userId, int page, int pageSize);
    }
}
