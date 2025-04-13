using Recipes.Application.Recipes.Models;

namespace Recipes.Application.Recipes.Services;

public interface IEmbeddingsService
{
    Task<EmbeddingModel?> GetEmbeddingAsync(string text, CancellationToken token);
}