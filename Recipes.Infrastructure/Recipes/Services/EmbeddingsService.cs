using System.Net.Http.Json;
using Recipes.Application.Recipes.Models;
using Recipes.Application.Recipes.Services;

namespace Recipes.Infrastructure.Recipes.Services;

public class EmbeddingsService(IHttpClientFactory factory) : IEmbeddingsService
{
    private readonly HttpClient _client = factory.CreateClient("ai-client");

    //TODO: add caching and resiliency (Polly)
    public async Task<EmbeddingModel?> GetEmbeddingAsync(string text, CancellationToken token)
    {
        using var res = await _client.PostAsJsonAsync("embed", new EmbeddingRequestModel()
        {
            Message = text
        }, cancellationToken: token).ConfigureAwait(ConfigureAwaitOptions.None);

        var embedding = await res.Content.ReadFromJsonAsync<EmbeddingModel>(token).ConfigureAwait(false);

        return embedding;
    }
}