using Explorer.Stakeholders.API.Dtos;
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
    public class TourPointRequestRepository : ITourPointRequestRepository
    {
        private readonly DbSet<TourPointRequest> _requests;
        private readonly StakeholdersContext _context;
        public TourPointRequestRepository(StakeholdersContext context)
        {
            _context = context;
            _requests = _context.Set<TourPointRequest>();
        }
        public TourPointRequest AcceptRequest(int id,string comment)
        {
            TourPointRequest request = new TourPointRequest();
            foreach (var r in _requests)
            {
                if (id == r.Id)
                {
                    r.AcceptRequest();
                    request = r;
                }
            }
            _context.SaveChanges();
            return request;
        }

        public TourPointRequest RejectRequest(int id,string comment)
        {
            TourPointRequest result = new TourPointRequest();
            foreach (var request in _requests)
            {
                if (request.Id == id)
                {
                    request.RejectRequest();
                    result = request;
                }
            }
            _context.SaveChanges();
            return result;
        }

      
    }
}
