using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Payments.Core.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Explorer.Payments.Core.Domain.ShoppingCarts;
using Explorer.Payments.Infrastructure.Database;
using Explorer.Payments.API.Public.ShoppingCart;
using Explorer.Payments.Core.UseCases.ShoppingCarts;
using Microsoft.EntityFrameworkCore;
using Explorer.Payments.API.Public;
using Explorer.Payments.Core.UseCases;
using Explorer.Payments.Core.Domain;
using Explorer.Payments.Core.Domain.RepositoryInterfaces;
using Explorer.Payments.Infrastructure.Database.Repositories;

using Explorer.Payments.API.Public.BundlePayRecord;
using Explorer.Payments.Core.UseCases.BundlePayRecords;
using Explorer.Payments.Core.Domain.BundlePayRecords;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

using Explorer.Payments.API.Public;
using Explorer.Payments.Core.UseCases;
using Explorer.Payments.Core.Domain;
using Explorer.Stakeholders.Core.UseCases;

namespace Explorer.Payments.Infrastructure
{
    public static class PaymentsStartup
    {
        public static IServiceCollection ConfigurePaymentsModule(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(PaymentsProfile).Assembly);
            SetupCore(services);
            SetupInfrastructure(services);
            return services;
        }

        private static void SetupCore(IServiceCollection services)
        {   
            services.AddScoped<IBundlePayRecordService, BundlePayRecordService>();  //BundlePayRecordService
            services.AddScoped<IShoppingCartService, ShoppingCartService>();   //ShoppingCart
            services.AddScoped<ITourPurchaseTokenService, TourPurchaseTokenService>();  //Token
            services.AddScoped<ITourSaleService, TourSaleService>();
            services.AddScoped<ICouponService, CouponService>();
            services.AddScoped<QRCodeService>();
            services.AddScoped<EmailSenderService>();
        }

        private static void SetupInfrastructure(IServiceCollection services)
        {
            //services.AddScoped<IBundlePayRecordsRepository, BundlePayRecordRepository>();
            services.AddScoped(typeof(ICrudRepository<BundlePayRecord>), typeof(CrudDatabaseRepository<BundlePayRecord, PaymentsContext>));
            services.AddScoped(typeof(ICrudRepository<ShoppingCart>), typeof(CrudDatabaseRepository<ShoppingCart, PaymentsContext>));
            services.AddScoped(typeof(ICrudRepository<TourPurchaseToken>), typeof(CrudDatabaseRepository<TourPurchaseToken, PaymentsContext>));
            services.AddScoped(typeof(ICrudRepository<Coupon>), typeof(CrudDatabaseRepository<Coupon, PaymentsContext>));
            services.AddScoped<ICouponRepository, CouponRepository>();

            services.AddScoped(typeof(ICrudRepository<TourSale>), typeof(CrudDatabaseRepository<TourSale, PaymentsContext>));

            services.AddDbContext<PaymentsContext>(opt =>
                opt.UseNpgsql(DbConnectionStringBuilder.Build("payments"),
                    x => x.MigrationsHistoryTable("__EFMigrationsHistory", "payments")));

        }
    }
}
