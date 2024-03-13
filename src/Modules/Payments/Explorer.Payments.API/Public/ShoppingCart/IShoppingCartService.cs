using Explorer.Payments.API.Dtos.ShoppingCartDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public.ShoppingCart
{
    public interface IShoppingCartService
    {
        Result<ShoppingCartDto> GetByUserId(int touristId);
        Result<ShoppingCartDto> Purchase(int cartId);
        Result<ShoppingCartDto> RemoveOrderItem(long cartId, int tourId);
        Result<ShoppingCartDto> Buy(ShoppingCartDto cart);
        Result<ShoppingCartDto> Update(ShoppingCartDto cart);
    }
}
