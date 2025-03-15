using System.ComponentModel.DataAnnotations;

namespace Recipes.Infrastructure.Common.Options;

public class StorageOptions
{
    [Required] public string App { get; set; } = null!;

    [Required] public string Hangfire { get; set; } = null!;
    
    [Required] public string Redis { get; set; } = null!;
}