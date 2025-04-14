using Microsoft.EntityFrameworkCore;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Repositories;
using Recipes.Domain.Common.Enums;
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

    public async Task<RatingModel?> GetRatingsAsync(Guid id, CancellationToken token)
    {
        return await ctx.Ratings.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);
    }

    public async Task<RatingModel> CreateRatingForRecipeAsync(RatingModel rating, CancellationToken token)
    {
        await ctx.Ratings.AddAsync(rating, token).ConfigureAwait(false);

        return rating;
    }

    public async Task<DeleteType> DeleteRatingAsync(RatingDeleteDto rating, CancellationToken token)
    {
        var ratingForDeletion =
            await ctx.Ratings.FirstOrDefaultAsync(u => u.Id == rating.Id, token).ConfigureAwait(false);

        if (ratingForDeletion is null)
        {
            return DeleteType.DeleteFailed;
        }

        ctx.Remove(ratingForDeletion);

        return DeleteType.DeleteSuccessful;
    }

    public Task SaveChangesAsync(CancellationToken token)
    {
        return ctx.SaveChangesAsync(token);
    }
}