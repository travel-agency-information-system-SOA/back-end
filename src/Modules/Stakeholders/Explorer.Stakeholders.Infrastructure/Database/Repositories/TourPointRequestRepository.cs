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
        public TourPointRequestRepository(StakeholdersContext context)
        {
            _requests = context.TourPointRequests;
        }
        public TourPointRequest AcceptRequest(int id)
        {
            TourPointRequest result = new TourPointRequest();
            foreach (var request in _requests)
            {
                if (request.Id == id)
                {
                    request.AcceptRequest();
                    result = request;
                }
            }
            return result;
        }

        public TourPointRequest RejectRequest(int id)
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
            return result;
        }
    }
}
