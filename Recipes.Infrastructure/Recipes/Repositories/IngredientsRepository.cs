using Microsoft.EntityFrameworkCore;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Repositories;
using Recipes.Domain.Common.Enums;
using Recipes.Domain.Recipes.Models;
using Recipes.Infrastructure.Common.Data;

namespace Recipes.Infrastructure.Recipes.Repositories;

public class IngredientsRepository(AppDbContext ctx) : IIngredientsRepository
{
    public async Task<IList<IngredientModel>> GetIngredientsForRecipeAsync(Guid recipeId, CancellationToken token)
    {
        return await ctx.Ingredients.AsNoTracking().Where(x => x.Id == recipeId).ToListAsync(token)
            .ConfigureAwait(ConfigureAwaitOptions.None);
    }

    public async Task<IngredientModel?> GetIngredientByIdAsync(Guid id, CancellationToken token)
    {
        return await ctx.Ingredients.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);
    }

    public async Task<IngredientModel?> CreateIngredientAsync(IngredientModel ingredient, CancellationToken token)
    {
        await ctx.Ingredients.AddAsync(ingredient, token);

        return ingredient;
    }

    public async Task<UpdateType> UpdateIngredientAsync(IngredientEditDto ingredient, CancellationToken token)
    {
        var ingredientForModification =
            await ctx.Ingredients.FirstOrDefaultAsync(u => u.Id == ingredient.Id, token).ConfigureAwait(false);

        if (ingredientForModification is null)
        {
            return UpdateType.UpdateFailed;
        }

        if (ingredient.Description is { } description)
        {
            ingredientForModification.Description = description;
        }

        return UpdateType.UpdateSuccessful;
    }

    public async Task<DeleteType> DeleteIngredientAsync(IngredientDeleteDto ingredient, CancellationToken token)
    {
        var ingredientForDeletion =
            await ctx.Ingredients.FirstOrDefaultAsync(u => u.Id == ingredient.Id, token).ConfigureAwait(false);

        if (ingredientForDeletion is null)
        {
            return DeleteType.DeleteFailed;
        }

        ctx.Remove(ingredientForDeletion);

        return DeleteType.DeleteSuccessful;
    }

    public Task SaveChangesAsync(CancellationToken token)
    {
        return ctx.SaveChangesAsync(token);
    }
}