using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourReviewDto
    {
        public int Id { get; set; }
        public int Grade { get; set; }
        public string Comment { get; set; }
        public int TouristId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime ReviewDate { get; set; }
        public List<string> Images { get; set; }
        public long TourId { get; set; }
    }
}
