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
        public int UserId { get; private set; }
        public int TourId { get; private set; }
        public TourExecutionStatus Status { get; private set; }
        public List<TourPointExecution> TourPoints { get; } = new List<TourPointExecution>();
        public TourExecutionPosition? Position { get; private set; }


        public TourExecution(int userId, int tourId, TourExecutionStatus status)
        {
            UserId = userId;
            TourId = tourId;
            Status = status;
            Validate();
        }

        private void Validate()
        {
            if (TourId < 0)
                throw new ArgumentException("TourId must be a positive integer.");
            if (UserId < 0)
                throw new ArgumentException("TourPointId must be a positive integer.");
        }

        public void UpdateFrom(TourExecution updatedTourExecution)
        {
            UserId = updatedTourExecution.UserId;
            TourId = updatedTourExecution.TourId;
            Status = updatedTourExecution.Status;

            TourPoints.Clear();
            
            TourPoints.AddRange(updatedTourExecution.TourPoints);

            Position.UpdateFrom(updatedTourExecution.Position);
        }

        public void SetId(int id)
        {
            this.Id = id;
        }

    }
}
