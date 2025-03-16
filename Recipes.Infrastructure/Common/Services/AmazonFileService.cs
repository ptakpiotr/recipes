using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using Recipes.Application.Common.Services;
using Recipes.Infrastructure.Common.Options;

namespace Recipes.Infrastructure.Common.Services;

public class AmazonFileService(IAmazonS3 s3, IOptionsSnapshot<S3Options> options) : IFileService
{
    public async Task<Stream> GetFileContentsAsync(string fileName, CancellationToken token = default)
    {
        var obj = await s3.GetObjectAsync(new GetObjectRequest
        {
            BucketName = options.Value.Bucket,
            Key = fileName
        }, token).ConfigureAwait(ConfigureAwaitOptions.None);

        return obj.ResponseStream;
    }

    public async Task SaveFileContentsAsync(string fileName, Stream stream, CancellationToken token = default)
    {
        await s3.PutObjectAsync(new PutObjectRequest
        {
            BucketName = options.Value.Bucket,
            Key = fileName,
            InputStream = stream
        }, token).ConfigureAwait(ConfigureAwaitOptions.None);
    }
}