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

      

        
        [HttpGet("{id:int}")]          //dobavljanje ShoppingCart-a od bas tog trenutno ulogovanog turiste
        public ActionResult<ShoppingCartDto> GetByUserId(int id)
        {
            var result = _shoppingCartService.GetByUserId(id);
            return CreateResponse(result);
        }

       
        
        [HttpPut("/purchase")]
        public ActionResult Purchase([FromBody] ShoppingCartDto cart)
        {
            var result = _shoppingCartService.Purchase(cart);
            return CreateResponse(result);
        }

        /*
        //Remove Order Item -  puca pri dobavljanju shoppingCart-a na osnovu ovog cartId
        [HttpPut("{cartId:long}/{tourId:int}")]
        public ActionResult RemoveOrderItem(long cartId, int tourId )
        {

                var result = _shoppingCartService.RemoveOrderItem(cartId, tourId);
                return CreateResponse(result);
        }
        */


        /*
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _shoppingCartService.Delete(id);
            return CreateResponse(result);
        }*/





    }
}
