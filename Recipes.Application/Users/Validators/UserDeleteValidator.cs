namespace Recipes.Application.Users.Validators;

public class UserDeleteValidator : AbstractValidator<UserDeleteDto>
{
    public UserDeleteValidator()
    {
        RuleFor(u => u.Id).NotEqual(Guid.Empty);
    }
}