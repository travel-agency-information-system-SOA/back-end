using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;

namespace Explorer.Payments.API.Public
{
    public interface ITourSaleService
    {
        Result<PagedResult<TourSaleDto>> GetPaged(int page, int pageSize);
        Result<TourSaleDto> Create(TourSaleDto tourSale);
        Result<TourSaleDto> Update(TourSaleDto tourSale);
        Result Delete(int id);
        Result<PagedResult<TourSaleDto>> GetAllByAuthor(int page, int pageSize, int author);
        Result<int> GetDiscount(int id);
    }
}
