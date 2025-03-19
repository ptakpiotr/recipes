using Microsoft.EntityFrameworkCore;
using Recipes.Application.Recipes.Repositories;
using Recipes.Domain.Recipes.Models;
using Recipes.Infrastructure.Common.Data;

namespace Recipes.Infrastructure.Recipes.Repositories;

public class RatingsRepository(AppDbContext ctx) : IRatingsRepository
{
    public async Task<IList<RatingModel>> GetRatingsForRecipeAsync(Guid recipeId, CancellationToken token)
    {
        return await ctx.Ratings.AsNoTracking().Where(r => r.RecipeId == recipeId).ToListAsync(token)
            .ConfigureAwait(ConfigureAwaitOptions.None);
    }

    public async Task<RatingModel> CreateRatingForRecipeAsync(RatingModel rating, CancellationToken token)
    {
        await ctx.Ratings.AddAsync(rating, token).ConfigureAwait(false);

        return rating;
    }

    public Task SaveChangesAsync(CancellationToken token)
    {
        return ctx.SaveChangesAsync(token);
    }
}