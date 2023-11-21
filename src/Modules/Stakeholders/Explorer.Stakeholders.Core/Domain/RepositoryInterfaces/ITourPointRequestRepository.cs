using Explorer.Stakeholders.API.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain.RepositoryInterfaces
{
    public interface ITourPointRequestRepository
    {
        TourPointRequest AcceptRequest(int id, string comment);
        TourPointRequest RejectRequest(int id, string comment);
    }
}
