using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using OneOf;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Repositories;
using Recipes.Application.Recipes.Services;
using Recipes.Domain.Common.Enums;
using Recipes.Domain.Common.Results;
using Recipes.Domain.Recipes.Models;

namespace Recipes.Infrastructure.Recipes.Services;

public class RatingsService(IRatingsRepository ratingsRepository, IDistributedCache cache, IMapper mapper)
    : IRatingsService
{
    private const string RecipeCacheKeyPrefix = "Recipe";

    public async Task<OneOf<SuccessWithValue<IReadOnlyList<RatingReadDto>>, Error>> GetRatingsForRecipeAsync(
        Guid recipeId, CancellationToken token)
    {
        var ingredientsFromDb = await ratingsRepository.GetRatingsForRecipeAsync(recipeId, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        if (ingredientsFromDb.Count == 0)
        {
            return new Error(ErrorType.NotFound);
        }

        var result = mapper.Map<IList<RatingReadDto>>(ingredientsFromDb);

        return new SuccessWithValue<IReadOnlyList<RatingReadDto>>(result.AsReadOnly().ToList());
    }

    public async Task<OneOf<SuccessWithValue<RatingReadDto>, Error>> CreateRatingForRecipeAsync(RatingCreateDto rating,
        CancellationToken token)
    {
        var ratingToCreate = mapper.Map<RatingModel>(rating);

        var createdRating = await ratingsRepository.CreateRatingForRecipeAsync(ratingToCreate, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        await ratingsRepository.SaveChangesAsync(token).ConfigureAwait(ConfigureAwaitOptions.None);

        var result = mapper.Map<RatingReadDto>(createdRating);

        var cacheKey = $"{RecipeCacheKeyPrefix}_{createdRating.RecipeId}";

        await cache.RemoveAsync(cacheKey, token).ConfigureAwait(ConfigureAwaitOptions.None);

        return new SuccessWithValue<RatingReadDto>(result);
    }

    public async Task<OneOf<Success, Error>> DeleteRecipeAsync(RatingDeleteDto rating, Guid userId,
        CancellationToken token)
    {
        var recipeToCheck = await ratingsRepository.GetRatingsAsync(rating.Id, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        if (recipeToCheck?.UserId != userId)
        {
            return new Error(ErrorType.Unauthorized);
        }

        var deleteOperation =
            await ratingsRepository.DeleteRatingAsync(rating, token)
                .ConfigureAwait(ConfigureAwaitOptions.None);

        await ratingsRepository.SaveChangesAsync(token).ConfigureAwait(ConfigureAwaitOptions.None);

        if (deleteOperation == DeleteType.DeleteFailed)
        {
            return new Error(ErrorType.OperationFailed);
        }

        return new Success();
    }
}