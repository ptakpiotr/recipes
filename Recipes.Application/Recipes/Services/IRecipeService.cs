using Recipes.Application.Recipes.DTO;

namespace Recipes.Application.Recipes.Services;

public interface IRecipeService
{
    Task<OneOf<SuccessWithValue<RecipeReadDto>, Error>> GetRecipeByIdAsync(Guid recipeId, CancellationToken token);

    Task<OneOf<SuccessWithValue<RecipeReadDto>, Error>> GetRandomRecipeAsync(CancellationToken token);

    Task<OneOf<SuccessWithValue<IReadOnlyList<RecipeReadDto>>, Error>> GetAllRecipesAsync(CancellationToken token);

    Task<OneOf<SuccessWithValue<RecipeReadDto>, Error>> CreateRecipeAsync(RecipeCreateDto recipe,
        CancellationToken token);

    Task<OneOf<Success, Error>> UpdateRecipeAsync(RecipeEditDto recipe, Guid userId, CancellationToken token);

    Task<OneOf<Success, Error>> DeleteRecipeAsync(RecipeDeleteDto recipe, Guid userId, CancellationToken token);
}