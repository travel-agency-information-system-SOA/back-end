using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Infrastructure.Database.Repositories
{
    public class ClubRepository : IClubRepository
    {
        private readonly DbSet<Club> _clubs;

        public ClubRepository(StakeholdersContext context)
        {
            _clubs = context.Clubs;
        }
        public List<Club> GetByUserId(int userId)
        {
            return _clubs.Where(club => club.OwnerId == userId).ToList();
        }
    }
}
