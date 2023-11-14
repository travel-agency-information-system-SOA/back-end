using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos.TourExecutionsDTO
{
    public class TourExecutionDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TourId { get; set; }
        public string Status { get; set; }
        public List<TourPointExecutionDto> TourPoints { get; set; } 
        public TourExecutionPositionDto Position { get; set; }
        public TourDTO Tour { get; set; }

    }
}
