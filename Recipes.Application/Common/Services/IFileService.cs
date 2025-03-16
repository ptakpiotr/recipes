namespace Recipes.Application.Common.Services;

public interface IFileService
{
    Task<Stream> GetFileContentsAsync(string fileName, CancellationToken token = default);

    Task SaveFileContentsAsync(string fileName, Stream stream, CancellationToken token = default);
}