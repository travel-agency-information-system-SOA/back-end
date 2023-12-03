using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    [Route("api/administration/tourSale")]
    public class TourSaleController : BaseApiController
    {
        //servis
        //constr

        [Authorize(Policy = "authorPolicy")]
        [HttpGet("{id:int}")]
        public ActionResult<PagedResult<TourSaleDto>> GetAllByAuthor([FromQuery] int page, [FromQuery] int pageSize, int id)
        {
            throw new NotImplementedException();
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpPost]
        public ActionResult<PagedResult<TourSaleDto>> Create([FromBody] TourSaleDto tourSale)
        {
            throw new NotImplementedException();
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpPut("{id:int}")]
        public ActionResult<TourSaleDto> Update([FromBody] TourSaleDto tourSale)
        {
            throw new NotImplementedException();
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            throw new NotImplementedException();
        }

        //salje turu prima popust
        [Authorize(Policy = "authorPolicy")]
        [HttpGet("tour/{id:int}")]
        public ActionResult<TourSaleDto> GetDiscount(int id)
        {
            throw new NotImplementedException();
        }
    }
}
