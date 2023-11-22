using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Explorer.Payments.API.Dtos.ShoppingCartDtos;
using Explorer.Payments.API.Public.ShoppingCart;

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
