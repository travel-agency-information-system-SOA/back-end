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
    public class FollowerRepository : IFollowerRepository
    {
        private readonly StakeholdersContext _context;
        private readonly DbSet<Follower> _followers;

        public FollowerRepository(StakeholdersContext context)
        {
            _context = context;
            _followers = _context.Followers;
        }

        public Result<List<Follower>> GetByUserId(int userId)
        {
            var task = _followers.GetPagedById(1, int.MaxValue).Result.Results.Where(f => f.UserId == userId).ToList();
            return task;
        }
    }
}
