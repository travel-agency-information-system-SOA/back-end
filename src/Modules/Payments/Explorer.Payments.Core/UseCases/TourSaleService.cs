using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.Domain;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.Core.Domain;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.UseCases
{
    public class TourSaleService : CrudService<TourSaleDto,TourSale>, ITourSaleService
    {
        public TourSaleService(ICrudRepository<TourSale> repository, IMapper mapper) : base(repository, mapper) { }

        public Result<PagedResult<TourSaleDto>> GetAllByAuthor(int page, int pageSize, int author)
        {
            var allSales = GetPaged(page, pageSize).Value;
            var filteredSales = allSales.Results.Where(sa => sa.AuthorId == author).ToList();
            var pagedResult = new PagedResult<TourSaleDto>(filteredSales, filteredSales.Count);

            return Result.Ok(pagedResult); //datumi mozda jebem mu seme
        }

        public Result<TourSaleDto> GetDiscount(int id)
        {
            throw new NotImplementedException(); //aaaaaaaaaaaa
        }
    }
}
