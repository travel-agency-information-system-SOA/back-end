using Explorer.BuildingBlocks.Core.Domain;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database.Repositories
{
    public class CouponRepository : ICouponRepository
    {
        private readonly PaymentsContext _context;
        private readonly DbSet<Coupon> _coupons;

        public CouponRepository(PaymentsContext context)
        {
            _context = context;
            _coupons = context.Coupons;
        }
        /*public Coupon GetByCodeAndTourId(string code, int tourId)
        {
            var task = _coupons.GetPagedById(1, int.MaxValue).Result.Results.FirstOrDefault(c => c.Code.Equals(code) && c.TourId == tourId && !c.IsExpired());
            return task;
        }*/

        public Coupon GetByCode(string code)
        {
            var task = _coupons.GetPagedById(1, int.MaxValue).Result.Results.FirstOrDefault(c => c.Code.Equals(code) && !c.IsExpired());
            return task;
        }


        public Result<List<Coupon>> GetByAuthorId(int authorId)
        {
            var task = _coupons.GetPagedById(1, int.MaxValue).Result.Results.Where(c => c.AuthorId == authorId).ToList();
            return task;
        }

        public Coupon Create(Coupon coupon)
        {
            coupon.GenerateCode();
            _coupons.Add(coupon);
            _context.SaveChanges();
            return coupon;
        }


    }
}
