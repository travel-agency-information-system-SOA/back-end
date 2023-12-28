using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class FollowerMessageRepository : IFollowerMessageRepository
    {
        private readonly StakeholdersContext _context;
        private readonly DbSet<FollowerMessage> _messages;

        public FollowerMessageRepository(StakeholdersContext context)
        {
            _context = context;
            _messages = _context.FollowerMessages;
        }

        public Result<List<FollowerMessage>> GetByFollowerId(int followerId)
        {
            var task = _messages.GetPaged(1, int.MaxValue).Result.Results.Where(m => m.FollowerId == followerId).ToList();
            return task;
        }
    }
}
