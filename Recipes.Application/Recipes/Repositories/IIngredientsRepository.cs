using Recipes.Application.Recipes.DTO;
using Recipes.Domain.Common.Enums;
using Recipes.Domain.Recipes.Models;

namespace Recipes.Application.Recipes.Repositories;

public interface IIngredientsRepository
{
    Task<IList<IngredientModel>> GetIngredientsForRecipeAsync(Guid recipeId, CancellationToken token);
    
    Task<IngredientModel?> GetIngredientByIdAsync(Guid id, CancellationToken token);

    Task<IngredientModel?> CreateIngredientAsync(IngredientModel ingredient, CancellationToken token);

    Task<UpdateType> UpdateIngredientAsync(IngredientEditDto ingredient, CancellationToken token);

    Task<DeleteType> DeleteIngredientAsync(IngredientDeleteDto ingredient, CancellationToken token);

    Task SaveChangesAsync(CancellationToken token);
}