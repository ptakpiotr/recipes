namespace Recipes.Application.Users.DTO;

public class UserBasicReadDto
{
    public Guid Id { get; init; }
    
    public string UserName { get; init; } = null!;
    
    public string UserImageLink { get; init; } = null!;
}