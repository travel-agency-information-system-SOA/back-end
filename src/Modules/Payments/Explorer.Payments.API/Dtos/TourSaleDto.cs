using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos
{
    public class TourSaleDto
    {
        public int Id { get; set; }
        public List<int> TourIds { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int SalePercentage { get; set; }
        public int AuthorId { get; set; }
    }
}
