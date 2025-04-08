using Recipes.Domain.Users.Enums;

namespace Recipes.Application.Users.DTO;

public class RoleReadDto
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public RoleType Role { get; set; }
}