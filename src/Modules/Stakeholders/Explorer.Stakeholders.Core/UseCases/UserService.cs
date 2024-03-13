using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class UserService : BaseService<UserDto, User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository, IMapper mapper) : base(mapper)
        {
            _userRepository = userRepository;
        }

        public Result<UserDto> Get(int userId)
        {
            var result = _userRepository.Get(userId);
            if (result == null)
            {
                return Result.Fail("User not found");
            }
            return MapToDto(result);
        }

        public UserRole GetUserRole(int userId)
        {
            var result = _userRepository.Get(userId);
            return result == null ? throw new Exception("User not found") : result.Role;
        }

        public Result<UserDto> ConfirmRegistration(string verificationToken)
        {
            var user = _userRepository.GetUserByToken(verificationToken);
            if (user == null) return Result.Fail(FailureCode.NotFound);
            if (user.IsActive) return Result.Fail("User is already active");
            user.IsActive = true;
            var updatedUser = _userRepository.Update(user);
            return MapToDto(updatedUser);
        }
    }
}
