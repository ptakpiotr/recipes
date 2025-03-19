using AutoMapper;
using OneOf;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Repositories;
using Recipes.Application.Recipes.Services;
using Recipes.Domain.Common.Enums;
using Recipes.Domain.Common.Results;
using Recipes.Domain.Recipes.Models;

namespace Recipes.Infrastructure.Recipes.Services;

public class RatingsService(IRatingsRepository ratingsRepository, IMapper mapper) : IRatingsService
{
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

        return new SuccessWithValue<RatingReadDto>(result);
    }
}