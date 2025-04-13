using System.Text.Json;
using Amazon;
using Amazon.S3;
using FluentValidation;
using Hangfire;
using Hangfire.PostgreSql;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Recipes.Application.Common.Services;
using Recipes.Application.Common.Validators;
using Recipes.Application.Recipes.Events;
using Recipes.Application.Recipes.Repositories;
using Recipes.Application.Recipes.Services;
using Recipes.Application.Users.Repositories;
using Recipes.Application.Users.Services;
using Recipes.Infrastructure.Common.Data;
using Recipes.Infrastructure.Common.Identity;
using Recipes.Infrastructure.Common.Options;
using Recipes.Infrastructure.Common.Services;
using Recipes.Infrastructure.Recipes.Jobs;
using Recipes.Infrastructure.Recipes.Repositories;
using Recipes.Infrastructure.Recipes.Services;
using Recipes.Infrastructure.Users.Repositories;
using Recipes.Infrastructure.Users.Services;

namespace Recipes.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services,
        IConfiguration configuration)
    {
        StorageOptions storageOptions = new();
        S3Options s3Options = new();
        EmailOptions emailOptions = new();
        ExternalOptions externalOptions = new();

        configuration.GetSection("ConnectionStrings").Bind(storageOptions);
        configuration.GetSection("S3").Bind(s3Options);
        configuration.GetSection("Email").Bind(emailOptions);
        configuration.GetSection("External").Bind(externalOptions);

        services.AddIdentityConfiguration(configuration);

        services.AddDbContext<AppDbContext>(opts => { opts.UseNpgsql(storageOptions.App, o => o.UseVector()); });

        services.AddStackExchangeRedisCache(opts => { opts.Configuration = storageOptions.Redis; });

        services.AddHangfire(opts => { opts.UsePostgreSqlStorage(storageOptions.Hangfire); });

        services.AddFluentEmail(emailOptions.Email)
            .AddSmtpSender(emailOptions.Host, emailOptions.Port,
                emailOptions.Email, emailOptions.Password);

        services.AddSingleton<IAmazonS3, AmazonS3Client>((_) => new AmazonS3Client(s3Options.AccessKeyId,
            s3Options.SecretAccessKey, new AmazonS3Config()
            {
                RegionEndpoint = RegionEndpoint.USEast1,
                ServiceURL = s3Options.ConnStr,
                ForcePathStyle = true
            }));

        services.AddScoped<IRolesRepository, RolesRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IFileService, AmazonFileService>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();

        services.AddScoped<IIngredientsRepository, IngredientsRepository>();
        services.AddScoped<IRatingsRepository, RatingsRepository>();
        services.AddScoped<IRecipesRepository, RecipesRepository>();

        services.AddScoped<IRatingsService, RatingsService>();
        services.AddScoped<IIngredientsService, IngredientsService>();
        services.AddScoped<IRecipeService, RecipeService>();

        services.AddScoped<IEmbeddingsService, EmbeddingsService>();

        services.AddScoped<NewsletterRecurringJob>();

        services.AddMassTransit(opts =>
        {
            opts.AddConsumersFromNamespaceContaining<SendNewsletterDataEvent>();
            opts.UsingInMemory((context, cfg) => { cfg.ConfigureEndpoints(context); });
        });

        services.AddHttpClient("ai-client",(client) =>
        {
            client.BaseAddress = new Uri(externalOptions.AiService);
        });

        return services;
    }
}