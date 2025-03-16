using Hangfire;
using Recipes.Application;
using Recipes.Infrastructure;
using Recipes.Infrastructure.Recipes.Jobs;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationDependencies()
    .AddInfrastructureDependencies(null!);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "/openapi/{documentName}.json";
    });
    app.MapScalarApiReference();
}

app.MapGet("/", () => "Hello World!");

var job = app.Services.GetRequiredService<NewsletterRecurringJob>();
RecurringJob.AddOrUpdate("newsletter-recurring-job", (CancellationToken token) => job.ExecuteAsync(token),
    Cron.Weekly(DayOfWeek.Monday));
app.Run();