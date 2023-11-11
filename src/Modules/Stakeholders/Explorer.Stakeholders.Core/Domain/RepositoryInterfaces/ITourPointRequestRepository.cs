using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface ITourPointRequestRepository
    {
        TourPointRequest AcceptRequest(int id);
        TourPointRequest RejectRequest(int id);
    }
}
