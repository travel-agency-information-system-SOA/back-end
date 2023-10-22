using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using FluentResults;

namespace Explorer.Stakeholders.API.Public
{
    public interface IAccountManagementService
    {
        Result<List<AccountDto>> GetAllAccounts();
        Result<AccountDto> BlockOrUnblock(AccountDto account);

    }
}
