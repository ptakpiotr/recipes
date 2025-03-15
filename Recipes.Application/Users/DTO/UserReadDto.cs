namespace Recipes.Application.Users.DTO;

public class UserReadDto
{
    public Guid Id { get; init; }

    public string UserName { get; init; } = null!;

    public string UserImageLink { get; init; } = null!;
}