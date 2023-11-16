using Explorer.BuildingBlocks.Core.Domain;
using Explorer.Tours.API.Dtos;
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

        public double Total {  get; set; }  //total price of OrderItems in ShoppingCart


        public ShoppingCart(int touristId)
        {
            if (touristId == 0) throw new ArgumentException("Invalid tourdistId.");
            TouristId = touristId;

            OrderItems = new List<OrderItem>();
            Total = 0;  
        }

        public void RemoveOrderItem(OrderItem item)
        {
            OrderItems.Remove(item);
            CalculateTotal();
        }

        public void CalculateTotal() 
        {   
            Total = 0;
            foreach(var orderItem in OrderItems)
            {
                Total += orderItem.Price;
            }
        }
        
        // implement the metod in method for buying the Tour  ( MarketplaceService )
        public bool AddOrderItem(OrderItem item)
        {
            if(OrderItems.Contains(item)) return false;
            OrderItems.Add(item);   
            CalculateTotal();
            return true;
        }

    }
}
