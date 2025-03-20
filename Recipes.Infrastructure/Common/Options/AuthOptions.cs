using System.ComponentModel.DataAnnotations;

namespace Recipes.Infrastructure.Common.Options;

public class AuthOptions
{
    [Required] public string ClientId { get; set; } = null!;

    [Required] public string ClientSecret { get; set; } = null!;

    [Required] public string TokenEndpoint { get; set; } = null!;

    [Required] public string UserEndpoint { get; set; } = null!;

    [Required] public string AuthorizationEndpoint { get; set; } = null!;
}