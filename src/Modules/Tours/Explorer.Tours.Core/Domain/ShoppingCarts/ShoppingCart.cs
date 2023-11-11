using Explorer.BuildingBlocks.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Explorer.Tours.Core.Domain.ShoppingCarts
{
    public class ShoppingCart : Entity
    {
        public int TouristId { get; set; }
        public ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

        public double Total {  get; set; }  //total price of all OrderItems that are in ShoppingCart


        public ShoppingCart(int touristId)
        {
            if (touristId == 0) throw new ArgumentException("Invalid tourdistId.");
            TouristId = touristId;

            OrderItems = new List<OrderItem>();
            Total = 0;  
        }

        public void calculateTotal() 
        {
            foreach(var orderItem in OrderItems)
            {
                Total += orderItem.Price;
            }
        }

    }
}
