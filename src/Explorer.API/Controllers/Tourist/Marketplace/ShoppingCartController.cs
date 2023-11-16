using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Marketplace;
using Explorer.Tours.Core.Domain.ShoppingCarts;
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


        [HttpGet("{id:int}")]  
        public ActionResult<ShoppingCartDto> GetByUserId(int id)
        {
            var result = _shoppingCartService.GetByUserId(id);
            return CreateResponse(result);
        }

       
        [HttpPut("/purchase/{cartId:int}")]
        public ActionResult Purchase(int cartId)
        {
            var result = _shoppingCartService.Purchase(cartId);
            return CreateResponse(result);
        }

        
        [HttpPut("{cartId:long}/{tourId:int}")]
        public ActionResult<ShoppingCartDto> RemoveOrderItem(long cartId, int tourId )
        {
            var result = _shoppingCartService.RemoveOrderItem(cartId, tourId);
            return CreateResponse(result);
        }
        

    }
}
