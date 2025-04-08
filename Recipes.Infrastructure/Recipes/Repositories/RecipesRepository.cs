using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Repositories;
using Recipes.Domain.Common.Enums;
using Recipes.Domain.Recipes.Models;
using Recipes.Infrastructure.Common.Data;

namespace Recipes.Infrastructure.Recipes.Repositories;

public class RecipesRepository(AppDbContext ctx, IMapper mapper) : IRecipesRepository
{
    public async Task<RecipeModel?> GetRecipeByIdAsync(Guid recipeId, CancellationToken token)
    {
        return await ctx.Recipes.AsNoTracking().FirstOrDefaultAsync(recipe => recipe.Id == recipeId, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);
    }

    public async Task<RecipeModel?> GetRandomRecipeAsync(CancellationToken token)
    {
        return await ctx.Recipes.AsNoTracking().OrderBy(x => Guid.NewGuid()).Take(1)
            .FirstOrDefaultAsync(token).ConfigureAwait(ConfigureAwaitOptions.None);
    }

    public async Task<IList<RecipeModel>> GetRecipesAsync(CancellationToken token)
    {
        return await ctx.Recipes.AsNoTracking().ToListAsync(token)
            .ConfigureAwait(ConfigureAwaitOptions.None);
    }

    public async Task<RecipeModel?> CreateRecipeAsync(RecipeModel recipe, CancellationToken token)
    {
        await ctx.Recipes.AddAsync(recipe, token).ConfigureAwait(false);

        return recipe;
    }

    public async Task<UpdateType> UpdateRecipeAsync(RecipeEditDto recipe, CancellationToken token)
    {
        var recipeForModification =
            await ctx.Recipes.FirstOrDefaultAsync(u => u.Id == recipe.Id, token).ConfigureAwait(false);

        if (recipeForModification is null)
        {
            return UpdateType.UpdateFailed;
        }

        if (recipe.Description is { } description)
        {
            recipeForModification.Description = description;
        }

        if (recipe.Title is { } title)
        {
            recipeForModification.Title = title;
        }

        if (recipe.Ingredients.Count > 0)
        {
            ctx.Ingredients.RemoveRange(ctx.Ingredients.Where(c => c.RecipeId == recipe.Id));
            var ingredients = mapper.Map<ICollection<IngredientModel>>(recipe.Ingredients);
            await ctx.Ingredients.AddRangeAsync(ingredients, token).ConfigureAwait(false);
        }

        if (recipe.Types.Count > 0)
        {
            recipeForModification.Types = recipe.Types.ToList();
        }

        return UpdateType.UpdateSuccessful;
    }

    public async Task<DeleteType> DeleteRecipeAsync(RecipeDeleteDto recipe, CancellationToken token)
    {
        var recipeForDeletion =
            await ctx.Recipes.FirstOrDefaultAsync(u => u.Id == recipe.Id, token)
                .ConfigureAwait(false);

        if (recipeForDeletion is null)
        {
            return DeleteType.DeleteFailed;
        }

        ctx.Remove(recipeForDeletion);

        return DeleteType.DeleteSuccessful;
    }

    public Task SaveChangesAsync(CancellationToken token)
    {
        return ctx.SaveChangesAsync(token);
    }
}