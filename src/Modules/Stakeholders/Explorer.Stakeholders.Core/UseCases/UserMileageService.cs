using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.API.Public.TourExecuting;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserMileageService : CrudService<UserMileageDto, UserMileage>, IUserMileageService
    {
        private readonly IUserMileageRepository _userMileageRepository;
        private readonly IUserTourMileageService _userTourMileageService;
        private readonly IUserService _userService;
        public UserMileageService(ICrudRepository<UserMileage> crudRepository, IMapper mapper, IUserMileageRepository userMileageRepository, IUserTourMileageService userTourMileageService, IUserService userService) : base(crudRepository, mapper)
        {
            _userMileageRepository = userMileageRepository;
            _userTourMileageService = userTourMileageService;
            _userService = userService; 
        }

        public void AddMileage(int userId, double mileage)
        {
            var mileages = CrudRepository.GetPaged(0, 0).Results;
            var userMileage = mileages.Where(m => m.UserId == userId).FirstOrDefault();
            var userMileageDto = MapToDto(userMileage);
            
            userMileageDto.Mileage += mileage;
            _userMileageRepository.Update(MapToDomain(userMileageDto));
        }

        public void UpdateMileageByMonth(int userId)
        {
            double mileage = 0;
            var mileages = CrudRepository.GetPaged(0, 0).Results;
            var userMileage = mileages.Where(m => m.UserId == userId).FirstOrDefault();
            var userMileageDto = MapToDto(userMileage);

            var userTourMileageDtos = _userTourMileageService.GetRecentMileageByUser(userId);
            foreach(var m in userTourMileageDtos.Results)
            {
               mileage+= m.TourMileage;
            }
            userMileageDto.MileageByMonth=mileage;
            _userMileageRepository.Update(MapToDomain(userMileageDto));


        }

        public void UpdateAllUserMileages()
        {
            var mileages = CrudRepository.GetPaged(0,0).Results;
            foreach(var mileage in  mileages)
            {
                UpdateMileageByMonth(mileage.UserId);
            }
        }

        public Result<UserMileageDto> GetMileageByUser(int userId)
        {
            var mileages = CrudRepository.GetPaged(0, 0).Results;
            var userMileage = mileages.Where(m => m.UserId == userId).FirstOrDefault();
            var userMileageDto = MapToDto(userMileage);

            var user = _userService.Get(userMileage.UserId);
            userMileageDto.Username = user.Value.Username;

            return userMileageDto;
        }

        public PagedResult<UserMileageDto> GetAllSorted()
        {
            var mileages = CrudRepository.GetPaged(0, 0).Results;
            var sortedMileages = mileages.OrderByDescending(m => m.Mileage).ToList();
            List<UserMileageDto> sortedMileageDtos = new List<UserMileageDto>();
            foreach(var m in sortedMileages)
            {
                var mDto = MapToDto(m);
                var user = _userService.Get(mDto.UserId);
                mDto.Username = user.Value.Username;
                sortedMileageDtos.Add(mDto);
            }


            return new PagedResult<UserMileageDto>(sortedMileageDtos, sortedMileages.Count());
        }

        public PagedResult<UserMileageDto> GetAllSortedByMonth()
        {
            var mileages = CrudRepository.GetPaged(0, 0).Results;
            var sortedMileages = mileages.OrderByDescending(m => m.MileageByMonth).ToList();
            List<UserMileageDto> sortedMileageDtos = new List<UserMileageDto>();
            foreach (var m in sortedMileages)
            {
                var mDto = MapToDto(m);
                var user = _userService.Get(mDto.UserId);
                mDto.Username = user.Value.Username;
                sortedMileageDtos.Add(mDto);
            }


            return new PagedResult<UserMileageDto>(sortedMileageDtos, sortedMileages.Count());
        }


    }
}
