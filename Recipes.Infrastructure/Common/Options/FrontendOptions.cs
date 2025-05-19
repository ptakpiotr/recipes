using System.ComponentModel.DataAnnotations;

namespace Recipes.Infrastructure.Common.Options;

public class FrontendOptions
{
    [Required] public string Url { get; set; } = null!;
}