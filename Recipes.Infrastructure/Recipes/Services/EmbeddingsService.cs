using System.Net.Http.Json;
using Polly;
using Polly.Registry;
using Recipes.Application.Recipes.Models;
using Recipes.Application.Recipes.Services;

namespace Recipes.Infrastructure.Recipes.Services;

public class EmbeddingsService(
    IHttpClientFactory factory,
    ResiliencePipelineProvider<string> resiliencePipelineProvider) : IEmbeddingsService
{
    private readonly HttpClient _client = factory.CreateClient("ai-client");
    private readonly ResiliencePipeline _pipeline = resiliencePipelineProvider.GetPipeline("retry-pipeline");

    public async Task<EmbeddingModel?> GetEmbeddingAsync(string text, CancellationToken token)
    {
        EmbeddingModel? embeddingModel = null;

        await _pipeline.ExecuteAsync(async (cancellationToken) =>
        {
            using var res = await _client.PostAsJsonAsync("embed", new EmbeddingRequestModel()
            {
                Message = text
            }, cancellationToken: cancellationToken).ConfigureAwait(ConfigureAwaitOptions.None);

            var embedding = await res.Content.ReadFromJsonAsync<EmbeddingModel>(cancellationToken)
                .ConfigureAwait(false);

            embeddingModel = embedding;
        }, token).ConfigureAwait(false);

        return embeddingModel;
    }
}