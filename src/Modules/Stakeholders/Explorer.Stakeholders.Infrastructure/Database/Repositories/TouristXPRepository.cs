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
    public class TouristXPRepository : ITouristXPRepository
    {
        private readonly DbSet<TouristXP> _touristXPs;
        private readonly StakeholdersContext _context;
        public TouristXPRepository(StakeholdersContext context)
        {
            _context = context;
            _touristXPs = _context.Set<TouristXP>();
        }
        public TouristXP AddExperience(int id, int ammount)
        {
            TouristXP touristXP = new TouristXP();
            foreach(var t in _touristXPs) 
            {
                if(t.TouristId == id) 
                {
                    int previousXP = 0;
                    t.AddExperience(ammount);
                    for(int i = 1; i <= t.Level; i++)
                    {
                        previousXP += i * 10; 
                    }
                    for(int i = 1; i < 1000; i++)
                    {
                        if (t.Experience > ((t.Level + i) * 10 + previousXP))
                        {
                            t.LevelUp(i);
                            previousXP += (t.Level + i) * 10;
                        }
                    }
                }
            }
            _context.SaveChanges();
            return touristXP;
        }
    }
}
