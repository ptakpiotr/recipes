using Recipes.Domain.Recipes.Models;

namespace Recipes.Application.Recipes.Repositories;

public interface IRatingsRepository
{
    Task<IList<RatingModel>> GetRatingsForRecipeAsync(Guid recipeId, CancellationToken token);
    
    Task<RatingModel> CreateRatingForRecipeAsync(RatingModel rating, CancellationToken token);
    
    Task SaveChangesAsync(CancellationToken token);
}