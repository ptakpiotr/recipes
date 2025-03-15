using Recipes.Domain.Common.Abstractions;
using Recipes.Domain.Users.Enums;

namespace Recipes.Domain.Users.Models;

public class RoleModel : IStorableEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;
    
    public RoleType Role { get; set; }
}