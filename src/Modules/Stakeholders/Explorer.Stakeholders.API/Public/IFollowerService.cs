using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IFollowerService
    {
        Result<List<FollowerDto>> GetByUserId(int userId);
        Result<FollowerDto> Create(FollowerDto followerDto);
        Result<FollowerDto> Update(FollowerDto followerDto);
        Result Delete(int id);
    }
}
