using Recipes.Application.Recipes.DTO;

namespace Recipes.Application.Recipes.Validators;

public class RecipeCreateValidator : AbstractValidator<RecipeCreateDto>
{
    public RecipeCreateValidator()
    {
        RuleFor(x => x.AuthorId).NotEqual(Guid.Empty);
        RuleFor(x => x.Title).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Image).NotEmpty();
        RuleFor(x => x.Types).NotEmpty();
        RuleFor(x => x.Ingredients).NotEmpty();
    }
}