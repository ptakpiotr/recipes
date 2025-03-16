using Hangfire;
using MassTransit;
using Microsoft.Extensions.Logging;
using Recipes.Application.Recipes.Services;

namespace Recipes.Infrastructure.Recipes.Jobs;

public class NewsletterRecurringJob(
    IRecipeService recipeService,
    ILogger<NewsletterRecurringJob> logger,
    IPublishEndpoint publishEndpoint)
{
    [DisableConcurrentExecution(60)]
    public async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var recipe = await recipeService.GetRandomRecipeAsync(stoppingToken)
            .ConfigureAwait(ConfigureAwaitOptions.None);
        await recipe.Match<Task<bool>>(async (r) =>
            {
                await publishEndpoint.Publish(r, stoppingToken);
                return true;
            },
            (_) =>
            {
                logger.LogError("Couldn't send information");
                return Task.FromResult(false);
            }).ConfigureAwait(ConfigureAwaitOptions.None);
    }
}