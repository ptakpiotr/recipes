using AspNetCore.Scalar;
using Hangfire;
using Recipes.Application;
using Recipes.Infrastructure;
using Recipes.Infrastructure.Common.Identity;
using Recipes.Infrastructure.Common.Options;
using Recipes.Infrastructure.Recipes.Jobs;

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

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseScalar(options =>
    {
        options.RoutePrefix = "scalar";
        options.UseTheme(Theme.Moon);
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapReverseProxy();

app.MapGet("/", () => Results.LocalRedirect("/app"));

app.MapGet("/logout", () => Results.SignOut(new()
{
    RedirectUri = "/"
}, [IdentityConstants.CookieAuthScheme]));

app.MapGet("/login", () => Results.Challenge(new()
{
    RedirectUri = "/"
}, [IdentityConstants.GithubAuthScheme]));

app.Lifetime.ApplicationStarted.Register(() =>
{
    using var jobServiceScope = app.Services.CreateScope();
    var job = jobServiceScope.ServiceProvider.GetRequiredService<NewsletterRecurringJob>();
    var client = jobServiceScope.ServiceProvider.GetRequiredService<IRecurringJobManager>();

    client.AddOrUpdate("newsletter-recurring-job", () => job.ExecuteAsync(CancellationToken.None),
        Cron.Weekly(DayOfWeek.Monday));
});

app.Run();