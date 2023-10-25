using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Mappers;
using Explorer.Tours.Core.UseCases.Administration;
using Explorer.Tours.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Tours.Infrastructure;

public static class ToursStartup
{
    public static IServiceCollection ConfigureToursModule(this IServiceCollection services)
    {
        // Registers all profiles since it works on the assembly
        services.AddAutoMapper(typeof(ToursProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }
    
    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IEquipmentService, EquipmentService>();
        services.AddScoped<ITourObjectService, TourObjectService>();
<<<<<<< HEAD
        services.AddScoped<IObjInTourService, ObjInTourService>();
=======
        services.AddScoped<ITourService, TourService>();
        services.AddScoped<ITourEquipmentService,TourEquipmentService>();
        services.AddScoped<ITourPointService, TourPointService>();
        services.AddScoped<ITourKeyPointService, TourKeyPointService>();
>>>>>>> e3b022f87b0a07bf6f699568991aac84175f429d
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Equipment>), typeof(CrudDatabaseRepository<Equipment, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<TourObject>), typeof(CrudDatabaseRepository<TourObject, ToursContext>));
<<<<<<< HEAD
        services.AddScoped(typeof(ICrudRepository<ObjInTour>), typeof(CrudDatabaseRepository<ObjInTour, ToursContext>));
=======
        services.AddScoped(typeof(ICrudRepository<Tour>), typeof(CrudDatabaseRepository<Tour, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<TourEquipment>), typeof(CrudDatabaseRepository<TourEquipment, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<TourPoint>), typeof(CrudDatabaseRepository<TourPoint, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<TourKeyPoint>), typeof(CrudDatabaseRepository<TourKeyPoint, ToursContext>));

>>>>>>> e3b022f87b0a07bf6f699568991aac84175f429d

        services.AddDbContext<ToursContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("tours"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "tours")));
    }
}