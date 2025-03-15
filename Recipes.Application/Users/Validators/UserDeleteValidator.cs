namespace Recipes.Application.Users.Validators;

internal class UserDeleteValidator : AbstractValidator<UserDeleteDto>
{
    public UserDeleteValidator()
    {
        RuleFor(u => u.Id).NotEqual(Guid.Empty);
    }
}