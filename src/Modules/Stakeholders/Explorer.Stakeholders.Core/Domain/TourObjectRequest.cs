using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class TourObjectRequest : Entity
    {
        public int AuthorId { get; init; }
        public int TourObjectId { get; init; }
        public Status Status { get; private set; }

        public TourObjectRequest() { }
        public TourObjectRequest(int id, int tourObjectId)
        {
            AuthorId = id;
            TourObjectId = tourObjectId;
            Status = Status.Onhold;
        }

        public void AcceptRequest()
        {
            Status = Status.Accepted;
        }
        public void RejectRequest()
        {
            Status = Status.Rejected;
        }
    }
}
