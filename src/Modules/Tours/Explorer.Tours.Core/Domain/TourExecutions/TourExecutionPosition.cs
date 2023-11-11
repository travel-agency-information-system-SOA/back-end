using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.TourExecutions
{
    public class TourExecutionPosition : Entity
    {
        public long TourExecutionId { get; private set; }
        public DateTime LastActivity { get; private set; }
        public int Latitude { get; private set; }
        public int Longitude { get; private set; }
        public TourExecution Execution { get; private set; } = null!;


        public TourExecutionPosition(int tourExecutionId, DateTime lastActivity, int latitude, int longitude)
        {
            TourExecutionId = tourExecutionId;
            LastActivity = lastActivity;
            Latitude = latitude;
            Longitude = longitude;
            Validate();
        }

        public TourExecutionPosition() { }

        private void Validate()
        {
            if (TourExecutionId < 0)
                throw new ArgumentException("TourExecutionId must be a positive integer.");
        }

        public void UpdateFrom(TourExecutionPosition updatedPosition)
        {
            this.TourExecutionId = updatedPosition.TourExecutionId;
            this.LastActivity = updatedPosition.LastActivity;
            this.Latitude = updatedPosition.Latitude;
            this.Longitude = updatedPosition.Longitude;

        }

    }
}
