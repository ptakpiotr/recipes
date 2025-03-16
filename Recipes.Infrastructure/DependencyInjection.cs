using Hangfire;
using Hangfire.PostgreSql;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Recipes.Application.Common.Services;
using Recipes.Application.Users.Repositories;
using Recipes.Application.Users.Services;
using Recipes.Infrastructure.Common.Data;
using Recipes.Infrastructure.Common.Options;
using Recipes.Infrastructure.Common.Services;
using Recipes.Infrastructure.Recipes.Jobs;
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
        services.AddScoped<IFileService, AmazonFileService>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();

        services.AddScoped<NewsletterRecurringJob>();

        services.AddMassTransit(opts => { });

        return services;
    }
}