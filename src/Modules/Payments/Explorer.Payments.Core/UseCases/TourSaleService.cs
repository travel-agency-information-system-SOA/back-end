using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using FluentResults;

namespace Explorer.Payments.Core.UseCases
{
    public class TourSaleService : CrudService<TourSaleDto, TourSale>, ITourSaleService
    {
        public TourSaleService(ICrudRepository<TourSale> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<PagedResult<TourSaleDto>> GetAllByAuthor(int page, int pageSize, int author)
        {
            var allSales = GetPaged(page, pageSize).Value;

            DateTime today = DateTime.Today;
            var filteredSales = allSales.Results
                .Where(sa => sa.AuthorId == author && today <= sa.EndDate)
                .ToList();

            var pagedResult = new PagedResult<TourSaleDto>(filteredSales, filteredSales.Count);

            return Result.Ok(pagedResult);
        }

        public Result<int> GetDiscount(int id)
        {
            var allTourSales = GetPaged(0, 0).Value.Results;

            DateTime today = DateTime.Today;

            foreach (var tourSale in allTourSales)
            {
                if (today >= tourSale.StartDate && today <= tourSale.EndDate && tourSale.TourIds.Contains(id))
                {
                    return Result.Ok(tourSale.SalePercentage);
                }
            }

            return Result.Ok(0);
        }
    }
}
