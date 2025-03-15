namespace Recipes.Application.Users.Validators;

internal class UserCreateValidator : AbstractValidator<UserCreateDto>
{
    public UserCreateValidator()
    {
        RuleFor(u => u.ExternalId).NotEmpty();
        RuleFor(u=>u.UserName).NotEmpty();
        RuleFor(u => u.UserImageLink).Matches(CommonValidators.HttpsUrlRegex);
    }
}