using System.ComponentModel.DataAnnotations;

namespace Recipes.Infrastructure.Common.Options;

public class S3Options
{
    [Required] public string Bucket { get; set; } = null!;

    [Required] public string ConnStr { get; set; } = null!;
    
    [Required] public string AccessKeyId { get; set; } = null!;
    
    [Required] public string SecretAccessKey { get; set; } = null!;
}