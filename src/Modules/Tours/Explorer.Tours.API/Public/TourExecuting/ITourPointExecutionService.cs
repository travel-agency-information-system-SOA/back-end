using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.TourExecuting
{
    public interface ITourPointExecutionService
    {
        public bool isTourPointCompletedByTourist(int touristId, int tourPointId);

        public int getMaxCompletedTourPointPerTourist(int touristId, List<long> tourExecutions);
    }
}
