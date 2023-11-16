using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
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
    public class InternalUserService : IInternalUserService
    {
        private readonly IUserRepository _userRepository;

        public InternalUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Dictionary<long, string> GetUsernames(List<long> userIds)
        {
            var allUsers = _userRepository.GetAll();
            var filteredUsers = allUsers.Where(user => userIds.Contains(user.Id)).ToList();
            var accounts = filteredUsers.ToDictionary(user => user.Id, user => user.Username);

            return accounts;

        }

    }
}
