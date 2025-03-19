using Hangfire;
using Hangfire.Common;
using Recipes.Application;
using Recipes.Infrastructure;
using Recipes.Infrastructure.Common.Options;
using Recipes.Infrastructure.Recipes.Jobs;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<S3Options>()
    .Bind(builder.Configuration.GetSection("S3"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<StorageOptions>()
    .Bind(builder.Configuration.GetSection("ConnectionStrings"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddOptions<EmailOptions>()
    .Bind(builder.Configuration.GetSection("Email"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddApplicationDependencies()
    .AddInfrastructureDependencies(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.MapScalarApiReference();
}

app.MapControllers();

using var jobServiceScope = app.Services.CreateScope();
var job = jobServiceScope.ServiceProvider.GetService<NewsletterRecurringJob>();

RecurringJob.AddOrUpdate("newsletter-recurring-job", () => job.ExecuteAsync(CancellationToken.None),
    Cron.Weekly(DayOfWeek.Monday));

app.Run();