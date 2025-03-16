namespace Recipes.Application.Users.Validators;

public class RoleCreateValidator : AbstractValidator<RoleCreateDto>
{
    public RoleCreateValidator()
    {
        RuleFor(role => role.Name).NotEmpty().NotNull();
    }
}