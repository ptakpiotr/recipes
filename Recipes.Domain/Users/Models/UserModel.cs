using Recipes.Domain.Common.Abstractions;

namespace Recipes.Domain.Users.Models;

public class UserModel : IStorableEntity
{
    public Guid Id { get; set; }

    public string ExternalId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string UserImageLink { get; set; } = null!;

    public bool SendNewsletter { get; set; } = false;
}