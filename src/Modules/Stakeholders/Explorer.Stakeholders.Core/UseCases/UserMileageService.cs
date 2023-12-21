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
        public UserMileageService(ICrudRepository<UserMileage> crudRepository, IMapper mapper, IUserMileageRepository userMileageRepository) : base(crudRepository, mapper)
        {
            _userMileageRepository = userMileageRepository;
        }

        public void AddMileage(int userId, double mileage)
        {
            var mileages = CrudRepository.GetPaged(0, 0).Results;
            var userMileage = mileages.Where(m => m.UserId == userId).FirstOrDefault();
            var userMileageDto = MapToDto(userMileage);

            userMileageDto.Mileage += mileage;
            _userMileageRepository.Update(MapToDomain(userMileageDto));
        }

        public Result<UserMileageDto> GetMileageByUser(int userId)
        {
            var mileages = CrudRepository.GetPaged(0, 0).Results;
            var userMileage = mileages.Where(m => m.UserId == userId).FirstOrDefault();
            var userMileageDto = MapToDto(userMileage);

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
                sortedMileageDtos.Add(mDto);
            }


            return new PagedResult<UserMileageDto>(sortedMileageDtos, sortedMileages.Count());
        }   

        
    }
}
