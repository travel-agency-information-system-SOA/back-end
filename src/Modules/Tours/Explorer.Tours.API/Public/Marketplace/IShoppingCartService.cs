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
        Result<PagedResult<ShoppingCartDto>> GetPaged(int page, int pageSize);
        Result<PagedResult<ShoppingCartDto>> GetAll(int page, int pageSize);
        Result<PagedResult<ShoppingCartDto>> GetByUserId(int touristId, int page, int pageSize);

        Result<ShoppingCartDto> Create(ShoppingCartDto cartDto);
        Result<ShoppingCartDto> Update(ShoppingCartDto cartDto);
        Result<ShoppingCartDto> Delete(int id);
       
    }
}
