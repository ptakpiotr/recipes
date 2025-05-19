using System.Text.Json;
using AutoMapper;
using MassTransit.Middleware;
using Microsoft.Extensions.Caching.Distributed;
using OneOf;
using Polly;
using Polly.Registry;
using Recipes.Application.Common.Services;
using Recipes.Application.Recipes.DTO;
using Recipes.Application.Recipes.Repositories;
using Recipes.Application.Recipes.Services;
using Recipes.Domain.Common.Enums;
using Recipes.Domain.Common.Results;
using Recipes.Domain.Recipes.Enums;
using Recipes.Domain.Recipes.Models;

namespace Recipes.Infrastructure.Recipes.Services;

public class RecipeService(
    IRecipesRepository recipesRepository,
    IDistributedCache cache,
    IFileService fileService,
    IEmbeddingsService embeddingsService,
    IMapper mapper)
    : IRecipeService
{
    private const string RecipeCacheKeyPrefix = "Recipe";

    public async Task<OneOf<SuccessWithValue<RecipeReadDto>, Error>> GetRecipeByIdAsync(Guid recipeId,
        CancellationToken token)
    {
        var cacheKey = $"{RecipeCacheKeyPrefix}_{recipeId}";
        var cacheStrResult = await cache.GetStringAsync(cacheKey, token);

        if (!string.IsNullOrEmpty(cacheStrResult))
        {
            var recipe = JsonSerializer.Deserialize<RecipeReadDto>(cacheStrResult);

            if (recipe is null)
            {
                return new Error(ErrorType.Parse, nameof(recipe));
            }

            recipe.ImageUrl = $"/api/files/recipe_{recipe.Id}";

            return new SuccessWithValue<RecipeReadDto>(recipe);
        }

        var recipeFromDb = await recipesRepository.GetRecipeByIdAsync(recipeId, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        if (recipeFromDb is null)
        {
            return new Error(ErrorType.NotFound);
        }

        await cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(recipeFromDb), new DistributedCacheEntryOptions()
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1)
        }, token);

        var result = mapper.Map<RecipeReadDto>(recipeFromDb);

        result.ImageUrl = $"/api/files/recipe_{result.Id}";

        return new SuccessWithValue<RecipeReadDto>(result);
    }

    public async Task<OneOf<SuccessWithValue<RecipeReadDto>, Error>> GetRandomRecipeAsync(CancellationToken token)
    {
        var recipe = await recipesRepository.GetRandomRecipeAsync(token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        if (recipe is null)
        {
            return new Error(ErrorType.NotFound);
        }

        var result = mapper.Map<RecipeReadDto>(recipe);

        result.ImageUrl = $"/api/files/recipe_{result.Id}";

        return new SuccessWithValue<RecipeReadDto>(result);
    }

    public async Task<OneOf<SuccessWithValue<IReadOnlyList<RecipeReadDto>>, Error>> GetAllRecipesAsync(
        RecipeType? recipeType,
        CancellationToken token)
    {
        var recipesFromDb = await recipesRepository.GetRecipesAsync(token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        if (recipeType is not null)
        {
            recipesFromDb = recipesFromDb.Where(r => r.Types.Any(r => r == recipeType)).ToList();
        }

        if (recipesFromDb.Count == 0)
        {
            return new Error(ErrorType.NotFound);
        }

        var result = mapper.Map<IList<RecipeReadDto>>(recipesFromDb);

        foreach (var recipe in result)
        {
            recipe.ImageUrl = $"/api/files/recipe_{recipe.Id}";
        }

        return new SuccessWithValue<IReadOnlyList<RecipeReadDto>>(result.AsReadOnly().ToList());
    }

    public async Task<OneOf<SuccessWithValue<RecipeReadDto>, Error>> CreateRecipeAsync(RecipeCreateDto recipe,
        CancellationToken token)
    {
        var recipeToCreate = mapper.Map<RecipeModel>(recipe);

        var embedding = await embeddingsService.GetEmbeddingAsync(recipe.Description, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        if (embedding is null)
        {
            return new Error(ErrorType.OperationFailed);
        }

        var createdRecipe = await recipesRepository.CreateRecipeAsync(recipeToCreate, embedding, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        await fileService.SaveFileContentsAsync($"recipe_{createdRecipe?.Id}", recipe.Image.OpenReadStream(), token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        await recipesRepository.SaveChangesAsync(token).ConfigureAwait(ConfigureAwaitOptions.None);

        var result = mapper.Map<RecipeReadDto>(createdRecipe);

        return new SuccessWithValue<RecipeReadDto>(result);
    }

    public async Task<OneOf<Success, Error>> MassCreateRecipesAsync(
        IList<RecipeCreateDto> recipes, CancellationToken token)
    {
        List<Task<OneOf<SuccessWithValue<RecipeReadDto>, Error>>> tasks = [];
        
        tasks.AddRange(recipes.Select(recipe => CreateRecipeAsync(recipe, token)));

        var res = await Task.WhenAll(tasks).ConfigureAwait(ConfigureAwaitOptions.None);

        return res.All(r => r.IsT0) switch
        {
            true => new Success(),
            _ => new Error(ErrorType.OperationFailed)
        };
    }

    public async Task<OneOf<Success, Error>> UpdateRecipeAsync(RecipeEditDto recipe, Guid userId,
        CancellationToken token)
    {
        var recipeToCheck = await recipesRepository.GetRecipeByIdAsync(recipe.Id, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        if (recipeToCheck?.AuthorId != userId)
        {
            return new Error(ErrorType.Unauthorized);
        }

        var embedding = await embeddingsService
            .GetEmbeddingAsync(recipe.Description ?? recipeToCheck.Description, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        if (embedding is null)
        {
            return new Error(ErrorType.OperationFailed);
        }

        var updateOperation =
            await recipesRepository.UpdateRecipeAsync(recipe, embedding, token)
                .ConfigureAwait(ConfigureAwaitOptions.None);

        await recipesRepository.SaveChangesAsync(token).ConfigureAwait(ConfigureAwaitOptions.None);

        var cacheKey = $"{RecipeCacheKeyPrefix}_{recipeToCheck.Id}";

        await cache.RemoveAsync(cacheKey, token).ConfigureAwait(ConfigureAwaitOptions.None);

        if (updateOperation == UpdateType.UpdateFailed)
        {
            return new Error(ErrorType.OperationFailed);
        }

        return new Success();
    }

    public async Task<OneOf<Success, Error>> DeleteRecipeAsync(RecipeDeleteDto recipe, Guid userId,
        CancellationToken token)
    {
        var recipeToCheck = await recipesRepository.GetRecipeByIdAsync(recipe.Id, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        if (recipeToCheck?.AuthorId != userId)
        {
            return new Error(ErrorType.Unauthorized);
        }

        var deleteOperation =
            await recipesRepository.DeleteRecipeAsync(recipe, token)
                .ConfigureAwait(ConfigureAwaitOptions.None);

        await recipesRepository.SaveChangesAsync(token).ConfigureAwait(ConfigureAwaitOptions.None);

        if (deleteOperation == DeleteType.DeleteFailed)
        {
            return new Error(ErrorType.OperationFailed);
        }

        return new Success();
    }

    public async Task<OneOf<Success, Error>> CheckOwnershipAsync(Guid userId, Guid recipeId, CancellationToken token)
    {
        var recipe = await recipesRepository.GetRecipeByIdAsync(recipeId, token)
            .ConfigureAwait(ConfigureAwaitOptions.None);

        if (recipe?.AuthorId == userId)
        {
            return new Success();
        }

        return new Error(ErrorType.Unauthorized);
    }
}