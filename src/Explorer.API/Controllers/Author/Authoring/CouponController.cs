using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Authoring;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Explorer.API.Controllers.Author.Authoring
{

    [Route("api/authoring/coupon")]
    public class CouponController : BaseApiController
    {
        private readonly ICouponService _couponService;

        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpPost]
        public ActionResult<CouponDto> Create([FromBody] CouponDto coupon)
        {
            var result = _couponService.Create(coupon);

            return CreateResponse(result);
        }

        //[Authorize(Policy = "authorPolicy")]
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var result = _couponService.Delete(id);
            return CreateResponse(result);
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpPut("{id:int}")]
        public ActionResult<CouponDto> Update([FromBody] CouponDto couponDto)
        {
            var result = _couponService.Update(couponDto);
            return CreateResponse(result);
        }

        /*[HttpGet("getByCode")]
        public ActionResult<CouponDto> GetByCodeAndTourId([FromQuery] int tourId, [FromQuery] string code)
        {
            var result = _couponService.GetByCodeAndTourId(code, tourId);
            return CreateResponse(result);
        }*/

        [HttpGet("getByCode")]
        public ActionResult<CouponDto> GetByCode([FromQuery] string code)
        {
            var result = _couponService.GetByCode(code);
            return CreateResponse(result);
        }

        [Authorize(Policy = "authorPolicy")]
        [HttpGet("{authorId:int}")]
        public ActionResult<List<CouponDto>> GetByAuthorId(int authorId)
        {
            var result = _couponService.GetByAuthorId(authorId);
            return CreateResponse(result);
        }






    }
}
