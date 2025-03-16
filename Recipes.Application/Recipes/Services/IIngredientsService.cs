using Recipes.Application.Recipes.DTO;

namespace Recipes.Application.Recipes.Services;

public interface IIngredientsService
{
    Task<OneOf<SuccessWithValue<IReadOnlyList<IngredientReadDto>>, Error>> GetIngredientsForRecipeAsync(Guid recipeId,
        CancellationToken token);

    Task<OneOf<SuccessWithValue<IngredientReadDto>, Error>> CreateIngredientAsync(IngredientCreateDto ingredient,
        CancellationToken token);

    Task<OneOf<Success, Error>> UpdateIngredientAsync(IngredientEditDto ingredient, Guid userId,
        CancellationToken token);

    Task<OneOf<Success, Error>> DeleteIngredientAsync(IngredientDeleteDto ingredient, Guid userId,
        CancellationToken token);
}