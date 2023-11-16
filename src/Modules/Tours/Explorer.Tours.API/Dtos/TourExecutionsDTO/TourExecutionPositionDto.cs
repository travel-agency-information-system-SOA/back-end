using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourExecutionsDTO
{
    public class TourExecutionPositionDto
    {
        public int Id { get; set; }
        public long TourExecutionId { get; set; }
        public DateTime LastActivity { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public TourExecutionDto? TourExecutionDto { get; set; }
    }
}
