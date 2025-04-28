using Recipes.Application.Recipes.DTO;

namespace Recipes.Application.Recipes.Services;

public interface IRatingsService
{
    Task<OneOf<SuccessWithValue<IReadOnlyList<RatingReadDto>>, Error>> GetRatingsForRecipeAsync(Guid recipeId,
        CancellationToken token);

    Task<OneOf<Success, Error>> DeleteRecipeAsync(RatingDeleteDto rating, Guid userId,
        CancellationToken token);

    Task<OneOf<SuccessWithValue<RatingReadDto>, Error>> CreateRatingForRecipeAsync(RatingCreateDto rating,
        CancellationToken token);

    Task<OneOf<Success, Error>> UpdateRatingAsync(RatingEditDto rating, Guid userId, CancellationToken token);

    Task<OneOf<Success, Error>> CheckOwnershipAsync(Guid userId, Guid ratingId, CancellationToken token);
}