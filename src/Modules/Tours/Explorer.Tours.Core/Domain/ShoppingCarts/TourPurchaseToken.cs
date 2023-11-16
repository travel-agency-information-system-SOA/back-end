using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.Core.Domain.Tours;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.Core.Domain.ShoppingCarts
{
    public class TourPurchaseToken : Entity
    {
        public int TouristId { get; set; }

        public int IdTour {  get; set; }

        public TourPurchaseToken(int touristId, int idTour) 
        {
            TouristId = touristId;
            IdTour = idTour;
        }

    }
}
