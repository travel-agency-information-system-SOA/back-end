using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourBundleDto
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public double Price { get; set; }

        public List<int> TourIds { get; set; }

        public String Status { get; set; }

        public TourBundleDto() { }
    }
}
