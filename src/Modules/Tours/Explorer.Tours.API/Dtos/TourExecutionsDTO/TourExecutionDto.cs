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

        public DateTime LastActivity { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
        public string Status { get; set; }
    }
}
