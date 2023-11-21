using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Tours.API.Public.Marketplace
{
    public interface IShoppingCartService
    {
        Result<ShoppingCartDto> GetByUserId(int touristId);

        Result <ShoppingCartDto> Purchase(ShoppingCartDto cart);

        Result <ShoppingCartDto> Buy(ShoppingCartDto cart);

        //Result<ShoppingCartDto> RemoveOrderItem(ShoppingCartDto cartDto, OrderItemDto item);
        Result<ShoppingCartDto> RemoveOrderItem(long cartId, int tourId);

    }
}

