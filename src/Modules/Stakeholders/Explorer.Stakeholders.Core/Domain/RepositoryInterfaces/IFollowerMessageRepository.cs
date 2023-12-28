using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface IFollowerMessageRepository
    {
        Result<List<FollowerMessage>> GetByFollowerId(int followerId);
    }
}
