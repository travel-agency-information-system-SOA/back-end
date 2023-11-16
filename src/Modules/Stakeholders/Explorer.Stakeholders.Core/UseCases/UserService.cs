using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserService : BaseService<UserDto, User>, IUserService
    {
        private readonly ICrudRepository<User> _userRepository;
        public UserService(ICrudRepository<User> userRepository, IMapper mapper) : base(mapper)
        {
            _userRepository = userRepository;
        }
        public Result<UserDto> Get(int userId)
        {
            var result = _userRepository.Get(userId);
            return MapToDto(result);
        }
    }
}
