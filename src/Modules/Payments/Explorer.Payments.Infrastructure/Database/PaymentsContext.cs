
﻿using Explorer.Payments.Core.Domain.BundlePayRecords;

﻿using Explorer.Payments.Core.Domain;

using Explorer.Payments.Core.Domain.ShoppingCarts;
//using Explorer.Tours.Core.Domain.TourExecutions;
//using Explorer.Tours.Core.Domain.Tours;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Payments.Infrastructure.Database
{
    public class PaymentsContext : DbContext
    {
        public DbSet<BundlePayRecord> BundlePayRecords { get; set; }    
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<TourPurchaseToken> TourPurchaseTokens { get; set; }
        public DbSet<TourSale> TourSale { get; set; }
        public DbSet<Coupon> Coupons { get; set; }


        public PaymentsContext(DbContextOptions<PaymentsContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("payments");

            modelBuilder.Entity<ShoppingCart>().Property(item => item.OrderItems).HasColumnType("jsonb");

        }

    }
}
