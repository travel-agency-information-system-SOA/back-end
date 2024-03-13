using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases;

public class UserTourMileageService : CrudService<UserTourMileageDto, UserTourMileage>, IUserTourMileageService
{
    
    public UserTourMileageService(ICrudRepository<UserTourMileage> crudRepository, IMapper mapper) : base(crudRepository, mapper)
    {
       
    }

    public void CreateInstance(int userId, double distance, DateTime time)
    {
        UserTourMileageDto userTourMileageDto = new UserTourMileageDto();
        userTourMileageDto.UserId=userId;
        userTourMileageDto.TourMileage=distance;
        userTourMileageDto.Time=time;
        UserTourMileage userTourMileage = MapToDomain(userTourMileageDto);
        CrudRepository.Create(userTourMileage);
    }

    public PagedResult<UserTourMileageDto> GetRecentMileageByUser(int userId)
    {
        var tourMileages = CrudRepository.GetPaged(0, 0).Results;
        var userTourMileage = tourMileages.Where(m => m.UserId == userId && m.Time>= DateTime.UtcNow.AddDays(-30)).ToList();
        List<UserTourMileageDto> userTourMileageDtos = new List<UserTourMileageDto>();
        foreach(var mileage in userTourMileage)
        {
            var mDto = MapToDto(mileage);
            userTourMileageDtos.Add(mDto);
        }

        return new PagedResult<UserTourMileageDto>(userTourMileageDtos, userTourMileage.Count());
    }
}
