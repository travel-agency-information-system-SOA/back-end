using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class AccountManagementService : IAccountManagementService
    {
        private readonly IUserRepository _userRepository;

        public AccountManagementService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Result<List<AccountDto>> GetAllAccounts()
        {

            var users = _userRepository.GetAll();
            var accounts = CreateAccountDtos(users);

            return accounts;
        }

        private List<AccountDto> CreateAccountDtos(List<User> users)
        {
            List<AccountDto> accounts = new List<AccountDto>();
            foreach (var user in users)
            {
                var accountDto = new AccountDto
                {
                    UserId = user.Id,
                    Username = user.Username,
                    Password = user.Password,
                    Email = _userRepository.GetPersonEmail(user.Id),
                    Role = user.GetPrimaryRoleName(),
                    IsActive = user.IsActive,
                };
                accounts.Add(accountDto);
            }

            return accounts;
        }

        public Result<AccountDto> BlockOrUnblock(AccountDto account)
        {
            try
            {
                var user = _userRepository.Get(account.UserId);

                if (account.IsActive)
                {
                    user.IsActive = false;
                }
                else
                {
                    user.IsActive = true;
                }

                _userRepository.Update(user);
                account.IsActive = user.IsActive;
                return account;
            }
            catch (KeyNotFoundException e)
            {
                return Result.Fail(FailureCode.NotFound).WithError(e.Message);
            }
            catch (ArgumentException e)
            {
                return Result.Fail(FailureCode.InvalidArgument).WithError(e.Message);
            }

        }
        
    }



}
