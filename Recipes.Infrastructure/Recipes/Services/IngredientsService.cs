using AutoMapper;
using OneOf;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Repositories;
using Recipes.Application.Recipes.Services;
using Recipes.Domain.Common.Enums;
using Recipes.Domain.Common.Results;
using Recipes.Domain.Recipes.Models;

namespace Recipes.Infrastructure.Recipes.Services;

public class IngredientsService(
    IIngredientsRepository ingredientsRepository,
    IRecipesRepository recipesRepository,
    IMapper mapper) : IIngredientsService
{
    public async Task<OneOf<SuccessWithValue<IReadOnlyList<IngredientReadDto>>, Error>> GetIngredientsForRecipeAsync(
        Guid recipeId, CancellationToken token)
    {
        var ingredientsFromDb = await ingredientsRepository.GetIngredientsForRecipeAsync(recipeId, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        if (ingredientsFromDb.Count == 0)
        {
            return new Error(ErrorType.NotFound);
        }

        var result = mapper.Map<IList<IngredientReadDto>>(ingredientsFromDb);

        return new SuccessWithValue<IReadOnlyList<IngredientReadDto>>(result.AsReadOnly().ToList());
    }

    public async Task<OneOf<SuccessWithValue<IngredientReadDto>, Error>> CreateIngredientAsync(
        IngredientCreateDto ingredient,
        CancellationToken token)
    {
        var ingredientToCreate = mapper.Map<IngredientModel>(ingredient);

        var createdIngredient = await ingredientsRepository.CreateIngredientAsync(ingredientToCreate, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        await ingredientsRepository.SaveChangesAsync(token).ConfigureAwait(ConfigureAwaitOptions.None);

        var result = mapper.Map<IngredientReadDto>(createdIngredient);

        return new SuccessWithValue<IngredientReadDto>(result);
    }

    public async Task<OneOf<Success, Error>> UpdateIngredientAsync(IngredientEditDto ingredient, Guid userId,
        CancellationToken token)
    {
        var recipeToCheck = await recipesRepository.GetRecipeByIdAsync(ingredient.RecipeId, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        if (recipeToCheck?.AuthorId != userId)
        {
            return new Error(ErrorType.Unauthorized);
        }

        var updateOperation =
            await ingredientsRepository.UpdateIngredientAsync(ingredient, token)
                .ConfigureAwait(ConfigureAwaitOptions.None);

        await ingredientsRepository.SaveChangesAsync(token).ConfigureAwait(ConfigureAwaitOptions.None);

        if (updateOperation == UpdateType.UpdateFailed)
        {
            return new Error(ErrorType.OperationFailed);
        }

        return new Success();
    }

    public async Task<OneOf<Success, Error>> DeleteIngredientAsync(IngredientDeleteDto ingredient, Guid userId,
        CancellationToken token)
    {
        var recipeToCheck = await recipesRepository.GetRecipeByIdAsync(ingredient.RecipeId, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        if (recipeToCheck?.AuthorId != userId)
        {
            return new Error(ErrorType.Unauthorized);
        }

        var deleteOperation =
            await ingredientsRepository.DeleteIngredientAsync(ingredient, token)
                .ConfigureAwait(ConfigureAwaitOptions.None);

        await ingredientsRepository.SaveChangesAsync(token).ConfigureAwait(ConfigureAwaitOptions.None);

        if (deleteOperation == DeleteType.DeleteFailed)
        {
            return new Error(ErrorType.OperationFailed);
        }

        return new Success();
    }
}