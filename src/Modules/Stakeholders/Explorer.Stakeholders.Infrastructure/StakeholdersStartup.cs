using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.BuildingBlocks.Infrastructure.Database;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.API.Public;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;
using Explorer.Stakeholders.Core.Mappers;
using Explorer.Stakeholders.Core.UseCases;
using Explorer.Stakeholders.Infrastructure.Authentication;
using Explorer.Stakeholders.Infrastructure.Database;
using Explorer.Stakeholders.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Explorer.Stakeholders.Infrastructure;

public static class StakeholdersStartup
{
    public static IServiceCollection ConfigureStakeholdersModule(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(StakeholderProfile).Assembly);
        SetupCore(services);
        SetupInfrastructure(services);
        return services;
    }

    
    private static void SetupCore(IServiceCollection services)
    {

        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<ITokenGenerator, JwtGenerator>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<IAccountManagementService, AccountManagementService>();
        services.AddScoped<IAppRatingService, AppRatingService>();  
        services.AddScoped<IClubService, ClubService>();

        services.AddScoped<ITourPointRequestService, TourPointRequestService>();
        services.AddScoped<IInternalTourPointRequestService,TourPointRequestService>();
        services.AddScoped<IUserService,UserService>();
        services.AddScoped<ITourObjectRequestService, TourObjectRequestService>();
       services.AddScoped<IInternalTourObjectRequestService, TourObjectRequestService>();
        services.AddScoped<IRequestResponseNotificationService, RequestResponseNotificationService>();

        services.AddScoped<IUserPositionService, UserPositionService>();
        services.AddScoped<IInternalUserService, InternalUserService>();
        services.AddScoped<ITouristXPService, TouristXPService>();
        services.AddScoped<IUserMileageService, UserMileageService>();
        services.AddScoped<IUserTourMileageService, UserTourMileageService>();
        services.AddScoped<IFollowerService, FollowerService>();
        services.AddScoped<IFollowerMessageService, FollowerMessageService>();
    }

    private static void SetupInfrastructure(IServiceCollection services)
    {
        services.AddScoped(typeof(ICrudRepository<Person>), typeof(CrudDatabaseRepository<Person, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<Club>), typeof(CrudDatabaseRepository<Club, StakeholdersContext>));
        services.AddScoped<IUserRepository, UserDatabaseRepository>();
        services.AddScoped<IPersonRepository, PersonDatabaseRepository>();
        services.AddScoped<ITourPointRequestRepository, TourPointRequestRepository>();
        services.AddScoped<ITourObjectRequestRepository, TourObjectRequestRepository>();
        services.AddScoped(typeof(ICrudRepository<AppRating>), typeof(CrudDatabaseRepository<AppRating,StakeholdersContext>));

        services.AddScoped(typeof(ICrudRepository<TourPointRequest>), typeof(CrudDatabaseRepository<TourPointRequest, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<User>), typeof(CrudDatabaseRepository<User, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<TourObjectRequest>), typeof(CrudDatabaseRepository<TourObjectRequest, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<RequestResponseNotification>), typeof(CrudDatabaseRepository<RequestResponseNotification, StakeholdersContext>));

        services.AddScoped(typeof(ICrudRepository<UserPosition>),typeof(CrudDatabaseRepository<UserPosition,StakeholdersContext>));
        services.AddScoped<IUserPositionRepository, UserPositionRepository>();
        services.AddScoped<ITouristXPRepository, TouristXPRepository>();
        services.AddScoped(typeof(ICrudRepository<TouristXP>), typeof(CrudDatabaseRepository<TouristXP, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<UserMileage>), typeof(CrudDatabaseRepository<UserMileage, StakeholdersContext>));
        services.AddScoped<IUserMileageRepository, UserMileageRepository>();
        services.AddScoped(typeof(ICrudRepository<UserTourMileage>), typeof(CrudDatabaseRepository<UserTourMileage, StakeholdersContext>));
        services.AddScoped(typeof(ICrudRepository<Follower>), typeof(CrudDatabaseRepository<Follower, StakeholdersContext>));
        services.AddScoped<IFollowerRepository, FollowerRepository>();
        services.AddScoped(typeof(ICrudRepository<FollowerMessage>), typeof(CrudDatabaseRepository<FollowerMessage, StakeholdersContext>));
        services.AddScoped<IFollowerMessageRepository, FollowerMessageRepository>();


        services.AddScoped<IPasswordResetRepository, PasswordResetDatabaseRepository>();

        services.AddDbContext<StakeholdersContext>(opt =>
            opt.UseNpgsql(DbConnectionStringBuilder.Build("stakeholders"),
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "stakeholders")));
    }
}