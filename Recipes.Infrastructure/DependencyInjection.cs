using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Recipes.Application.Users.Repositories;
using Recipes.Application.Users.Services;
using Recipes.Infrastructure.Common.Data;
using Recipes.Infrastructure.Common.Options;
using Recipes.Infrastructure.Users.Repositories;
using Recipes.Infrastructure.Users.Services;

namespace Recipes.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services,
        StorageOptions storageOptions)
    {
        services.AddDbContext<AppDbContext>(opts => { opts.UseNpgsql(storageOptions.App); });

        services.AddStackExchangeRedisCache(opts => { opts.Configuration = storageOptions.Redis; });

        services.AddHangfire(opts => { opts.UsePostgreSqlStorage(storageOptions.Hangfire); });

        services.AddScoped<IRolesRepository, RolesRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();

        services.AddScoped<IUserService, UserService>();

        return services;
    }
}