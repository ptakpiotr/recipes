using System.ComponentModel.DataAnnotations;

namespace Recipes.Infrastructure.Common.Options;

public class VapidOptions
{
    [Required] public string PublicKey { get; set; } = null!;
    [Required] public string PrivateKey { get; set; } = null!;
}