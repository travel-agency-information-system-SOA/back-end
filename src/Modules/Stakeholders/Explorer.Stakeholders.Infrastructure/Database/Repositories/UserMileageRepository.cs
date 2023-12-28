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
    public class UserMileageRepository : IUserMileageRepository
    {
        private readonly DbSet<UserMileage> _userMileages;
        private readonly StakeholdersContext _context;

        public UserMileageRepository(StakeholdersContext context)
        {
            _context = context;
            _userMileages = _context.Set<UserMileage>();
        }

        public Result Update(UserMileage userMileage)
        {
            var existingUserMileage = _userMileages.SingleOrDefault(m => m.Id == userMileage.Id);
            _context.Entry(userMileage).State = EntityState.Detached;

            existingUserMileage.Mileage = userMileage.Mileage;
            existingUserMileage.MileageByMonth = userMileage.MileageByMonth;
            _context.SaveChanges();


            return Result.Ok();

        }
    }
}
