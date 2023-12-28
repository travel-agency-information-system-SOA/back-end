using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.API.Public.Marketplace;
using Explorer.Tours.API.Public.TourExecuting;
using Explorer.Tours.Core.Domain;
using Explorer.Tours.Core.Domain.RepositoryInterfaces;
using Explorer.Tours.Core.Domain.TourExecutions;
using Explorer.Tours.Core.Domain.Tours;
using Explorer.Tours.Core.Domain.Problems;
using Explorer.Tours.Core.Mappers;
using Explorer.Tours.Core.UseCases;
using Explorer.Tours.Core.UseCases.Administration;
using Explorer.Tours.Core.UseCases.Authoring;
using Explorer.Tours.Core.UseCases.Marketplace;
using Explorer.Tours.Core.UseCases.TourExecuting;
using Explorer.Tours.Infrastructure.Database;
using Explorer.Tours.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Explorer.Tours.Core.Domain.TourBundle;
using Explorer.Tours.Core.Domain.Competitions;
using Explorer.Tours.Infrastructure.Services;

namespace Explorer.Tours.Infrastructure;

public static class ToursStartup
{
    public static IServiceCollection ConfigureToursModule(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ToursProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }
    
    private static void SetupCore(IServiceCollection services)
    {
        services.AddScoped<IEquipmentService, EquipmentService>();



        services.AddScoped<ITouristEquipmentService, TouristEquipmentService>();



        services.AddScoped<ITourReviewService, TourReviewService>();


        services.AddScoped<ITourObjectService, TourObjectService>();
        services.AddScoped<IObjInTourService, ObjInTourService>();
        services.AddScoped<ITourService, TourService>();
        services.AddScoped<ITourEquipmentService,TourEquipmentService>();
        services.AddScoped<ITourPointService, TourPointService>();
        services.AddScoped<IPublicTourPointService, PublicTourPointService>();
        services.AddScoped<IGuideReviewService, GuideReviewService>();
        services.AddScoped<IPreferencesService, PreferencesService>();
        services.AddScoped<ITourExecutionService, TourExecutionService>();
        services.AddScoped<ITourPointExecutionService, TourPointExecutionService>();
        services.AddScoped<ITourExecutionPositionService, TourExecutionPositionService>();

        services.AddScoped<ITourObjectService, TourObjectService>();
        services.AddScoped<IPublicTourObjectService, PublicTourObjectService>();

        services.AddScoped<IProblemService, ProblemService>();
        services.AddScoped<IProblemMessageService, ProblemMessageService>();

        //services.AddScoped<IInternalTourService, TourService>();
        services.AddScoped<ITourBundleService, TourBundleService>();


        services.AddScoped<ICompetitionService, CompetitionService>();


		services.AddScoped<ICompetitionApplyService, CompetitionApplyService>();

	

        services.AddScoped<ITourStatisticsService, TourStatisticsService>();
    }


    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Equipment>), typeof(CrudDatabaseRepository<Equipment, ToursContext>));



        services.AddScoped(typeof(ICrudRepository<TouristEquipment>), typeof(CrudDatabaseRepository<TouristEquipment, ToursContext>));


        


        services.AddScoped(typeof(ICrudRepository<TourReview>), typeof(CrudDatabaseRepository<TourReview, ToursContext>));


        services.AddScoped(typeof(ICrudRepository<TourObject>), typeof(CrudDatabaseRepository<TourObject, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<ObjInTour>), typeof(CrudDatabaseRepository<ObjInTour, ToursContext>));

        services.AddScoped(typeof(ICrudRepository<Tour>), typeof(CrudDatabaseRepository<Tour, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<TourEquipment>), typeof(CrudDatabaseRepository<TourEquipment, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<TourPoint>), typeof(CrudDatabaseRepository<TourPoint, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<PublicTourPoint>), typeof(CrudDatabaseRepository<PublicTourPoint, ToursContext>));

        services.AddScoped(typeof(ICrudRepository<GuideReview>), typeof(CrudDatabaseRepository<GuideReview, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<Preferences>), typeof(CrudDatabaseRepository<Preferences, ToursContext>));
		services.AddScoped<ITourRepository, TourRepository>();

        services.AddScoped(typeof(ICrudRepository<TourExecution>),typeof(CrudDatabaseRepository<TourExecution,ToursContext>));
        services.AddScoped(typeof(ICrudRepository<TourPointExecution>), typeof(CrudDatabaseRepository<TourPointExecution, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<TourExecutionPosition>), typeof(CrudDatabaseRepository<TourExecutionPosition, ToursContext>));
        services.AddScoped<ITourExecutionRepository, TourExecutionRepository>();
   
        services.AddScoped(typeof(ICrudRepository<Problem>), typeof(CrudDatabaseRepository<Problem, ToursContext>));
        services.AddScoped<IProblemRepository, ProblemRepository>();



        services.AddScoped(typeof(ICrudRepository<PublicTourObject>), typeof(CrudDatabaseRepository<PublicTourObject, ToursContext>));


        services.AddScoped(typeof(ICrudRepository<ProblemMessage>), typeof(CrudDatabaseRepository<ProblemMessage, ToursContext>));
        services.AddScoped(typeof(ICrudRepository<TourBundle>), typeof(CrudDatabaseRepository<TourBundle, ToursContext>));
		services.AddScoped(typeof(ICrudRepository<Competition>), typeof(CrudDatabaseRepository<Competition, ToursContext>));
		services.AddScoped(typeof(ICrudRepository<CompetitionApply>), typeof(CrudDatabaseRepository<CompetitionApply, ToursContext>));
		services.AddScoped<ICompetitionRepository, CompetitionRepository>();

		services.AddDbContext<ToursContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("tours"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "tours")));
    }
}