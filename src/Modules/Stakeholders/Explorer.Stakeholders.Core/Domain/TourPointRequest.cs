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
        public int Id {  get; init; }
        public int TourPointId {  get; init; }  
        public Status Status { get; private set; }
        public TourPointRequest() {
            Status = Status.Rejected;
        }   
        public TourPointRequest(int id,int tourPointId,Status status)
        {
            Id = id;
            TourPointId = tourPointId;
            Status = status;
        }

        public void AcceptRequest()
        {
            Status = Status.Accepted;
        }
    }
}
