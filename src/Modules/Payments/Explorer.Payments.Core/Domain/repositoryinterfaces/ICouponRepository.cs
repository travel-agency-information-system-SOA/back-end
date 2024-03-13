using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Payments.API.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Core.Domain.RepositoryInterfaces
{
    public interface ICouponRepository
    {
        //Coupon GetByCodeAndTourId(string code, int tourId);
        Coupon GetByCode(string code);
        Result<List<Coupon>> GetByAuthorId(int authorId);
        Coupon Create(Coupon coupon);
    }
}
