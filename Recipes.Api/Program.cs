using Recipes.Application;
using Recipes.Infrastructure;
using Recipes.Infrastructure.Common.Identity;
using Recipes.Infrastructure.Common.Options;
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

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.MapScalarApiReference();
}

app.MapControllers();

app.MapGet("/", () => Results.Ok());

app.MapGet("/logout", () => Results.SignOut(new()
{
    RedirectUri = "/"
}, [IdentityConstants.CookieAuthScheme]));

app.MapGet("/login", () => Results.Challenge(new()
{
    RedirectUri = "/"
}, [IdentityConstants.GithubAuthScheme]));

// using var jobServiceScope = app.Services.CreateScope();
// var job = jobServiceScope.ServiceProvider.GetService<NewsletterRecurringJob>();
//
// RecurringJob.AddOrUpdate("newsletter-recurring-job", () => job.ExecuteAsync(CancellationToken.None),
//     Cron.Weekly(DayOfWeek.Monday));

app.Run();