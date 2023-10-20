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

        public Result<List<AccountDto>> GetAll()
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
                var accountDto = new AccountDto(user.Id, user.Username, user.Password, _userRepository.GetPersonEmail(user.Id), user.GetPrimaryRoleName(), user.IsActive);
                accounts.Add(accountDto);
            }
            
            return accounts;
        }

        public Result<AccountDto> Block(AccountDto account)
        {
            if (account.IsActive)
            {
                var user = _userRepository.Get(account.UserId);
                user.IsActive = false;
                _userRepository.Update(user);

                account.IsActive = false;
                return account;
            }
            else
                return Result.Fail(FailureCode.InvalidArgument).WithError("Account is already blocked.");
        }

        
    }
}
