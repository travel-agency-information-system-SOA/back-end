using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    internal class UserPositionRepository:IUserPositionRepository
    {
        private readonly DbSet<UserPosition> _userPositions;
        private readonly StakeholdersContext _context;

        public UserPositionRepository(StakeholdersContext context)
        {
            _context = context;
            _userPositions = _context.Set<UserPosition>();
        }

        public UserPosition GetByUserId(int id, int page, int pageSize)
        {
            var userPosition = _userPositions
                .SingleOrDefault(te => te.UserId == id);
                

            return userPosition;
        }
    }
}
