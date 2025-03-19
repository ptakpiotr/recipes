using System.ComponentModel.DataAnnotations;

namespace Recipes.Infrastructure.Common.Options;

public class EmailOptions
{
    [Required] public string Email { get; set; } = null!;

    [Required] public string Host { get; set; } = null!;

    [Required] public int Port { get; set; }

    [Required] public string Password { get; set; } = null!;
}