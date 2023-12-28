using Explorer.Stakeholders.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.API.Public
{
    public interface IFollowerMessageService
    {
        Result<List<FollowerMessageDto>> GetByFollowerId(int followerId);
        Result<FollowerMessageDto> MarkAsRead(FollowerMessageDto messageDto);
        Result<FollowerMessageDto> Create(FollowerMessageDto messageDto);
        Result<FollowerMessageDto> Update(FollowerMessageDto messageDto);
        Result Delete(int id);
    }
}
