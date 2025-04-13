using System.ComponentModel.DataAnnotations;

namespace Recipes.Infrastructure.Common.Options;

public class ExternalOptions
{
    [Required] public string AiService { get; set; } = null!;
}