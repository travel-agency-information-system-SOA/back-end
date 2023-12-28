using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.API.Public
{
    public interface ICouponService
    {
        Result<CouponDto> Create(CouponDto couponDto);
        Result<CouponDto> Update(CouponDto couponDto);
        Result Delete(int id);
        //Result<CouponDto> GetByCodeAndTourId(string code, int tourId);
        Result<CouponDto> GetByCode(string code);
        Result<List<CouponDto>> GetByAuthorId(int authorId);
    }
}
