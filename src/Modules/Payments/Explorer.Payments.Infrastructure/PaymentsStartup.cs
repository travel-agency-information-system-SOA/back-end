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
            services.AddScoped<IShoppingCartService, ShoppingCartService>();   //ShoppingCart
            services.AddScoped<ITourPurchaseTokenService, TourPurchaseTokenService>();  //Token
            services.AddScoped<ICouponService, CouponService>();
        }

        private static void SetupInfrastructure(IServiceCollection services)
        {

            services.AddScoped(typeof(ICrudRepository<ShoppingCart>), typeof(CrudDatabaseRepository<ShoppingCart, PaymentsContext>));
            services.AddScoped(typeof(ICrudRepository<TourPurchaseToken>), typeof(CrudDatabaseRepository<TourPurchaseToken, PaymentsContext>));
            services.AddScoped(typeof(ICrudRepository<Coupon>), typeof(CrudDatabaseRepository<Coupon, PaymentsContext>));
            services.AddScoped<ICouponRepository, CouponRepository>();


            services.AddDbContext<PaymentsContext>(opt =>
                opt.UseNpgsql(DbConnectionStringBuilder.Build("payments"),
                    x => x.MigrationsHistoryTable("__EFMigrationsHistory", "payments")));

        }
    }
}
