using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourObjectDto
    {
        public int? Id { get; set; }

        public int? Tourid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
