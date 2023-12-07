using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.ShoppingCarts
{
    public class OrderItem : ValueObject
    {
        public string TourName { get; set; }
        public double Price { get; set; }
        public int IdTour { get; set; }



        [JsonConstructor]
        public OrderItem(string tourName, double price, int idTour)
        {
            if (string.IsNullOrEmpty(tourName)) throw new ArgumentException("Invalid TourName.");
            if (idTour == 0) throw new ArgumentException("Invalid IdTour.");
            if (price < 0) throw new ArgumentException("Invalid Price.");

            TourName = tourName;
            Price = price;
            IdTour = idTour;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TourName;
            yield return Price;
            yield return IdTour;
        }

        public TourPurchaseToken ToPurchaseToken(int touristId)
        {
            return new TourPurchaseToken(touristId, IdTour);
        }
    }
}
