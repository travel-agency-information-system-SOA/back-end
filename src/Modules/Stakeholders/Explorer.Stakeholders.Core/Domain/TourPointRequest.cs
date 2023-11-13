using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Stakeholders.Core.Domain
{
    public class TourPointRequest : Entity
    {
        public int AuthorId {  get; init; }
        public int TourPointId {  get; init; }  
        public Status Status { get; private set; }

        public TourPointRequest() { }
        public TourPointRequest(int id,int tourPointId)
        {
            AuthorId = id;
            TourPointId = tourPointId;
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
