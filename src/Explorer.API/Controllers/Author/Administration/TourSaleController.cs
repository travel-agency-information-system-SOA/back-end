using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Administration
{
    [Route("api/administration/tourSale")]
    public class TourSaleController : BaseApiController
    {
        private readonly ITourSaleService _tourSaleService;
        public TourSaleController(ITourSaleService tourSaleService)
        {
            _tourSaleService = tourSaleService;
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpGet("{id:int}")]
        public ActionResult<PagedResult<TourSaleDto>> GetAllByAuthor([FromQuery] int page, [FromQuery] int pageSize, int id)
        {
            var result = _tourSaleService.GetAllByAuthor(page, pageSize, id);
            return CreateResponse(result);
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpPost]
        public ActionResult<PagedResult<TourSaleDto>> Create([FromBody] TourSaleDto tourSale)
        {
            var result = _tourSaleService.Create(tourSale);
            return CreateResponse(result);
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpPut("{id:int}")]
        public ActionResult<TourSaleDto> Update([FromBody] TourSaleDto tourSale)
        {
            var result = _tourSaleService.Update(tourSale);
            return CreateResponse(result);
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _tourSaleService.Delete(id);
            return CreateResponse(result);
        }

        //salje turu prima popust
        //[Authorize(Policy = "authorPolicy")]
        [HttpGet("tour/{id:int}")]
        public ActionResult<int> GetDiscount(int id)
        {
            var result = _tourSaleService.GetDiscount(id);
            return CreateResponse(result);
        }
    }
}
