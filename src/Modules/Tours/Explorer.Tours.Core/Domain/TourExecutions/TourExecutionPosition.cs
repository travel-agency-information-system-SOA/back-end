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

    }
}
