using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain
{
    public class TourKeyPoint : Entity
    {
        public long TourId { get; set; }
        public long PointId { get; set; }

        public TourKeyPoint()
        {

        }

        public TourKeyPoint(long tourId, long pointId)
        {
            TourId = tourId;
            PointId = pointId;
        }
    }
}
