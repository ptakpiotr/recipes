namespace Recipes.Application.Users.Validators;

internal class UserEditValidator : AbstractValidator<UserEditDto>
{
    public UserEditValidator()
    {
        RuleFor(u => u.Id).NotEqual(Guid.Empty);
        RuleFor(u => u.UserImageLink).Matches(CommonValidators.HttpsUrlRegex);
    }
}