using Recipes.Application.Recipes.DTO;
using Recipes.Domain.Common.Enums;
using Recipes.Domain.Recipes.Models;

namespace Recipes.Application.Recipes.Repositories;

public interface IRecipesRepository
{
    Task<RecipeModel> GetRecipeByIdAsync(Guid recipeId, CancellationToken token);

    Task<RecipeModel> GetRandomRecipeAsync(CancellationToken token);

    Task<IList<RecipeModel>> GetRecipesAsync(CancellationToken token);

    Task<RecipeModel?> CreateRecipeAsync(RecipeModel recipe, CancellationToken token);

    Task<UpdateType> UpdateRecipeAsync(RecipeEditDto recipe, CancellationToken token);

    Task<DeleteType> DeleteRecipeAsync(RecipeDeleteDto recipe, CancellationToken token);

    Task SaveChangesAsync(CancellationToken token);
}