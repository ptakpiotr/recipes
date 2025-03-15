namespace Recipes.Application.Users.DTO;

public class UserCreateDto
{
    public string ExternalId { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string UserImageLink { get; set; } = null!;
}