namespace Recipes.Application.Users.DTO;

public class UserReadDto
{
    public Guid Id { get; init; }

    public string UserEmail { get; set; } = null!;

    public string UserName { get; init; } = null!;

    public string UserImageLink { get; init; } = null!;

    public ICollection<RoleReadDto> Roles { get; set; } = null!;
}