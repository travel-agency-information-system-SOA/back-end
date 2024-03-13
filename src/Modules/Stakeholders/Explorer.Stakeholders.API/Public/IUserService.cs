using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IUserService
    {
        Result<UserDto> Get(int userId);
        UserRole GetUserRole(int userId);
        Result<UserDto> ConfirmRegistration(string verificationToken);
    }
}
