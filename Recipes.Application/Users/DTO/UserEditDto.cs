namespace Recipes.Application.Users.DTO;

public class UserEditDto
{
    public Guid Id { get; set; }

    public string? UserImageLink { get; set; }

    public bool? SendNewsletter { get; set; }
}