using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public;

public interface IUserTourMileageService
{
    Result<UserTourMileageDto> Create(UserTourMileageDto userTourMileageDto);
    public void CreateInstance(int userId, double distance, DateTime time);
    public PagedResult<UserTourMileageDto> GetRecentMileageByUser(int userId);
}
