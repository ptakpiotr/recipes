using Recipes.Application;
using Recipes.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationDependencies()
    .AddInfrastructureDependencies(null!);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();