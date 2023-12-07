using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos.ShoppingCartDtos
{
    public class OrderItemDto
    {
        public string TourName { get; set; }
        public double Price { get; set; }
        public int IdTour { get; set; }
    }
}
