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
    public interface IUserMileageService
    {
        public void AddMileage(int userId, double mileage);
        public Result<UserMileageDto> GetMileageByUser(int userId);
        public PagedResult<UserMileageDto> GetAllSorted();
        public PagedResult<UserMileageDto> GetAllSortedByMonth();
        public void UpdateMileageByMonth(int userId);
        public void UpdateAllUserMileages();
    }
}
