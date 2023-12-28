using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos.ShoppingCartDtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public.ShoppingCart
{
    public interface ITourPurchaseTokenService
    {
        //Result<PagedResult<TourPurchaseTokenDto>> GetPurchasedTours(int touristId, int page, int pageSiz);
        Result<PagedResult<TourPurchaseTokenDto>> GetPaged(int page, int pageSize);
    }
}
