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
    public class TourObjectRequestRepository :ITourObjectRequestRepository
    {

        private readonly DbSet<TourObjectRequest> _requests;
        private readonly StakeholdersContext _context;
        public TourObjectRequestRepository(StakeholdersContext context)
        {
            _context = context;
            _requests = _context.Set<TourObjectRequest>();
        }
        public TourObjectRequest AcceptRequest(int id, string comment)
        {
            TourObjectRequest request = new TourObjectRequest();
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

        public TourObjectRequest RejectRequest(int id, string comment)
        {
            TourObjectRequest result = new TourObjectRequest();
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
