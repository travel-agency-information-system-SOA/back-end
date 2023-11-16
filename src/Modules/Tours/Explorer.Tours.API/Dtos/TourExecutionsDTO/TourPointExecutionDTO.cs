using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourExecutionsDTO
{
    public class TourPointExecutionDto
    {
        public int Id { get; set; }
        public int TourPointId { get; set; }
        public int TourExecutionId { get; set; }
        public DateTime? CompletionTime { get; set; }
        public bool Completed { get; set; }
        public string Secret { get; set; }
        public TourPointDto? TourPoint { get; set; }
        public TourExecutionDto? TourExecutionDto { get; set; }

    }
}
