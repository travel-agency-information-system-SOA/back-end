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
        public int TourExecutionId { get; init; }
        public DateTime LastActivity { get; init; }
        public int Latitude { get; init; }
        public int Longitude { get; init; }

        public TourExecutionPosition(int tourExecutionId, DateTime lastActivity, int latitude, int longitude)
        {
            TourExecutionId = tourExecutionId;
            LastActivity = lastActivity;
            Latitude = latitude;
            Longitude = longitude;
            Validate();
        }

        private void Validate()
        {
            if (TourExecutionId <= 0)
                throw new ArgumentException("TourExecutionId must be a positive integer.");
        }

    }
}
