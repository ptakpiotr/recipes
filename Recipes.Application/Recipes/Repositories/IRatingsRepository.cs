using Recipes.Application.Recipes.DTO;
using Recipes.Domain.Common.Enums;
using Recipes.Domain.Recipes.Models;

namespace Recipes.Application.Recipes.Repositories;

public interface IRatingsRepository
{
    Task<IList<RatingModel>> GetRatingsForRecipeAsync(Guid recipeId, CancellationToken token);

    Task<RatingModel?> GetRatingsAsync(Guid id, CancellationToken token);

    Task<RatingModel> CreateRatingForRecipeAsync(RatingModel rating, CancellationToken token);

    Task<DeleteType> DeleteRatingAsync(RatingDeleteDto rating, CancellationToken token);

    Task SaveChangesAsync(CancellationToken token);
}