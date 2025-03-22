namespace Recipes.Application.Users.Validators;

public class UserEditValidator : AbstractValidator<UserEditDto>
{
    public UserEditValidator()
    {
        RuleFor(u => u.Id).NotEqual(Guid.Empty);
    }
}