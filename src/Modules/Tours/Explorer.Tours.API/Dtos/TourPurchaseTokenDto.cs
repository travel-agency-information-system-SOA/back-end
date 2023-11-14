using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Dtos
{
    public class TourPurchaseTokenDto
    {
        public int TouristId { get; set; }
        public List<int> PurchasedTours { get; set; }
    }
}
