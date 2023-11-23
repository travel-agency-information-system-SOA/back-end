using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Dtos.ShoppingCartDtos
{
    public class ShoppingCartDto
    {
        public int? Id { get; set; } = 0;
        public int TouristId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public double Total { get; set; }

    }

}
