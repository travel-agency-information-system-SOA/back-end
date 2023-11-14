using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Marketplace;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Tourist.Marketplace
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/shoppingcart")]
    public class ShoppingCartController : BaseApiController
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }


        [HttpGet]
        public ActionResult<PagedResult<ShoppingCartDto>> GetAll([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result =    _shoppingCartService.GetPaged(page, pageSize);
            return CreateResponse(result);
        }

        //dobavljanje ShoppingCart-a od bas tog trenutno ulogovanog turiste
        [HttpGet("{id:int}")]
        public ActionResult<ShoppingCartDto> GetByUserId(int userId, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _shoppingCartService.GetByUserId(userId, page, pageSize);
            return CreateResponse(result);
        }

        //racunanje trenutnog Totala za OrderIteme u korpi
        //[HttpGet("{id:int}")]
        //public ActionResult<double> CalculateTotalPrice([FromBody] ShoppingCartDto cart)
        //{
        //    var result = _shoppingCartService.CalculateTotalPrice(cart.OrderItems);
        //    return CreateResponse(result);
        //}


        [HttpPost]
        public ActionResult<ShoppingCartDto> Create([FromBody] ShoppingCartDto cart)
        {
            var result = _shoppingCartService.Create(cart);
            return CreateResponse(result);
        }

        [HttpPut]
        public ActionResult<ShoppingCartDto> Update([FromBody] ShoppingCartDto cart)
        {
            var result = _shoppingCartService.Update(cart);
            return CreateResponse(result);
        }

        /*
        [HttpPut("purchase")]
        public ActionResult<ShoppingCartDto> Purchase([FromBody] ShoppingCartDto cart)
        {
            var result = _shoppingCartService.Update(cart);
            return CreateResponse(result);
        }*/

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _shoppingCartService.Delete(id);
            return CreateResponse(result);
        }
    }
}
