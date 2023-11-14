using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;

namespace Explorer.Tours.Core.Domain.TourExecutions
{
    public class TourExecution:Entity
    {
        public int UserId { get; init; }
        public int TourId { get; init; }
        public TourExecutionStatus Status { get; init; }
        public ICollection<TourPointExecution> TourPoints { get; } = new List<TourPointExecution>();


        public TourExecution(int userId, int tourId, TourExecutionStatus status)
        {
            UserId = userId;
            TourId = tourId;
            Status = status;
            Validate();
        }

        private void Validate()
        {
            if (TourId <= 0)
                throw new ArgumentException("TourId must be a positive integer.");
            if (UserId <= 0)
                throw new ArgumentException("TourPointId must be a positive integer.");
        }
    }
}
