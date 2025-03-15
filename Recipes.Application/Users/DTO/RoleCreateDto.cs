using Recipes.Domain.Users.Enums;

namespace Recipes.Application.Users.DTO;

public class RoleCreateDto
{
    public string Name { get; set; } = null!;
    
    public RoleType Role { get; set; }
}